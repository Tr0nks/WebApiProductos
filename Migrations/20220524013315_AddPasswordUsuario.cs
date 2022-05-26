using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCRUD.Migrations
{
    public partial class AddPasswordUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorreoElectronico",
                table: "Usuarios",
                newName: "Correo");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Correo",
                table: "Usuarios",
                newName: "CorreoElectronico");
        }
    }
}
