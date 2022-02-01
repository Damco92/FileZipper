using System;

namespace FileArchiver.Domain.Models
{
    public partial class Files
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DocumentTypeId { get; set; }
        public bool? IsDownloaded { get; set; }
        public byte[] Data { get; set; }
        public DateTime Created { get; set; }
        public int Creator { get; set; }
        public string FileName { get; set; }
        public string UploadNote { get; set; }

        public virtual DocumentTypes DocumentType { get; set; }
        public virtual Users User { get; set; }
    }
}
