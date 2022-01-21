using FileArchiver.Services.Services.Interfaces;
using FileArchiverCommon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FileArchiver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private IFilesService _filesService;
        private IUsersService _usersService;
        private IDocumentTypesService _documentTypeService;
        public FilesController(IFilesService filesService, IUsersService usersService, IDocumentTypesService documentTypesService)
        {
            _filesService = filesService;
            _usersService = usersService;
            _documentTypeService = documentTypesService;
        }
        [HttpGet("getAllFiles")]
        public IActionResult GetAllFiles()
        {
            var files = _filesService.GetAllFiles();
            return Ok(files);
        }

        [HttpGet("getAllFilesByUsername/{username}")]
        public IActionResult GetAllFilesByUsername([FromRoute] string username)
        {
            List<FileViewModel> result = null;
            var iamAdmin = _usersService.GetUserByUsername(username);
            if (iamAdmin != null && iamAdmin.IsAdmin)
                result = _filesService.GetAllFiles().Cast<FileViewModel>().ToList();
            else
                result = _filesService.GetAllFilesByUserId(iamAdmin.Id);
            foreach (var file in result)
            {
                file.UserVM = _usersService.GetUserByUserId(file.UserId);
                file.DocumentType = _documentTypeService.GetDocumentTypeById(file.DocumentTypeId);
            }
            return Ok(result);
        }
        

        [HttpGet("getFileByFileName/{username}/{fileName}")]
        public IActionResult GetFileByFileNameAndUsername([FromRoute]string username ,[FromRoute] string fileName)
        {
            var fileVM = _filesService.GetFileByFileNameAndUsername(username, fileName);
            return Ok(fileVM);
        }

        [HttpPost("uploadFile")]
        public IActionResult UploadFile([FromForm] FileViewModel fileViewModel)
        {
            fileViewModel.User = _usersService.GetUserByUserId(fileViewModel.UserId);
            fileViewModel.DocumentType = _documentTypeService.GetDocumentTypeByDocumentName(fileViewModel.DocumentName);
            fileViewModel.UserVM = _usersService.GetUserByUsername(fileViewModel.UserThatAFileIsUploadedToUsername);
            fileViewModel.IsDownloaded = false;
            fileViewModel.IsConfirmed = false;
            _filesService.UploadFile(fileViewModel);

            return Ok("Success");
        }

        [HttpPost("uploadMultipleFiles")]
        public IActionResult UploadMultipleFiles([FromBody] UploadFileViewModel uploadFile)
        {
            _filesService.UploadMultipleFiles(uploadFile.FilesToBeUploaded);
            return Ok("Here");
        }
    }
}
