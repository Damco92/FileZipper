using System.Collections.Generic;

namespace FileArchiverCommon.ViewModels
{
    public class UploadFileViewModel
    {
        public string Username { get; set; }
        public List<UserViewModel> Users { get; set; }
        public string DocumentName { get; set; }
        public List<DocumentTypeViewModel> Documents { get; set; }
        public FileViewModel File { get; set; }
    }
}
