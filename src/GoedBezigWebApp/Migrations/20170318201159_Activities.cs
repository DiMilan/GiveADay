using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoedBezigWebApp.Migrations
{
    public partial class Activities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_events_EventId",
                table: "messages");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "messages",
                newName: "ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_messages_EventId",
                table: "messages",
                newName: "IX_messages_ActivityId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "events",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "events",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_events_ActivityId",
                table: "messages",
                column: "ActivityId",
                principalTable: "events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_events_ActivityId",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "events");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "messages",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_messages_ActivityId",
                table: "messages",
                newName: "IX_messages_EventId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "events",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_events_EventId",
                table: "messages",
                column: "EventId",
                principalTable: "events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
