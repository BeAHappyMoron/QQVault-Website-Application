using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QQVault.Migrations
{
    /// <inheritdoc />
    public partial class AddedCreatedAtField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Feedback");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Feedback",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Feedback",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Feedback",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Feedback",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
