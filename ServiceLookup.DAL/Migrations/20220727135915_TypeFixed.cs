using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceLookup.DAL.Migrations
{
    public partial class TypeFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Services");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Services",
                type: "int",
                nullable: true);
        }
    }
}
