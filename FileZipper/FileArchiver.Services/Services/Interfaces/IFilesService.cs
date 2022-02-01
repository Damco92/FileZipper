using FileArchiver.Common.ViewModels;
using System.Collections.Generic;

namespace FileArchiver.Services.Interfaces
{
    public interface IFilesService
    {
        IEnumerable<FileViewModel> GetAllFiles();
        void UploadFile(FileViewModel fileVM);
        FileViewModel GetFileByFileNameAndUsername(string fileName, string username);
        FileViewModel GetFileById(int fileId, string domainPassword, UserViewModel user);
        List<FileViewModel> GetAllFilesByUserId(int userId);
        void UpdateFileToDownloaded(int fileId);
        void DeleteFile(FileViewModel file);
    }
}
