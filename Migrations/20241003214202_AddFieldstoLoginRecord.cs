using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QQVault.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldstoLoginRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "LoginRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "LoginRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumberInput",
                table: "LoginRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LoginRecords_UserId",
                table: "LoginRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginRecords_User_UserId",
                table: "LoginRecords",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginRecords_User_UserId",
                table: "LoginRecords");

            migrationBuilder.DropIndex(
                name: "IX_LoginRecords_UserId",
                table: "LoginRecords");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "LoginRecords");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "LoginRecords");

            migrationBuilder.DropColumn(
                name: "NumberInput",
                table: "LoginRecords");
        }
    }
}
