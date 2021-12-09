﻿using System;
using System.Collections.Generic;

namespace FileArchiver.Domain.Models
{
    public partial class DocumentTypes
    {
        public DocumentTypes()
        {
            Files = new HashSet<File>();
        }

        public int Id { get; set; }
        public string DocumentName { get; set; }
        public string FileNameMask { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
