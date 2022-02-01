using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace FileArchiver.Common.ViewModels
{
    public class FileViewModel
    {
        public FileViewModel()  {}
        public FileViewModel(int id, bool? isDownloaded, string fileName, DateTime created, int userId, int docType)
        {
            FileId = id;
            IsDownloaded = isDownloaded;
            FileName = fileName;
            Created = created;
            UserId = userId;
            DocumentTypeId = docType;
        }
        public int FileId { get; set; }
        public UserViewModel UserVM { get; set; }
        public string UserThatAFileIsUploadedToUsername { get; set; }
        public bool? IsDownloaded { get; set; }
        public string FileName { get; set; }
        public DateTime  Created { get; set; }
        public int CreatorId { get; set; }
        public UserViewModel User { get; set; }
        public int UserId { get; set; }
        public IFormFile FileData { get; set; }
        public DocumentTypeViewModel DocumentType { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
        public byte[] FileByteData { get; set; } 
        public List<FileModel> FileDataByteArray { get; set; }
        public string CreatorUsername { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class FileModel
    {
        public byte[] Data { get; set; }
    }
}
