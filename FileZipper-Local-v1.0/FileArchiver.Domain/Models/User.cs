using System;
using System.Collections.Generic;

namespace FileArchiver.Domain.Models
{
    public partial class User
    {
        public User()
        {
            Files = new HashSet<File>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string ZipPassword { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public int Creator { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
