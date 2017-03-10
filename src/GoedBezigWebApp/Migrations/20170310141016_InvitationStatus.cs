using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoedBezigWebApp.Migrations
{
    public partial class InvitationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_groups_groups_GroupName",
                table: "user_groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_groups",
                table: "user_groups");

            migrationBuilder.DropIndex(
                name: "IX_user_groups_UserId",
                table: "user_groups");

            migrationBuilder.DropColumn(
                name: "UserGroupId",
                table: "user_groups");

            migrationBuilder.RenameColumn(
                name: "GroupName",
                table: "user_groups",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_user_groups_GroupName",
                table: "user_groups",
                newName: "IX_user_groups_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_groups",
                table: "user_groups",
                columns: new[] { "UserId", "GroupId" });

            migrationBuilder.AddForeignKey(
                name: "FK_user_groups_groups_GroupId",
                table: "user_groups",
                column: "GroupId",
                principalTable: "groups",
                principalColumn: "GroupName",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_groups_groups_GroupId",
                table: "user_groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_groups",
                table: "user_groups");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "user_groups",
                newName: "GroupName");

            migrationBuilder.RenameIndex(
                name: "IX_user_groups_GroupId",
                table: "user_groups",
                newName: "IX_user_groups_GroupName");

            migrationBuilder.AddColumn<int>(
                name: "UserGroupId",
                table: "user_groups",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_groups",
                table: "user_groups",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_user_groups_UserId",
                table: "user_groups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_groups_groups_GroupName",
                table: "user_groups",
                column: "GroupName",
                principalTable: "groups",
                principalColumn: "GroupName",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
