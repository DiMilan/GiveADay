using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoedBezigWebApp.Migrations
{
    public partial class MotivationState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MotivationStatusId",
                table: "groups",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MotivationState",
                columns: table => new
                {
                    MotivationStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    MotivationEditable = table.Column<bool>(nullable: false),
                    MotivationSubmittable = table.Column<bool>(nullable: false),
                    MotivationName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivationState", x => x.MotivationStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_groups_MotivationStatusId",
                table: "groups",
                column: "MotivationStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_groups_MotivationState_MotivationStatusId",
                table: "groups",
                column: "MotivationStatusId",
                principalTable: "MotivationState",
                principalColumn: "MotivationStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_groups_MotivationState_MotivationStatusId",
                table: "groups");

            migrationBuilder.DropTable(
                name: "MotivationState");

            migrationBuilder.DropIndex(
                name: "IX_groups_MotivationStatusId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "MotivationStatusId",
                table: "groups");
        }
    }
}
