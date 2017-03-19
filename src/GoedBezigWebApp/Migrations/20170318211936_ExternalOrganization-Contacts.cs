using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoedBezigWebApp.Migrations
{
    public partial class ExternalOrganizationContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization_contacts",
                columns: table => new
                {
                    contact_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Functie = table.Column<string>(nullable: true),
                    Naam = table.Column<string>(nullable: true),
                    OrganizationOrgId = table.Column<int>(nullable: false),
                    Voornaam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_contacts_contact_id", x => x.contact_id);
                    table.ForeignKey(
                        name: "FK_organization_contacts_organization_OrganizationOrgId",
                        column: x => x.OrganizationOrgId,
                        principalTable: "organization",
                        principalColumn: "org_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_contacts_OrganizationOrgId",
                table: "organization_contacts",
                column: "OrganizationOrgId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_contacts");
        }
    }
}
