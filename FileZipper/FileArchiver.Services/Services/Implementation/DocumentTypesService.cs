using FileArchiver.Domain.Repositories.Interfaces;
using FileArchiver.Services.Interfaces;
using FileArchiver.Common.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace FileArchiver.Services.Implementation
{
    public class DocumentTypesService : IDocumentTypesService
    {
        private IDocumentTypesRepository _documentTypeRepository;
        public DocumentTypesService(IDocumentTypesRepository documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }
        public List<DocumentTypeViewModel> GetAllDocumentTypes()
        {
            var documentTypes = _documentTypeRepository.GetAllDocumentTypes();
            return documentTypes.Select(x => new DocumentTypeViewModel(x.Id, x.DocumentName, x.FileNameMask, x.IsActive)).ToList();
        }

        public string GetDocumentMaskByDocumentName(string documentName)
        {
            var documentType = _documentTypeRepository.GetAllDocumentTypes().FirstOrDefault(x => x.DocumentName == documentName);
            if (documentType == null)
                return string.Empty;

            return documentType.FileNameMask;
        }

        public DocumentTypeViewModel GetDocumentTypeByDocumentName(string documentName) 
        {
            var documentTypeVM = new DocumentTypeViewModel();
            var documentType = _documentTypeRepository.GetDocumentTypeByDocumentName(documentName);
            documentTypeVM.Id = documentType.Id;
            documentTypeVM.DocumentName = documentType.DocumentName;
            documentTypeVM.FileNameMask = documentType.FileNameMask;
            documentTypeVM.IsActive = documentTypeVM.IsActive;
            return documentTypeVM;
        }
        public DocumentTypeViewModel GetDocumentTypeById(int id)
        {
            var documentTypeVM = new DocumentTypeViewModel();
            var documentType = _documentTypeRepository.GetDocumentTypeById(id);
            documentTypeVM.Id = documentType.Id;
            documentTypeVM.DocumentName = documentType.DocumentName;
            documentTypeVM.FileNameMask = documentType.FileNameMask;
            documentTypeVM.IsActive = documentTypeVM.IsActive;
            return documentTypeVM;
        }
    }
}
