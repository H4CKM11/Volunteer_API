using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteer_API.Migrations
{
    /// <inheritdoc />
    public partial class skillLevelEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "skillLevel",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "skillLevel",
                table: "Events");
        }
    }
}
