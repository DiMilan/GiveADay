using Microsoft.EntityFrameworkCore.Migrations;

namespace GoedBezigWebApp.Migrations
{
    public partial class Invitations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_groups_groups_GroupName1",
                table: "user_groups");

            migrationBuilder.DropIndex(
                name: "IX_user_groups_GroupName1",
                table: "user_groups");

            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "user_groups");

            migrationBuilder.DropColumn(
                name: "GroupName1",
                table: "user_groups");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "user_groups",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "user_groups");

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "user_groups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GroupName1",
                table: "user_groups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_groups_GroupName1",
                table: "user_groups",
                column: "GroupName1");

            migrationBuilder.AddForeignKey(
                name: "FK_user_groups_groups_GroupName1",
                table: "user_groups",
                column: "GroupName1",
                principalTable: "groups",
                principalColumn: "GroupName",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
