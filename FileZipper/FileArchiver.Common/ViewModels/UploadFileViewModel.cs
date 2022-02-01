using System.Collections.Generic;

namespace FileArchiver.Common.ViewModels
{
    public class UploadFileViewModel
    {
        public UploadFileViewModel() { }
        public FileViewModel File { get; set; }
        public string Username { get; set; }
        public List<UserViewModel> Users { get; set; }
        public string DocumentName { get; set; }
        public int DocumentTypeId { get; set; }
        public List<DocumentTypeViewModel> Documents { get; set; }
    }
}
