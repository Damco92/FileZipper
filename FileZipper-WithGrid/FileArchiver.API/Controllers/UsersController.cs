using FileArchiver.Common.ViewModels;
using FileArchiver.Domain;
using FileArchiver.Services.Services.Interfaces;
using FileArchiverCommmon.Helpers;
using FileArchiverCommon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices.AccountManagement;

namespace FileArchiver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static CredentialsViewModel _credentials;
        private IUsersService _userService;
        private IFilesService _fileService;
        
        public UsersController(IUsersService userService, IFilesService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }
        [HttpGet("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        [HttpPost("login")]
        public IActionResult Index([FromBody] CredentialsViewModel userCredentials)
        {
            _credentials = userCredentials;
            var userVM = _userService.GetUser(userCredentials.Username, userCredentials.Password);
            if (userVM.Username == null)
            {
                return NotFound("The username or password is wrong");
            }
            return Ok(userVM);
        }
        [HttpGet("GetUsersEncryptedPassword/{username}")]
        public IActionResult GetUsersEncryptedPassword([FromRoute] string username)
        {
            var password = _userService.GetUsersPasswordByUsername(username);
            return Ok(password);
        }


        [HttpPost("updateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUsersViewModel updateUsersViewModel)
        {
            if (updateUsersViewModel.ZipPassword == null && updateUsersViewModel.ConfirmZipPassword == null)
            {
                updateUsersViewModel.ZipPassword = string.Empty;
                updateUsersViewModel.ConfirmZipPassword = string.Empty;
            }

            if (updateUsersViewModel.UsersNewName == null)
                updateUsersViewModel.UsersNewName = string.Empty;

            if(updateUsersViewModel.UsersNewName == string.Empty && (string.IsNullOrEmpty(updateUsersViewModel.ZipPassword) || string.IsNullOrEmpty(updateUsersViewModel.ConfirmZipPassword)))
                return Ok("Please fill in the two input fields if you want to change your password!");

            if (
                !string.IsNullOrEmpty(updateUsersViewModel.ZipPassword)
                && !string.IsNullOrEmpty(updateUsersViewModel.ConfirmZipPassword) &&
                !HashingHelper.IsRegexForDocumentPasswordMatch(updateUsersViewModel.ZipPassword)
                )
            {
                return Ok("Password must contain at least one digit and one special character and be at least 8 characters long");
            }

            if (
                !string.IsNullOrEmpty(updateUsersViewModel.ZipPassword) 
                && !string.IsNullOrEmpty(updateUsersViewModel.ConfirmZipPassword)
                && !HashingHelper.DoPasswordsMatch(updateUsersViewModel.ZipPassword, updateUsersViewModel.ConfirmZipPassword)
                )
            {
                return Ok("Passwords do not match");
            }
            _userService.UpdateUsersName(_credentials.Username, updateUsersViewModel.UsersNewName, updateUsersViewModel.ZipPassword, _credentials.Password);
            return Ok("Success");
        }

        [HttpGet("getUserByUsername/{username}")]
        public IActionResult GetUserByUsername([FromRoute] string username)
        {
            var user = _userService.GetUserByUsername(username);
            return Ok(user);
        }

        [HttpGet("getFileById/{fileId}")]
        public IActionResult GetFileById([FromRoute] int fileId)
        {
            var fileVM = _fileService.GetFileById(fileId, _credentials.Password);
            _fileService.UpdateFileToDownloaded(fileId);
            return Ok(fileVM);
        }

        [HttpPost("updateIsConfirmed")]
        public IActionResult UpdateIsConfirmed([FromBody] FileViewModel file)
        {
            var fileVM = _fileService.GetFileById(file.FileId, _credentials.Password);
            _fileService.UpdateStatusToConfirmed(file.FileId);
            return Ok(fileVM);
        }

        [HttpPost("registerUser")]
        public IActionResult RegisterUser([FromBody] RegisterNewUserViewModel user)
        {
            bool userExists = false;
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "QUIPU"))
            {
                using (var foundUser = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, user.Username))
                {
                  if (foundUser != null)
                      userExists = true;
                }
            }
            if (!userExists)
                return Ok("Such username does not exist on domain");

            var userVM = _userService.GetUser(_credentials.Username, _credentials.Password);
            try
            {
                _userService.InsertUser(user, userVM);
            }
            catch (System.Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok("User added");
        }

        [HttpGet("getUserById/{userId}")]
        public IActionResult GetUserById([FromRoute] int userId)
        {
            var userVM = _userService.GetUserByUserId(userId);
            return Ok(userVM);
        }
    }
}
