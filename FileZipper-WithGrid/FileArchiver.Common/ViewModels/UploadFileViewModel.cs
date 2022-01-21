using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FileArchiverCommon.ViewModels
{
    public class UploadFileViewModel
    {
        public UploadFileViewModel() { }
        public UploadFileViewModel(string name, int docTypeId,string docName)
        {
            Username = name;
            DocumentTypeId = docTypeId;
            DocumentName = docName;
        }
        public FileViewModel File { get; set; }
        public string Username { get; set; }
        public List<UserViewModel> Users { get; set; }
        public UserViewModel User { get; set; }
        public DocumentTypeViewModel DocumentType { get; set; }
        public List<IFormFile> FormFile { get; set; }
        public string DocumentName { get; set; }
        public int DocumentTypeId { get; set; }
        public List<DocumentTypeViewModel> Documents { get; set; }
        public FileModel singleFile { get; set; }
        public List<FileViewModel> FilesToBeUploaded { get; set; }
    }
}
