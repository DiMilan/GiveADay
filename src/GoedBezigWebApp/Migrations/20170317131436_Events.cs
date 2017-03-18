using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoedBezigWebApp.Migrations
{
    public partial class Events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_groups_MotivationState_MotivationStatusId",
                table: "groups");

            migrationBuilder.DropForeignKey(
                name: "FK_groups_organization_OrganizationOrgId",
                table: "groups");

            migrationBuilder.DropIndex(
                name: "IX_groups_MotivationStatusId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "MotivationStatusId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "MotivationEditable",
                table: "MotivationState");

            migrationBuilder.DropColumn(
                name: "MotivationSubmittable",
                table: "MotivationState");

            migrationBuilder.DropColumn(
                name: "MotivationName",
                table: "MotivationState");

            migrationBuilder.RenameColumn(
                name: "OrganizationOrgId",
                table: "groups",
                newName: "GBOrganizationOrgId");

            migrationBuilder.RenameIndex(
                name: "IX_groups_OrganizationOrgId",
                table: "groups",
                newName: "IX_groups_GBOrganizationOrgId");

            migrationBuilder.AddColumn<int>(
                name: "MotivationStatus",
                table: "groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_groups_GroupName",
                        column: x => x.GroupName,
                        principalTable: "groups",
                        principalColumn: "GroupName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_GroupName",
                table: "Event",
                column: "GroupName");

            migrationBuilder.AddForeignKey(
                name: "FK_groups_organization_GBOrganizationOrgId",
                table: "groups",
                column: "GBOrganizationOrgId",
                principalTable: "organization",
                principalColumn: "org_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_groups_organization_GBOrganizationOrgId",
                table: "groups");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropColumn(
                name: "MotivationStatus",
                table: "groups");

            migrationBuilder.RenameColumn(
                name: "GBOrganizationOrgId",
                table: "groups",
                newName: "OrganizationOrgId");

            migrationBuilder.RenameIndex(
                name: "IX_groups_GBOrganizationOrgId",
                table: "groups",
                newName: "IX_groups_OrganizationOrgId");

            migrationBuilder.AddColumn<int>(
                name: "MotivationStatusId",
                table: "groups",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MotivationEditable",
                table: "MotivationState",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MotivationSubmittable",
                table: "MotivationState",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MotivationName",
                table: "MotivationState",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddForeignKey(
                name: "FK_groups_organization_OrganizationOrgId",
                table: "groups",
                column: "OrganizationOrgId",
                principalTable: "organization",
                principalColumn: "org_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
