using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ES.FileUploading.Business.Abstract;
using ES.FileUploading.Business.Base;
using ES.FileUploading.Business.Dtos;
using ES.FileUploading.DataAccess;
using ES.FileUploading.Entities;
using Microsoft.EntityFrameworkCore;

namespace ES.FileUploading.Business.Concrete
{
    public class FilesOperationsBusiness : IFilesOperationsBusiness
    {
        private readonly EsContext _context;
        public FilesOperationsBusiness(EsContext context)
        {
            _context = context;
        }
        public async Task UploadFile(FilesInfo FilesInfo)
        {
            // to do check file extension 
            await _context.AddAsync(FilesInfo);
            await _context.SaveChangesAsync();
        }
        public async Task CreateExtension(FileExtension fileExtension)
        {
                await _context.AddAsync(fileExtension);
                await _context.SaveChangesAsync();
        }
        public async Task<PagedList<FileExtension>> GetAllExtensionsHasFiles(PaginationsParams input)
        {
            //var fileExtensions = _context.FileExtensions;
            //return await PagedList<FileExtension>.CreateAsync(fileExtensions, input.PageNumber, input.PageSize);
            var fileExtensions = _context.FileExtensions.Include(x => x.FileInfos).Where(z => z.FileInfos.Count > 0);
            return await PagedList<FileExtension>.CreateAsync(fileExtensions, input.PageNumber, input.PageSize);

        }
        public async Task<PagedList<FileExtension>> GetAllExtenstionHasFiles(PaginationsParams input)
        {
            var fileExtensions = _context.FileExtensions.Include(x => x.FileInfos).Where(z => z.FileInfos.Count > 0);
            return await PagedList<FileExtension>.CreateAsync(fileExtensions, input.PageNumber, input.PageSize);

        }
        public FileExtension GetExtenstionByName(string extensionName)
        {
            return _context.FileExtensions.FirstOrDefault(x => x.ExtensionName == extensionName);
        }
         
        public async Task<List<FileExtension>> GetAllExtenstion()
        {
            return await _context.FileExtensions.ToListAsync();

        }
        public async Task<PagedList<FilesInfo>> GetAllFilesByExtenstion(fileInfoDto input)
        {
            
            var filesInfos = _context.FilesInfos.Where(x => x.FileExtension.ExtensionName == input.ExtensionName);

            return await PagedList<FilesInfo>.CreateAsync(filesInfos, input.PageNumber, input.PageSize);

        }


    }
}
