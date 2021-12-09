using FileArchiver.Domain.Context;
using FileArchiver.Domain.Models;
using FileArchiver.Domain.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FileArchiver.Domain.Repositories
{
    public class DocumentTypesRepository : IDocumentTypesRepository
    {
        private FilesArchiveDBContext _dbContext;
        public DocumentTypesRepository(FilesArchiveDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<DocumentTypes> GetAllDocumentTypes()
        {
            return _dbContext.DocumentTypes;
        }

        public DocumentTypes GetDocumentTypeByDocumentName(string documentName)
        {
            return _dbContext.DocumentTypes.FirstOrDefault(x => x.DocumentName == documentName);
        }

        public DocumentTypes GetDocumentTypeById(int id)
        {
            return _dbContext.DocumentTypes.FirstOrDefault(x => x.Id == id);
        }
    }
}
