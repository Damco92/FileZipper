namespace FileArchiver.Common.ViewModels
{
    public class DocumentTypeViewModel
    {
        public DocumentTypeViewModel() {}
        public DocumentTypeViewModel(int id, string documentName, string fileMask, bool isActive)
        {
            Id = id;
            DocumentName = documentName;
            FileNameMask = fileMask;
            IsActive = isActive;
        }
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public string FileNameMask { get; set; }
        public bool IsActive { get; set; }
    }
}
