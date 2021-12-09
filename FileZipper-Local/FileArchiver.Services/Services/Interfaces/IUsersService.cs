using FileArchiverCommon.ViewModels;
using System.Collections.Generic;

namespace FileArchiver.Services.Services.Interfaces
{
    public interface IUsersService
    {
        IEnumerable<UserViewModel> GetUsers();
        UserViewModel GetUser(string username, string password);
        UserViewModel GetUserByUsername(string username);
        string GetUsersPasswordByUsername(string username);
        void UpdateUsersPassword(string username,string documentPassword, string domainpass);
        void UpdateUsersName(string username, string newName, string newPassword, string confirmPassword);
    }
}
