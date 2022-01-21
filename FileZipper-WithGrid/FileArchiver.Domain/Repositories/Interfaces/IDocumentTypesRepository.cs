using FileArchiver.Domain.Models;
using System.Collections.Generic;

namespace FileArchiver.Domain.Repositories.Interfaces
{
    public interface IDocumentTypesRepository
    {
        DocumentTypes GetDocumentTypeById(int id);
        IEnumerable<DocumentTypes> GetAllDocumentTypes();
        DocumentTypes GetDocumentTypeByDocumentName(string documentName);
    }
}
