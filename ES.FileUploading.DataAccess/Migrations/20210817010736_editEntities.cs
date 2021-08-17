using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ES.FileUploading.DataAccess.Migrations
{
    public partial class editEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadingDate",
                table: "FilesInfos");

            migrationBuilder.AlterColumn<long>(
                name: "MaxAllowedSize",
                table: "FileExtensions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UploadingDate",
                table: "FilesInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "MaxAllowedSize",
                table: "FileExtensions",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
