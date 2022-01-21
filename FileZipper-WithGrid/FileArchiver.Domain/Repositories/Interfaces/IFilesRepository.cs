using FileArchiver.Domain.Models;
using System.Collections.Generic;

namespace FileArchiver.Domain.Repositories.Interfaces
{
    public interface IFilesRepository
    {
        IEnumerable<Files> GetAllFiles();
        IEnumerable<Files> GetAllFilesByUsername(string username);
        IEnumerable<Files> GetAllFilesByUserId(int userId);
        Files GetFileById(int fileId);
        Files GetUserByFileId(int fileId);
        Files GetFileByFileNameAndUsername(string fileName, string username);
        void InsertFile(Files file);
        void InsertFileToUser(Files file, string username);
        void UpdateFile(Files file);
    }
}
