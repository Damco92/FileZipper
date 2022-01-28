using System.Collections.Generic;

namespace FileArchiver.Common.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel() {}
        public UserViewModel(int id, string username, string name, bool isAdmin)
        {
            Id = id;
            Username = username;
            Name = name;
            IsAdmin = isAdmin;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public List<FileViewModel> Files { get; set; }
        public IEnumerable<UserViewModel> CreatedUsers { get; set; }
    }
}
