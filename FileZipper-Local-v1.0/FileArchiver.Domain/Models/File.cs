using System;

namespace FileArchiver.Domain.Models
{
    public partial class File
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DocumentTypeId { get; set; }
        public bool? IsDownloaded { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public DateTime Created { get; set; }
        public int Creator { get; set; }

        public virtual DocumentTypes DocumentType { get; set; }
        public virtual User User { get; set; }
    }
}
