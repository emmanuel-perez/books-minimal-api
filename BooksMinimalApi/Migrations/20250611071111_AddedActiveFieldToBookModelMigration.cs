using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedActiveFieldToBookModelMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Books");
        }
    }
}
