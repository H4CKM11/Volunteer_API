using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteer_API.Migrations
{
    /// <inheritdoc />
    public partial class skillLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "skillLevel",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "skillLevel",
                table: "Users");
        }
    }
}
