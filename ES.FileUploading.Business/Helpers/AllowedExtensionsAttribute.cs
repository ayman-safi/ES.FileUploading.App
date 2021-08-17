using ES.FileUploading.Business.Abstract;
using ES.FileUploading.Business.Dtos;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ES.FileUploading.Business.Helpers
{

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (IFilesOperationsBusiness)validationContext.GetService(typeof(IFilesOperationsBusiness));
            var file = value as IFormFile;
            var extension = Path.GetExtension(file?.FileName);
            if (file != null)
            {
                var extensions = service.GetAllExtenstion().Result.Select(x=>x.ExtensionName).ToArray();

                if (!extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage($"this File extension{extension} is not allowed!"));
                }
                return ValidationResult.Success;

            }
            return new ValidationResult(GetErrorMessage($"opps ! No file sumbitted!"));

        }

        public string GetErrorMessage(string message)
        {
            return message;
        }
    }
}
