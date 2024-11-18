using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneIdeaGenerator.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingsRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Ratings");
        }
    }
}
