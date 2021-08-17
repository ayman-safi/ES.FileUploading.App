using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES.FileUploading.Business.Base;
using ES.FileUploading.Business.Dtos;
using ES.FileUploading.Entities;

namespace ES.FileUploading.Business.Abstract
{
    public interface IFilesOperationsBusiness
    {
        Task<PagedList<FileExtension>> GetAllExtensionsHasFiles(PaginationsParams input);
        Task<List<FileExtension>> GetAllExtenstion();
        FileExtension GetExtenstionByName(string extensionName);
        Task<PagedList<FilesInfo>> GetAllFilesByExtenstion(fileInfoDto input);
        Task UploadFile(FilesInfo FilesInfo);
        Task CreateExtension(FileExtension FilesInfo);
    }
}
