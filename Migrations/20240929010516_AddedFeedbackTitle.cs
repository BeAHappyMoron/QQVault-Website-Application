using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QQVault.Migrations
{
    /// <inheritdoc />
    public partial class AddedFeedbackTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Feedback",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Feedback");
        }
    }
}
