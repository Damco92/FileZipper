using FileArchiver.Services.Services.Interfaces;
using FileArchiverCommon.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("getAllFiles/{username}")]
        public IActionResult GetAllFilesForUser([FromRoute] string username)
        {
            var files = _filesService.GetAllFilesByUsername(username);
            return Ok(files);
        }

        [HttpGet("getFileByFileName/{username}/{fileName}")]
        public IActionResult GetFileById([FromRoute]string username ,[FromRoute] string fileName)
        {
            var fileVM = _filesService.GetFileByFileNameAndUsername(username, fileName);
            return Ok(fileVM);
        }

        [HttpPost("uploadFile")]
        public IActionResult UploadFile([FromForm] FileViewModel fileViewModel)
        {
            fileViewModel.Creator = _usersService.GetUserByUsername(fileViewModel.CreatorUsername);
            fileViewModel.DocumentType = _documentTypeService.GetDocumentTypeByDocumentName(fileViewModel.DocumentName);
            fileViewModel.UserVM = _usersService.GetUserByUsername(fileViewModel.UserThatAFileIsUploadedToUsername);
            fileViewModel.IsDownloaded = false;
            _filesService.UploadFile(fileViewModel);
            return Ok("Success");
        }
    }
}
