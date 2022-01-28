using FileArchiver.Common.ViewModels;
using System.Collections.Generic;

namespace FileArchiver.Services.Interfaces
{
    public interface IDocumentTypesService
    {
        List<DocumentTypeViewModel> GetAllDocumentTypes();
        string GetDocumentMaskByDocumentName(string documentName);
        DocumentTypeViewModel GetDocumentTypeByDocumentName(string documentName);
        DocumentTypeViewModel GetDocumentTypeById(int id);
    }
}
