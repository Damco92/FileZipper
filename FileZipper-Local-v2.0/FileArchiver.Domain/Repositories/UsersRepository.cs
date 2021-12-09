using FileArchiver.Domain.Context;
using FileArchiver.Domain.Models;
using FileArchiver.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FileArchiver.Domain.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private FilesArchiveDBContext _dbContext;
        public UsersRepository(FilesArchiveDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users;
        }

        public User GetCreatorByUserId(int creatorId)
        {

            return _dbContext.Users.FirstOrDefault(x => x.Id == creatorId);
        }

        public User GetUserById(int id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public  User GetUserByLoginCredentials(string username, string password)
        {
            var user =  _dbContext.Users.Include(x => x.Files).FirstOrDefault(x => x.Username == username);
            var autheticationResult = AuthenticationHelper.AuthenticateDomain(username, password);
            if (!autheticationResult.IsValidLogIn)
            {
                return null;
            }
            return user;
        }

        public User GetUserByUsername(string username)
        {
            var user = _dbContext.Users.Include(x => x.Files).FirstOrDefault(x => x.Username == username);
            return user;
        }

        public void InsertUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }
    }
}
