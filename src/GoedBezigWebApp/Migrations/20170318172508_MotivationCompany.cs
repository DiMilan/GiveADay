﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoedBezigWebApp.Migrations
{
    public partial class MotivationCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotivationState");

            migrationBuilder.AddColumn<string>(
                name: "CompanyAddress",
                table: "groups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyContactEmail",
                table: "groups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyContactName",
                table: "groups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyContactSurname",
                table: "groups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyContactTitle",
                table: "groups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyEmail",
                table: "groups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "groups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyWebsite",
                table: "groups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyAddress",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "CompanyContactEmail",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "CompanyContactName",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "CompanyContactSurname",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "CompanyContactTitle",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "CompanyEmail",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "CompanyWebsite",
                table: "groups");

            migrationBuilder.CreateTable(
                name: "MotivationState",
                columns: table => new
                {
                    MotivationStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivationState", x => x.MotivationStatusId);
                });
        }
    }
}
