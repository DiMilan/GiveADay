using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoedBezigWebApp.Migrations
{
    public partial class OrganizationAndGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    GroupName = table.Column<string>(maxLength: 100, nullable: false),
                    ClosedGroup = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups", x => x.GroupName);
                });

            migrationBuilder.CreateTable(
                name: "organizational_addresses",
                columns: table => new
                {
                    address_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    address_capital = table.Column<string>(maxLength: 255, nullable: true),
                    address_city = table.Column<string>(maxLength: 255, nullable: true),
                    address_country = table.Column<string>(maxLength: 255, nullable: false),
                    address_line_1 = table.Column<string>(maxLength: 255, nullable: true),
                    address_line_2 = table.Column<string>(maxLength: 255, nullable: true),
                    address_postal_code = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizational_addresses_address_id", x => x.address_id);
                });

            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    org_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    address_id = table.Column<int>(nullable: true),
                    btw = table.Column<string>(maxLength: 50, nullable: true),
                    description = table.Column<string>(maxLength: 800, nullable: true),
                    logo = table.Column<string>(maxLength: 255, nullable: true),
                    name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_org_id", x => x.org_id);
                    table.ForeignKey(
                        name: "FK_organization_organizational_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "organizational_addresses",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "FK_org_address_id_ref",
                table: "organization",
                column: "address_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "organization");

            migrationBuilder.DropTable(
                name: "organizational_addresses");
        }
    }
}
