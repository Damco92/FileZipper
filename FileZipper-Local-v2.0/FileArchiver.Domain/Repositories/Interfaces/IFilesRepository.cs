using FileArchiver.Domain.Models;
using System.Collections.Generic;

namespace FileArchiver.Domain.Repositories.Interfaces
{
    public interface IFilesRepository
    {
        IEnumerable<File> GetAllFiles();
        IEnumerable<File> GetAllFilesByUsername(string username);
        File GetFileById(int fileId);
        File GetFileByFileNameAndUsername(string fileName, string username);
        void InsertFile(File file);
        void InsertFileToUser(File file, string username);
        void UpdateFile(File file);
    }
}
