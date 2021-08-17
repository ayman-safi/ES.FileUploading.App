using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ES.FileUploading.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileExtensions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtensionName = table.Column<string>(nullable: true),
                    MaxAllowedSize = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileExtensions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilesInfos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileExtensionId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileUrl = table.Column<string>(nullable: true),
                    FileSize = table.Column<double>(nullable: false),
                    UploadingDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilesInfos_FileExtensions_FileExtensionId",
                        column: x => x.FileExtensionId,
                        principalTable: "FileExtensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileExtensions_ExtensionName",
                table: "FileExtensions",
                column: "ExtensionName",
                unique: true,
                filter: "[ExtensionName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FilesInfos_FileExtensionId",
                table: "FilesInfos",
                column: "FileExtensionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesInfos");

            migrationBuilder.DropTable(
                name: "FileExtensions");
        }
    }
}
