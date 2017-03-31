using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoedBezigWebApp.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_groups_GroupName",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "FK_groups_organization_GBOrganizationOrgId",
                table: "groups");

            migrationBuilder.RenameColumn(
                name: "hasGBLabel",
                table: "organization",
                newName: "HasGbLabel");

            migrationBuilder.RenameColumn(
                name: "MotivationStatus",
                table: "groups",
                newName: "GroupState");

            migrationBuilder.RenameColumn(
                name: "GBOrganizationOrgId",
                table: "groups",
                newName: "GbOrganizationOrgId");

            migrationBuilder.RenameIndex(
                name: "IX_groups_GBOrganizationOrgId",
                table: "groups",
                newName: "IX_groups_GbOrganizationOrgId");

            migrationBuilder.AlterColumn<string>(
                name: "Motivatie",
                table: "groups",
                maxLength: 10000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExternalOrganizationOrgId",
                table: "groups",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ActivityTasks",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "ActivityTasks",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "events",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_groups_ExternalOrganizationOrgId",
                table: "groups",
                column: "ExternalOrganizationOrgId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_groups_GroupName",
                table: "events",
                column: "GroupName",
                principalTable: "groups",
                principalColumn: "GroupName",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_groups_organization_ExternalOrganizationOrgId",
                table: "groups",
                column: "ExternalOrganizationOrgId",
                principalTable: "organization",
                principalColumn: "org_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_groups_organization_GbOrganizationOrgId",
                table: "groups",
                column: "GbOrganizationOrgId",
                principalTable: "organization",
                principalColumn: "org_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_groups_GroupName",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "FK_groups_organization_ExternalOrganizationOrgId",
                table: "groups");

            migrationBuilder.DropForeignKey(
                name: "FK_groups_organization_GbOrganizationOrgId",
                table: "groups");

            migrationBuilder.DropIndex(
                name: "IX_groups_ExternalOrganizationOrgId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "ExternalOrganizationOrgId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "ActivityTasks");

            migrationBuilder.RenameColumn(
                name: "HasGbLabel",
                table: "organization",
                newName: "hasGBLabel");

            migrationBuilder.RenameColumn(
                name: "GroupState",
                table: "groups",
                newName: "MotivationStatus");

            migrationBuilder.RenameColumn(
                name: "GbOrganizationOrgId",
                table: "groups",
                newName: "GBOrganizationOrgId");

            migrationBuilder.RenameIndex(
                name: "IX_groups_GbOrganizationOrgId",
                table: "groups",
                newName: "IX_groups_GBOrganizationOrgId");

            migrationBuilder.AlterColumn<string>(
                name: "Motivatie",
                table: "groups",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ActivityTasks",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "events",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_events_groups_GroupName",
                table: "events",
                column: "GroupName",
                principalTable: "groups",
                principalColumn: "GroupName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_groups_organization_GBOrganizationOrgId",
                table: "groups",
                column: "GBOrganizationOrgId",
                principalTable: "organization",
                principalColumn: "org_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
