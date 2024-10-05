using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QQVault.Migrations
{
    /// <inheritdoc />
    public partial class Okay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Advices",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "AdviceId",
                table: "Advices",
                newName: "Id");

            migrationBuilder.AddColumn<byte[]>(
                name: "AvatarImage",
                table: "User",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ConsultantName",
                table: "Advices",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarImage",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ConsultantName",
                table: "Advices");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Advices",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Advices",
                newName: "AdviceId");
        }
    }
}
