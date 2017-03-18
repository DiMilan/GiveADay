using Microsoft.EntityFrameworkCore.Migrations;

namespace GoedBezigWebApp.Migrations
{
    public partial class OrganizationModelDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "organization",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domain",
                table: "organization");
        }
    }
}
