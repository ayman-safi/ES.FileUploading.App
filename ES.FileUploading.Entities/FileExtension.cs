
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ES.FileUploading.Entities
{
    public class FileExtension
    {
        public int Id { get; set; }
        public string ExtensionName { get; set; }
        public long MaxAllowedSize { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public ICollection<FilesInfo> FileInfos { get; set; }
        public FileExtension()
        {
            FileInfos = new Collection<FilesInfo>();
        }
    }
}
