using FileArchiver.Domain.Context;
using FileArchiver.Domain.Models;
using FileArchiver.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FileArchiver.Domain.Repositories
{
    public class FilesRepositroy : IFilesRepository
    {
        private FilesArchiveDBContext _dbContext;
        public FilesRepositroy(FilesArchiveDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Files> GetAllFiles()
        {
            return _dbContext.Files;
        }
               
        public Files GetFileById(int fileId)
        {
            return _dbContext.Files.FirstOrDefault(x => x.Id == fileId);
        }

        public Files GetUserByFileId(int fileId)
        {
            var user = _dbContext.Files.Include(u => u.User.Username).Where(x => x.Id == fileId);
            return (Files)user;
        }
        public IEnumerable<Files> GetAllFilesByUsername(string username)
        {
            return _dbContext.Files.Where(x => x.User.Username == username);
        }
        public IEnumerable<Files> GetAllFilesByUserId(int userId)
        {
            return _dbContext.Files.Where(x => x.UserId == userId);
        }
        public Files GetFileByFileNameAndUsername(string username, string fileName)
        {
            return _dbContext.Files.Include(x => x.User).Include(x => x.DocumentType).FirstOrDefault(f => f.FileName == fileName && f.User.Username == username);
        }

        public void InsertFile(Files file)
        {
            _dbContext.Files.Add(file);
            _dbContext.SaveChanges();
        }

        public void InsertFileToUser(Files file, string username)
        {
            var user = _dbContext.Users.Include(u => u.Files).FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                throw new System.Exception();
            }
            user.Files.Add(file);
            _dbContext.Update(user);
            _dbContext.SaveChanges();
        }

        public void UpdateFile(Files file)
        {
            _dbContext.Files.Update(file);
            _dbContext.SaveChanges();
        }
    }
}
