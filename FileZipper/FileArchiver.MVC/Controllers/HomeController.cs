using FileArchiver.Common.ViewModels;
using FileArchiver.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FileArchiver.MVC.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        public readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        private UploadFileViewModel uploadFileVM;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult UpdateUser()
        {
            return View("_UpdateClientData");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUsersViewModel updateUsersViewModel)
        {
            string result;
            var credentials = JsonConvert.DeserializeObject<CredentialsViewModel>(HttpContext.Session.GetString("SessionCredentials"));
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getUserMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:login");
            try
            {
                var updateUserEndpoint = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:updateUser");
                result = await HttpClientHelper.UpdateUser(updateUsersViewModel, baseAddress + updateUserEndpoint);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                _logger.LogError(ex.Message);
                return View("_UpdateClientData");
            }
            UserViewModel loggedUser = await HttpClientHelper.GetUser(credentials, baseAddress + getUserMethod);
            if (result == "Success")
            {
                if (loggedUser.IsAdmin)
                {
                    return RedirectToAction("GetToAdminView");
                }
                else
                {
                    return RedirectToAction("GetToUserView");
                }
            }
            else
            {
                ViewBag.ErrorMessage = result;
                return View("_UpdateClientData");
            }
        }

        public async Task<IActionResult> UploadFile()
        {
            UserViewModel user;
            uploadFileVM = new UploadFileViewModel();
            List<UserViewModel> users;
            List<DocumentTypeViewModel> documentTypes;
            var credentials = JsonConvert.DeserializeObject<CredentialsViewModel>(HttpContext.Session.GetString("SessionCredentials"));
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getUserMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:login");
            var getAllUsersMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:getAllUsers");
            var getDocumentTypesMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:DocumentTypes:getAllDocumentTypes");
            try
            {
                user = await HttpClientHelper.GetUser(credentials, baseAddress + getUserMethod);
                users = await HttpClientHelper.GetAllUsers(baseAddress, getAllUsersMethod);
                documentTypes = await HttpClientHelper.GetAllDocumentTypes(baseAddress, getDocumentTypesMethod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("_CouldNotLoadErrorView", ex.Message);
            }
            uploadFileVM.Users = users;
            uploadFileVM.Documents = documentTypes;
            uploadFileVM.File = new FileViewModel()
            {
                Created = DateTime.Now,
                User = user
            };
            return View("InsertFileForm", uploadFileVM);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(UploadFileViewModel uploadFileViewModel)
        {
            var credentials = JsonConvert.DeserializeObject<CredentialsViewModel>(HttpContext.Session.GetString("SessionCredentials"));
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getUserMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:getUserByUsername");
            UserViewModel loggeduser = new UserViewModel();
            UserViewModel user = new UserViewModel();
            try
            {
                loggeduser = await HttpClientHelper.GetUserByUsername(credentials.Username, baseAddress, getUserMethod);
                user = await HttpClientHelper.GetUserByUsername(uploadFileViewModel.Username, baseAddress, getUserMethod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            

            if (uploadFileViewModel.File == null)
            {
                return RedirectToAction("UploadFile");
            }

            uploadFileViewModel.File.UserThatAFileIsUploadedToUsername = user.Username;
            uploadFileViewModel.File.IsDownloaded = false;
            uploadFileViewModel.File.IsConfirmed = false;
            uploadFileViewModel.File.Created = DateTime.Now;
            uploadFileViewModel.File.DocumentName = uploadFileViewModel.DocumentName;
            uploadFileViewModel.File.DocumentTypeId = uploadFileViewModel.DocumentTypeId;
            uploadFileViewModel.File.FileName = string.Empty;
            uploadFileViewModel.File.CreatorUsername = credentials.Username;
            uploadFileViewModel.File.CreatorId = loggeduser.Id;
            uploadFileViewModel.File.UserId = user.Id;

            var uploadFileMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Files:uploadFile");
            string response = string.Empty;
            try
            {
                response = await HttpClientHelper.UploadFile(uploadFileViewModel.File, baseAddress + uploadFileMethod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("GetToAdminView", loggeduser);
        }

        public async Task<IActionResult> GetToAdminView()
        {
            var credentials = JsonConvert.DeserializeObject<CredentialsViewModel>(HttpContext.Session.GetString("SessionCredentials"));
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getUserByUsernameMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:getUserByUsername");
            var getFiles = _configuration.GetValue<string>("ApiEndpoints:Controllers:Files:getAllFiles");
            var getFilesByUsername = _configuration.GetValue<string>("ApiEndpoints:Controllers:Files:getAllFilesByUsername");

            UserViewModel loggeduser = new UserViewModel();

            try
            {
                loggeduser =  await HttpClientHelper.GetUserByUsername(credentials.Username, baseAddress, getUserByUsernameMethod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            var files = await HttpClientHelper.GetAllFiles(baseAddress, getFiles);
            var userFiles = await HttpClientHelper.GetAllFilesByUsername(credentials.Username, baseAddress, getFilesByUsername);
            loggeduser.Files = files;
            ViewBag.DataSource = userFiles;
            return View("AdminLoginView", loggeduser);
        }

        public async Task<IActionResult> GetToUserView()
        {
            var credentials = JsonConvert.DeserializeObject<CredentialsViewModel>(HttpContext.Session.GetString("SessionCredentials"));
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getUserByUsernameMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:getUserByUsername");
            var getFilesByUsername = _configuration.GetValue<string>("ApiEndpoints:Controllers:Files:getAllFilesByUsername");
            List<FileViewModel> userFiles = new List<FileViewModel>(); 
            UserViewModel loggeduser = new UserViewModel();

            try
            {
                loggeduser = await HttpClientHelper.GetUserByUsername(credentials.Username, baseAddress, getUserByUsernameMethod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            try
            {
                userFiles = await HttpClientHelper.GetAllFilesByUsername(credentials.Username, baseAddress, getFilesByUsername);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            ViewBag.DataSource = userFiles;

            return View("UserLoginView", loggeduser);
        }

        [HttpGet("Home/DownloadFile/{fileId}")]
        public async Task<JsonResult> DownloadFile([FromRoute] int fileId)
        {
            {
                var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
                var getFileByIdMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:getFileById");

                FileViewModel filesToBeDownloaded = new FileViewModel();

                try
                {
                    await HttpClientHelper.GetFile(fileId, baseAddress, getFileByIdMethod);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                if (!string.IsNullOrEmpty(filesToBeDownloaded.ErrorMessage))
                {
                    _logger.LogError(filesToBeDownloaded.ErrorMessage);
                    return Json(false);
                }

                return Json(true);
            }
        }
        [HttpGet("Home/DownloadFileQuery")]
        public async Task<FileResult> DownloadFileQuery([FromQuery] int fileId)
        {
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getFileByIdMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:getFileById");
            FileViewModel fileToBeDownloaded = new FileViewModel();
            try
            {
                fileToBeDownloaded = await HttpClientHelper.GetFile(fileId, baseAddress, getFileByIdMethod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return File(fileToBeDownloaded.FileByteData, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileNameWithoutExtension(fileToBeDownloaded.FileName) + ".7z");
        }

        [HttpPost("Home/DeleteFile")]
        public async Task<IActionResult> DeleteFile([FromForm] FileViewModel file)
        {
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var deleteFile = _configuration.GetValue<string>("ApiEndpoints:Controllers:Files:deleteFile");
            FileViewModel fileToBeDeleted = new FileViewModel();
            fileToBeDeleted.FileId = file.FileId;
            try
            {
                fileToBeDeleted = await HttpClientHelper.DeleteFile(fileToBeDeleted, baseAddress, deleteFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Ok(fileToBeDeleted);
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View("RegisterUserForm");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterNewUserViewModel user)
        {
            if ((string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrEmpty(user.Username)) || (string.IsNullOrWhiteSpace(user.FullName) || string.IsNullOrEmpty(user.FullName)) || (string.IsNullOrWhiteSpace(user.ZipPassword) || (string.IsNullOrEmpty(user.ZipPassword))))
            {
                ViewBag.ErrorMessage = "All fields are required";
                return View("RegisterUserForm");
            }
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var registerNewUseremthod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:registerNewUser");
            string result = string.Empty;
            try
            {
                result = await HttpClientHelper.RegisterNewUser(user, baseAddress + registerNewUseremthod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            if (result != "User added")
            {
                _logger.LogInformation("User was not sucessfully added");
                ViewBag.ErrorMessage = "User was not sucessfully added";
                return RedirectToAction("RegisterUser");
            }

            return RedirectToAction("GetToAdminView");
        }

        [HttpGet("Home/GetFileById/{fileId}")]
        public async Task<JsonResult> GetFileById([FromRoute] int fileId)
        {
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getFileByIdMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:getFileById");
            FileViewModel fileToBeDownloaded = new FileViewModel();

            try
            {
                fileToBeDownloaded = await HttpClientHelper.GetFile(fileId, baseAddress, getFileByIdMethod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return Json(fileToBeDownloaded);
        }

        [HttpGet("Home/GetMaskForDocument/{documentName}")]
        public async Task<JsonResult> GetMaskForDocumentType(string documentName)
        {
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getMaskByDocumentTypeMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:DocumentTypes:getMaskByDocumentName");
            string result = string.Empty;

            try
            {
                result = await HttpClientHelper.GetMaskByDocumentType(documentName, baseAddress, getMaskByDocumentTypeMethod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Json(new { result = result });
        }

        [HttpGet("Home/GetAllFilesByUsername/{username}")]
        public async Task<JsonResult> GetAllFilesByUsername([FromRoute] string username)
        {
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getAllFilesByUsername = _configuration.GetValue<string>("ApiEndpoints:Controllers:Files:getAllFilesByUsername");
            List<FileViewModel> result = new List<FileViewModel>();

            try
            {
                result = await HttpClientHelper.GetAllFilesByUsername(username, baseAddress, getAllFilesByUsername);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
           
            return Json(result);
        }
    }
}