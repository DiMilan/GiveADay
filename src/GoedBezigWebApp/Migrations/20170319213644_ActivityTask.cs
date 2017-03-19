using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoedBezigWebApp.Migrations
{
    public partial class ActivityTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityTaskId",
                table: "users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActivityTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentState = table.Column<int>(nullable: false),
                    description = table.Column<string>(maxLength: 255, nullable: false),
                    fromDateTime = table.Column<DateTime>(nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    toDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityTasks_groups_GroupName",
                        column: x => x.GroupName,
                        principalTable: "groups",
                        principalColumn: "GroupName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityTasks_events_Id",
                        column: x => x.Id,
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityTaskUser",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ActivityTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTaskUser", x => new { x.UserId, x.ActivityTaskId });
                    table.ForeignKey(
                        name: "FK_ActivityTaskUser_ActivityTasks_ActivityTaskId",
                        column: x => x.ActivityTaskId,
                        principalTable: "ActivityTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityTaskUser_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_ActivityTaskId",
                table: "users",
                column: "ActivityTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTasks_GroupName",
                table: "ActivityTasks",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTaskUser_ActivityTaskId",
                table: "ActivityTaskUser",
                column: "ActivityTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_ActivityTasks_ActivityTaskId",
                table: "users",
                column: "ActivityTaskId",
                principalTable: "ActivityTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_ActivityTasks_ActivityTaskId",
                table: "users");

            migrationBuilder.DropTable(
                name: "ActivityTaskUser");

            migrationBuilder.DropTable(
                name: "ActivityTasks");

            migrationBuilder.DropIndex(
                name: "IX_users_ActivityTaskId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ActivityTaskId",
                table: "users");
        }
    }
}
