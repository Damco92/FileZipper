using FileArchiver.Domain.Models;
using System.Collections.Generic;

namespace FileArchiver.Domain.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Users GetUserById(int id);
        IEnumerable<Users> GetAllUsers();
        Users GetUserByLoginCredentials(string username, string password);
        Users GetUserByUsername(string username);
        void UpdateUser(Users user);
        Users GetCreatorByUserId(int userId);
        void InsertUser(Users user);
    }
}
