using FileArchiver.Domain.Context;
using FileArchiver.Domain.Models;
using FileArchiver.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileArchiver.Domain.Repositories.Implementation
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
        public void DeleteFile(Files file)
        {
            try
            {
                _dbContext.Files.Remove(file);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("File can not be deleted");
            }
            _dbContext.SaveChanges();
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

        public void UpdateFile(Files file)
        {
            _dbContext.Files.Update(file);
            _dbContext.SaveChanges();
        }
    }
}
