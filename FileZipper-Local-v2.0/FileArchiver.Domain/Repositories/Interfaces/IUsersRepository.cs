using FileArchiver.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileArchiver.Domain.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
        User GetUserByLoginCredentials(string username, string password);
        User GetUserByUsername(string username);
        void UpdateUser(User user);
        User GetCreatorByUserId(int userId);
        void InsertUser(User user);
    }
}
