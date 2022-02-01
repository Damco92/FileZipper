using FileArchiver.Domain.Context;
using FileArchiver.Domain.Models;
using FileArchiver.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FileArchiver.Domain.Repositories.Implementation
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
            return _dbContext.DocumentTypes.Include(x => x.Files).FirstOrDefault(x => x.DocumentName == documentName);
        }

        public DocumentTypes GetDocumentTypeById(int id)
        {
            return _dbContext.DocumentTypes.Include(x => x.Files).FirstOrDefault(x => x.Id == id);
        }
    }
}
