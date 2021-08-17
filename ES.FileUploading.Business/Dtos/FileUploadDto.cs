using ES.FileUploading.Business.Abstract;
using ES.FileUploading.Business.Concrete;
using ES.FileUploading.Business.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ES.FileUploading.Business.Dtos
{
    public class FileUploadDto 
    {
        public string ExtensionName => Path.GetExtension(File?.FileName);

        [AllowedExtensions]
        [MaxFileSize]
        public IFormFile File { get; set; }

        public string FileName => File?.FileName;
        public long FileSize => File?.Length ?? 0;
    }
    public class fileInfoDto : PaginationsParams
    {
        public string ExtensionName { get; set; }
    }
}
