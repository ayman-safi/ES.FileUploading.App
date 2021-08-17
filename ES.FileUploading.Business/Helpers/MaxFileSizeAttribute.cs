using ES.FileUploading.Business.Abstract;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ES.FileUploading.Business.Helpers
{
    /// <summary>
    /// source https://stackoverflow.com/questions/56588900/how-to-validate-uploaded-file-in-asp-net-core
    /// </summary>
    public class MaxFileSizeAttribute : ValidationAttribute
    {
         private long _maxFileSize = 0;

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var _filesOperationsBusiness = (IFilesOperationsBusiness)validationContext.GetService(typeof(IFilesOperationsBusiness));

                var extension = Path.GetExtension(file.FileName);

                _maxFileSize = _filesOperationsBusiness.GetExtenstionByName( extension)?.MaxAllowedSize ?? 0;
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is { _maxFileSize} bytes.";
        }
    }
}
