using Microsoft.EntityFrameworkCore.Migrations;

namespace GoedBezigWebApp.Migrations
{
    public partial class AddLector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LectorUserId",
                table: "users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_LectorUserId",
                table: "users",
                column: "LectorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_users_LectorUserId",
                table: "users",
                column: "LectorUserId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_users_LectorUserId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_LectorUserId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LectorUserId",
                table: "users");
        }
    }
}
