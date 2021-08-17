using ES.FileUploading.Entities;
using Microsoft.EntityFrameworkCore;


namespace ES.FileUploading.DataAccess
{

    public class EsContext : DbContext
    {
        public DbSet<FilesInfo> FilesInfos { get; set; }
        public DbSet<FileExtension> FileExtensions{ get; set; }
        public EsContext(DbContextOptions<EsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FileExtension>(f =>
            {
                f.HasIndex(x => x.ExtensionName).IsUnique();
                f.HasKey(x => x.Id);
            });

            modelBuilder.Entity<FilesInfo>(f =>
            {
                f.HasKey(x => x.Id);
            });
        }

    }

}