using Microsoft.EntityFrameworkCore.Migrations;

namespace GoedBezigWebApp.Migrations
{
    public partial class ModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupName1",
                table: "user_groups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationOrgId",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ClosedGroups",
                table: "organization",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationOrgId",
                table: "groups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_groups_GroupName1",
                table: "user_groups",
                column: "GroupName1");

            migrationBuilder.CreateIndex(
                name: "IX_users_GroupName",
                table: "users",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_users_OrganizationOrgId",
                table: "users",
                column: "OrganizationOrgId");

            migrationBuilder.CreateIndex(
                name: "IX_groups_OrganizationOrgId",
                table: "groups",
                column: "OrganizationOrgId");

            migrationBuilder.AddForeignKey(
                name: "FK_groups_organization_OrganizationOrgId",
                table: "groups",
                column: "OrganizationOrgId",
                principalTable: "organization",
                principalColumn: "org_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_groups_GroupName",
                table: "users",
                column: "GroupName",
                principalTable: "groups",
                principalColumn: "GroupName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_organization_OrganizationOrgId",
                table: "users",
                column: "OrganizationOrgId",
                principalTable: "organization",
                principalColumn: "org_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_groups_groups_GroupName1",
                table: "user_groups",
                column: "GroupName1",
                principalTable: "groups",
                principalColumn: "GroupName",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_groups_organization_OrganizationOrgId",
                table: "groups");

            migrationBuilder.DropForeignKey(
                name: "FK_users_groups_GroupName",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_organization_OrganizationOrgId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_user_groups_groups_GroupName1",
                table: "user_groups");

            migrationBuilder.DropIndex(
                name: "IX_user_groups_GroupName1",
                table: "user_groups");

            migrationBuilder.DropIndex(
                name: "IX_users_GroupName",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_OrganizationOrgId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_groups_OrganizationOrgId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "GroupName1",
                table: "user_groups");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "users");

            migrationBuilder.DropColumn(
                name: "OrganizationOrgId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ClosedGroups",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "OrganizationOrgId",
                table: "groups");
        }
    }
}
