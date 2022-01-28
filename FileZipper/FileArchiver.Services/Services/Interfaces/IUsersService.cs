using FileArchiver.Common.ViewModels;
using System.Collections.Generic;

namespace FileArchiver.Services.Interfaces
{
    public interface IUsersService
    {
        IEnumerable<UserViewModel> GetUsers();
        UserViewModel GetUser(string username, string password);
        UserViewModel GetUserByUsername(string username);
        UserViewModel GetUserByUserId(int userId);
        string GetUsersPasswordByUsername(string username);
        void UpdateUsersName(string username, string newName, string newPassword, string confirmPassword);
        void InsertUser(RegisterNewUserViewModel user,UserViewModel loggedUser);
    }
}
