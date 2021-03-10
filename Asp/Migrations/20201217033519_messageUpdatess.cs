using Microsoft.EntityFrameworkCore.Migrations;

namespace Asp.Migrations
{
    public partial class messageUpdatess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Apartments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
