using FileArchiverCommon.ViewModels;
using System.Collections.Generic;

namespace FileArchiver.Services.Services.Interfaces
{
    public interface IDocumentTypesService
    {
        List<DocumentTypeViewModel> GetAllDocumentTypes();
        string GetDocumentMaskByDocumentName(string documentName);
        DocumentTypeViewModel GetDocumentTypeByDocumentName(string documentName);
    }
}
