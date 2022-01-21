using System;
using Microsoft.AspNetCore.Http;
namespace FileArchiverCommon.ViewModels
{
    public class FileViewModel
    {
        public FileViewModel()  {}
        public FileViewModel(int id, bool? isDownloaded, string fileName, DateTime created)
        {
            FileId = id;
            IsDownloaded = isDownloaded;
            FileName = fileName;
            Created = created;
        }
        public int FileId { get; set; }
        public UserViewModel UserVM { get; set; }
        public string UserThatAFileIsUploadedToUsername { get; set; }
        public bool? IsDownloaded { get; set; }
        public string FileName { get; set; }
        public DateTime  Created { get; set; }
        public UserViewModel Creator { get; set; }
        public IFormFile FileData { get; set; }
        public DocumentTypeViewModel DocumentType { get; set; }
        public string DocumentName { get; set; }
        public byte[] FileDataByteArray { get; set; }
        public string CreatorUsername { get; set; }
    }
}
