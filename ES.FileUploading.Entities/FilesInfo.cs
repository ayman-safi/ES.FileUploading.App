
using System;
using System.ComponentModel.DataAnnotations;

namespace ES.FileUploading.Entities
{
    public class FilesInfo 
    {
        public long  Id { get; set; }
        public int FileExtensionId { get; set; }
        public FileExtension FileExtension { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public double FileSize { get; set; }
        public DateTime UploadingDate => DateTime.Now;

    }
}
