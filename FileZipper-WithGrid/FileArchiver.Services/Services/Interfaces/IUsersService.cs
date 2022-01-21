using FileArchiver.Common.ViewModels;
using FileArchiverCommon.ViewModels;
using System.Collections.Generic;

namespace FileArchiver.Services.Services.Interfaces
{
    public interface IUsersService
    {
        IEnumerable<UserViewModel> GetUsers();
        UserViewModel GetUser(string username, string password);
        UserViewModel GetUserByUsername(string username);
        UserViewModel GetUserByUserId(int userId);
        UserViewModel GetUserIdByUsername(string username);
        FileViewModel GetUserByFileId(int fileId);
        string GetUsersPasswordByUsername(string username);
        void UpdateUsersPassword(string username,string documentPassword, string domainpass);
        void UpdateUsersName(string username, string newName, string newPassword, string confirmPassword);
        void InsertUser(RegisterNewUserViewModel user,UserViewModel loggedUser);
    }
}
