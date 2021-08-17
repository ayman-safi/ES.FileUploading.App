using ES.FileUploading.Business.Abstract;
using ES.FileUploading.Business.Base;
using ES.FileUploading.Business.Dtos;
using ES.FileUploading.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ES.FileUploading.API.Controllers
{
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private IFilesOperationsBusiness _filesOperationsBusiness{ get; set; }
        public FileUploadController(IFilesOperationsBusiness fileOperationsBusiness)
        {
            _filesOperationsBusiness = fileOperationsBusiness;
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadingFile([FromForm]FileUploadDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                         .Where(y => y.Count > 0)
                         .ToList();
                return Ok(errors);
            }
            var input = new FilesInfo();
            input.FileName = model.FileName;
            input.FileSize = model.FileSize;
            input.FileExtensionId = _filesOperationsBusiness.GetExtenstionByName(model.ExtensionName)?.Id ?? 0;
            
            if (model.File != null && input.FileExtensionId !=0)
            {
                input.FileUrl = UploadFile(model.File);
            }
            if (!string.IsNullOrWhiteSpace(input.FileUrl))
            {
                await _filesOperationsBusiness.UploadFile(input);
                return Ok();

            }
            return BadRequest("something wrong goes while uploading Video!!! please try again ");
        }

        [HttpPost("CreateExtension")]
        public async Task<IActionResult> CreateExtension([FromBody]FileExtension model)
        {
            try
            {

                string pattern = @"^(.(([a-z]|[A-Z]){2,4}))$"; ;
               
                if (Regex.IsMatch(model.ExtensionName, pattern))
                {
                    if (!_filesOperationsBusiness.GetAllExtenstion().Result.Any(x => x.ExtensionName == model.ExtensionName))
                    {
                        await _filesOperationsBusiness.CreateExtension(model);
                    }

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost("GetAllExtensionsHasFiles")]
        public async Task<ActionResult> GetAllExtensionsHasFiles([FromBody] PaginationsParams model)
        {
            try
            {
                var list = await _filesOperationsBusiness.GetAllExtensionsHasFiles(model);
                Response.AddPagination(list.CurrentPage, list.PageSize, list.TotalCount, list.TotalPages);

                return Ok(list);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }  
        [HttpGet("GetAllExtenstion")]
        public async Task<ActionResult> GetAllExtensions()
        {
            try
            {
                var list = await _filesOperationsBusiness.GetAllExtenstion();

                return Ok(list);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        } 
        [HttpPost("GetAllFile")]
        public async Task<ActionResult> GetAllFileByExtentionName([FromBody] fileInfoDto model)
        {
            try
            {
                var list = await _filesOperationsBusiness.GetAllFilesByExtenstion(model);

                Response.AddPagination(list.CurrentPage, list.PageSize, list.TotalCount, list.TotalPages);

                return Ok(list);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [NonAction]
        private string UploadFile(IFormFile file)
        {
            string uniqueFileName = string.Empty;
            string filePath = string.Empty;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName ;
                bool mainFolderExists = Directory.Exists(uploadsFolder);
                if (!mainFolderExists)
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string fileNameUploadsFolder = Path.Combine(uploadsFolder,Path.GetExtension(file.FileName)?.Trim('.') );

                bool fileFolderExists = Directory.Exists(fileNameUploadsFolder);
                if (!fileFolderExists)
                {
                    Directory.CreateDirectory(fileNameUploadsFolder);
                }
                filePath = Path.Combine(fileNameUploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);

                }

            }
            return filePath;
        }
    }
}
