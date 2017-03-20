using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoedBezigWebApp.Migrations
{
    public partial class ExternalOrganizationInGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExternalOrganizationOrgId",
                table: "groups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_groups_ExternalOrganizationOrgId",
                table: "groups",
                column: "ExternalOrganizationOrgId");

            migrationBuilder.AddForeignKey(
                name: "FK_groups_organization_ExternalOrganizationOrgId",
                table: "groups",
                column: "ExternalOrganizationOrgId",
                principalTable: "organization",
                principalColumn: "org_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_groups_organization_ExternalOrganizationOrgId",
                table: "groups");

            migrationBuilder.DropIndex(
                name: "IX_groups_ExternalOrganizationOrgId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "ExternalOrganizationOrgId",
                table: "groups");
        }
    }
}
