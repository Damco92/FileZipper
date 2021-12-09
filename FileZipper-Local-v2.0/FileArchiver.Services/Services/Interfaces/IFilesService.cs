using FileArchiverCommon.ViewModels;
using System.Collections.Generic;

namespace FileArchiver.Services.Services.Interfaces
{
    public interface IFilesService
    {
        void UploadFile(FileViewModel fileVM);
        FileViewModel GetFileByFileNameAndUsername(string fileName, string username);
        FileViewModel GetFileById(int fileId, string domainPassword);
        List<FileViewModel> GetAllFilesByUsername(string username);
        void UpdateFileToDownloaded(int fileId);
    }
}
