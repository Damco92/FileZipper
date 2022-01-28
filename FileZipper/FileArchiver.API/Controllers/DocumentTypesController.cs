using FileArchiver.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileArchiver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentTypesController : Controller
    {
        private IDocumentTypesService _documentTypeService;
        public DocumentTypesController(IDocumentTypesService documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }
        [HttpGet("getAllDocumentTypes")]
        public IActionResult GetAllDocumentTypes()
        {
            var documentTypes = _documentTypeService.GetAllDocumentTypes();
            return Ok(documentTypes);
        }

        [HttpGet("getMaskForDocument/{documentName}")]
        public IActionResult GetMaskForDocument([FromRoute] string documentName)
        {
            var result = _documentTypeService.GetDocumentMaskByDocumentName(documentName);
            return Ok(result);
        }

        [HttpGet("getDocumentTypeByDocumentName/{documentName}")]
        public IActionResult GetDocumentTypeByDocumentName([FromRoute] string documentName)
        {
            var documentType = _documentTypeService.GetDocumentTypeByDocumentName(documentName);
            return Ok(documentType);
        }

        [HttpGet("getDocumentTypeById/{Id}")]
        public IActionResult GetDocumentTypeById([FromRoute] int Id)
        {
            var documentType = _documentTypeService.GetDocumentTypeById(Id);
            return Ok(documentType);
        }
    }
}
