﻿using FileArchiver.Common.ViewModels;
using FileArchiver.Services.Helpers;
using FileArchiverCommon.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            try
            {
                var getUserMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:login");
                var getAllUsersMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:getAllUsers");
                var getDocumentTypesMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:DocumentTypes:getAllDocumentTypes");
                user = await HttpClientHelper.GetUser(credentials, baseAddress + getUserMethod);
                users = await HttpClientHelper.GetAllUsers(baseAddress, getAllUsersMethod);
                documentTypes = await HttpClientHelper.GetAllDocumentTypes(baseAddress, getDocumentTypesMethod);
            }
            catch (Exception ex)
            {
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
            var loggeduser = await HttpClientHelper.GetUserByUsername(credentials.Username, baseAddress, getUserMethod);
            var user = await HttpClientHelper.GetUserByUsername(uploadFileViewModel.Username, baseAddress, getUserMethod);

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
            var response = await HttpClientHelper.UploadFile(uploadFileViewModel.File, baseAddress + uploadFileMethod);
            return RedirectToAction("GetToAdminView", loggeduser);
        }

        public async Task<IActionResult> GetToAdminView()
        {
            var credentials = JsonConvert.DeserializeObject<CredentialsViewModel>(HttpContext.Session.GetString("SessionCredentials"));
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");

            var getUserByUsernameMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:getUserByUsername");
            var loggeduser = await HttpClientHelper.GetUserByUsername(credentials.Username, baseAddress, getUserByUsernameMethod);

            var getFiles = _configuration.GetValue<string>("ApiEndpoints:Controllers:Files:getAllFiles");
            var files = await HttpClientHelper.GetAllFiles(baseAddress, getFiles);

            var getFilesByUsername = _configuration.GetValue<string>("ApiEndpoints:Controllers:Files:getAllFilesByUsername");
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
            var loggeduser = await HttpClientHelper.GetUserByUsername(credentials.Username, baseAddress, getUserByUsernameMethod);
            return View("UserLoginView", loggeduser);
        }

        [HttpGet("Home/DownloadFile/{fileId}")]
        public async Task<JsonResult> DownloadFile([FromRoute] int fileId)
        {
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getFileByIdMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:getFileById");
            var fileToBeDownloaded = await HttpClientHelper.GetFile(fileId, baseAddress, getFileByIdMethod);

            if (!string.IsNullOrEmpty(fileToBeDownloaded.ErrorMessage))
                return Json(false);

            return Json(true);
        }

        [HttpGet("Home/DownloadFileQuery")]
        public async Task<FileResult> DownloadFileQuery([FromQuery] int fileId)
        {
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getFileByIdMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:getFileById");
            var fileToBeDownloaded = await HttpClientHelper.GetFile(fileId, baseAddress, getFileByIdMethod);
            return File(fileToBeDownloaded.FileByteData, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileNameWithoutExtension(fileToBeDownloaded.FileName) + ".7z");
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
            var result = await HttpClientHelper.RegisterNewUser(user, baseAddress + registerNewUseremthod);
            if (result != "User added")
            {
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
            var fileToBeDownloaded = await HttpClientHelper.GetFile(fileId, baseAddress, getFileByIdMethod);

            return Json(fileToBeDownloaded);
        }

        [HttpPost("Home/UpdateIsFonfirmed")]
        public async Task<IActionResult> UpdateIsConfirmed([FromBody] FileViewModel file)
        {
            var baseAddress = _configuration.GetValue<string>("ApiEndpoints:BaseAddress");
            var getFileByIdMethod = _configuration.GetValue<string>("ApiEndpoints:Controllers:Users:updateIsConfirmed");

            string result = await HttpClientHelper.UpdateIsConfirmed(file, baseAddress + getFileByIdMethod);

            return Ok(result);
        }
    }
}