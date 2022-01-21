using FileArchiver.Domain.Models;
using FileArchiver.Domain.Repositories.Interfaces;
using FileArchiver.Services.Services.Interfaces;
using FileArchiverCommmon.Helpers;
using FileArchiverCommon.ViewModels;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileArchiver.Services.Services.Implementation
{
    public class UserService : IUsersService
    {
        private IUsersRepository _userRepo;
        private IFilesRepository _filesRepository;
        private static FastZip fastZip = new FastZip();
        public UserService(IUsersRepository userRepository, IFilesRepository filesRepository)
        {
            _userRepo = userRepository;
            _filesRepository = filesRepository;
        }


        public UserViewModel GetUser(string username, string password)
        {
            User user = _userRepo.GetUserByLoginCredentials(username, password);
            IEnumerable<User> users = _userRepo.GetAllUsers();
            if (user == null)
                return new UserViewModel();

            User creator = _userRepo.GetCreatorByUserId(user.Creator);
            UserViewModel creatorVM = new UserViewModel()
            {
                Id = creator.Id,
                Username = creator.Username,
                Name = creator.Name,
                IsAdmin = creator.IsAdmin,
                Files = new List<FileViewModel>()
            };
            if (user.IsAdmin)
            {
                return new UserViewModel()
                {
                    Username = user.Username,
                    IsAdmin = user.IsAdmin,
                    Name = user.Name,
                    Files = 
                    user.Files.Select(x => new FileViewModel 
                    {
                        Created = x.Created, 
                        FileId = x.Id, 
                        Creator = creatorVM,
                        FileName = x.FileName,
                        IsDownloaded = x.IsDownloaded
                    }).ToList(),
                    Id = user.Id,
                    CreatedUsers = 
                    _userRepo.GetAllUsers().Where(u => u.Creator == user.Id)
                    .Select(uvm => new UserViewModel { Id = uvm.Id,Name = uvm.Name,IsAdmin = uvm.IsAdmin,Username = uvm.Username})
                };
            }
            else
            {
                return new UserViewModel()
                {
                    Username = user.Username,
                    Name = user.Name,
                    IsAdmin = user.IsAdmin,
                    Files =
                    user.Files.Select(x => new FileViewModel
                    {
                        Created = x.Created,
                        FileId = x.Id,
                        Creator = creatorVM,
                        FileName = x.FileName,
                        IsDownloaded = x.IsDownloaded
                    }).ToList(),
                    Id = user.Id,
                    CreatedUsers = new List<UserViewModel>()
                };
            }
            
        }

        public string GetUsersPasswordByUsername(string username)
        {
            var user = _userRepo.GetUserByUsername(username);
            var pass = user.ZipPassword;
            return pass;
        }

        public void UpdateUsersPassword(string username, string documentPassword, string domainPassword)
        {
            var user = _userRepo.GetUserByUsername(username);
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(documentPassword);
            var keyByteArray = HashingHelper.GetDomainPasswordByteArray(domainPassword);
            byte[] bytesEncrypted = HashingHelper.AES_Encrypt(bytesToBeEncrypted, keyByteArray);
            user.ZipPassword = Convert.ToBase64String(bytesEncrypted);
            _userRepo.UpdateUser(user);
        }

        public void UpdateUsersName(string username, string newName, string zipPassword, string domainPassword)
        {
            var user = _userRepo.GetUserByUsername(username);
            user.Name = string.IsNullOrEmpty(newName) ? user.Name : newName;

            if (!string.IsNullOrEmpty(zipPassword))
            {
                byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(zipPassword);
                var keyByteArray = HashingHelper.GetDomainPasswordByteArray(domainPassword);
                byte[] bytesEncrypted = HashingHelper.AES_Encrypt(bytesToBeEncrypted, keyByteArray);
                user.ZipPassword = Convert.ToBase64String(bytesEncrypted);
            }
            else
            {
                user.ZipPassword = user.ZipPassword;
            }
            _userRepo.UpdateUser(user);
        }

        public UserViewModel GetUserByUsername(string username)
        {
            var user =  _userRepo.GetUserByUsername(username);
            var creator = _userRepo.GetCreatorByUserId(user.Creator);
            UserViewModel creatorVM = new UserViewModel()
            {
                Id = creator.Id,
                Username = creator.Username,
                Name = creator.Name,
                IsAdmin = creator.IsAdmin,
                Files = new List<FileViewModel>()
            };
            UserViewModel result = new UserViewModel();
            result.Id = user.Id;
            result.Name = user.Name;
            result.IsAdmin = user.IsAdmin;
            result.Username = user.Username;
            result.Files =
                    user.Files.Select(x => new FileViewModel
                    {
                        Created = x.Created,
                        FileId = x.Id,
                        Creator = creatorVM,
                        FileName = x.FileName,
                        IsDownloaded = x.IsDownloaded
                    }).ToList();
            return result;
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = _userRepo.GetAllUsers();
            return users.Select(u => new UserViewModel(u.Id, u.Username, u.Name, u.IsAdmin)
            {
                Files =
                _filesRepository.GetAllFiles().Where(x => x.UserId == u.Id).Select(f => new FileViewModel(f.Id, f.IsDownloaded, f.FileName, f.Created)).ToList()
            });
        }
    }
}

