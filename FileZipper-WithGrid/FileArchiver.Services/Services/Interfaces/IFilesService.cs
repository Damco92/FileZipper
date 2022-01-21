using FileArchiverCommon.ViewModels;
using System.Collections.Generic;

namespace FileArchiver.Services.Services.Interfaces
{
    public interface IFilesService
    {
        IEnumerable<FileViewModel> GetAllFiles();
        void UploadFile(FileViewModel fileVM);
        FileViewModel GetFileByFileNameAndUsername(string fileName, string username);
        FileViewModel GetFileById(int fileId, string domainPassword);
        List<FileViewModel> GetAllFilesByUsername(string username);
        List<FileViewModel> GetAllFilesByUserId(int userId);
        void UpdateFileToDownloaded(int fileId);
        void UpdateStatusToConfirmed(int fileId);
        void UploadMultipleFiles(List<FileViewModel> files);
    }
}
