using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoedBezigWebApp.Migrations
{
    public partial class UserContactsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "interests",
                columns: table => new
                {
                    interest_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(maxLength: 255, nullable: true),
                    name = table.Column<string>(maxLength: 255, nullable: false),
                    name_brown = table.Column<string>(maxLength: 255, nullable: true),
                    name_grey = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interests_interest_id", x => x.interest_id);
                });

            migrationBuilder.CreateTable(
                name: "news_subscribers",
                columns: table => new
                {
                    subscriber_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(maxLength: 255, nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_news_subscribers_subscriber_id", x => x.subscriber_id);
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
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(maxLength: 255, nullable: true),
                    name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_role_id", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    skill_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skills_skill_id", x => x.skill_id);
                });

            migrationBuilder.CreateTable(
                name: "structuur",
                columns: table => new
                {
                    structuur_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_structuur", x => x.structuur_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    address_capital = table.Column<string>(maxLength: 255, nullable: true),
                    address_city = table.Column<string>(maxLength: 255, nullable: true),
                    address_country = table.Column<string>(maxLength: 255, nullable: true),
                    address_line1 = table.Column<string>(maxLength: 255, nullable: true),
                    address_line2 = table.Column<string>(maxLength: 255, nullable: true),
                    address_postal_code = table.Column<string>(maxLength: 255, nullable: true),
                    auth_level = table.Column<short>(nullable: true),
                    bank_account = table.Column<string>(maxLength: 255, nullable: true),
                    banned = table.Column<string>(maxLength: 1, nullable: true),
                    bic_code = table.Column<string>(maxLength: 255, nullable: true),
                    birthdate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    email = table.Column<string>(maxLength: 255, nullable: false),
                    engagement = table.Column<int>(nullable: true),
                    facebook_login = table.Column<string>(maxLength: 255, nullable: true),
                    family_name = table.Column<string>(maxLength: 255, nullable: true),
                    first_name = table.Column<string>(maxLength: 255, nullable: true),
                    gender = table.Column<string>(maxLength: 1, nullable: true),
                    google_login = table.Column<string>(maxLength: 255, nullable: true),
                    is_account_active = table.Column<int>(nullable: true),
                    is_agenda_public = table.Column<int>(nullable: true),
                    is_profile_public = table.Column<int>(nullable: true),
                    job_search_description = table.Column<string>(nullable: true),
                    language = table.Column<string>(maxLength: 255, nullable: true),
                    last_login = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    modified_at = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    national_number = table.Column<string>(maxLength: 255, nullable: true),
                    nationality = table.Column<string>(maxLength: 255, nullable: true),
                    oauth_provider = table.Column<string>(maxLength: 255, nullable: true),
                    oauth_uid = table.Column<string>(maxLength: 255, nullable: true),
                    passwd = table.Column<string>(maxLength: 255, nullable: true),
                    passwd_modified_at = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    passwd_recovery_code = table.Column<string>(maxLength: 255, nullable: true),
                    passwd_recovery_date = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    phone = table.Column<string>(maxLength: 255, nullable: true),
                    profile_picture = table.Column<string>(maxLength: 255, nullable: true),
                    short_introduction = table.Column<string>(nullable: true),
                    twitter_login = table.Column<string>(maxLength: 255, nullable: true),
                    username = table.Column<string>(maxLength: 255, nullable: false),
                    volunteer_location = table.Column<string>(maxLength: 255, nullable: true),
                    volunteer_location_max_distance = table.Column<int>(nullable: true),
                    youtube_login = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_user_id", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "username_or_email_on_hold",
                columns: table => new
                {
                    ai = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    time = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    username_or_email = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_username_or_email_on_hold_ai", x => x.ai);
                });

            migrationBuilder.CreateTable(
                name: "user_provider",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    provider = table.Column<string>(maxLength: 50, nullable: false),
                    provider_uid = table.Column<string>(maxLength: 255, nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_provider", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organization",
                columns: table => new
                {
                    org_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    address_id = table.Column<int>(nullable: true),
                    btw = table.Column<string>(maxLength: 50, nullable: true),
                    contact_counter = table.Column<int>(nullable: true, defaultValueSql: "0"),
                    contact_date = table.Column<DateTime>(type: "date", nullable: true),
                    description = table.Column<string>(maxLength: 800, nullable: true),
                    facebook_url = table.Column<string>(maxLength: 255, nullable: true),
                    google_url = table.Column<string>(maxLength: 255, nullable: true),
                    identification_nr = table.Column<string>(maxLength: 255, nullable: true),
                    instagram_url = table.Column<string>(maxLength: 255, nullable: true),
                    logo = table.Column<string>(maxLength: 255, nullable: true),
                    name = table.Column<string>(maxLength: 255, nullable: false),
                    org_type = table.Column<int>(nullable: false),
                    parent_id = table.Column<int>(nullable: true),
                    structuur_id = table.Column<int>(nullable: true),
                    twitter_url = table.Column<string>(maxLength: 255, nullable: true),
                    website_url = table.Column<string>(maxLength: 255, nullable: true),
                    youtube_url = table.Column<string>(maxLength: 255, nullable: true)
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
                    table.ForeignKey(
                        name: "organization$FK_org_child_ref",
                        column: x => x.parent_id,
                        principalTable: "organization",
                        principalColumn: "org_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "organization$FK_structuur_to_vacancy_ref",
                        column: x => x.structuur_id,
                        principalTable: "structuur",
                        principalColumn: "structuur_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "educations",
                columns: table => new
                {
                    education_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    date_from = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    date_to = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    school = table.Column<string>(maxLength: 255, nullable: true),
                    specialization = table.Column<string>(maxLength: 255, nullable: true),
                    user_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_educations_education_id", x => x.education_id);
                    table.ForeignKey(
                        name: "educations$FK_user_education_ref",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "professions",
                columns: table => new
                {
                    profession_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    date_from = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    date_to = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    description = table.Column<string>(maxLength: 255, nullable: true),
                    profession = table.Column<string>(maxLength: 255, nullable: false),
                    user_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professions_profession_id", x => x.profession_id);
                    table.ForeignKey(
                        name: "professions$FK_user_profession_ref",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_messages",
                columns: table => new
                {
                    message_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    from_user_id = table.Column<int>(nullable: false),
                    message = table.Column<string>(nullable: false),
                    read_date = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    sent_date = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    to_user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_messages_message_id", x => x.message_id);
                    table.ForeignKey(
                        name: "user_messages$FK_message_from_user_ref",
                        column: x => x.from_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "user_messages$FK_message_to_user_ref",
                        column: x => x.to_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "org_contacts",
                columns: table => new
                {
                    contact_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(maxLength: 255, nullable: true),
                    family_name = table.Column<string>(maxLength: 255, nullable: false),
                    first_name = table.Column<string>(maxLength: 255, nullable: false),
                    function = table.Column<string>(maxLength: 255, nullable: true),
                    org_id = table.Column<int>(nullable: true),
                    phone = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_contacts_contact_id", x => x.contact_id);
                    table.ForeignKey(
                        name: "FK_org_contacts_organization_org_id",
                        column: x => x.org_id,
                        principalTable: "organization",
                        principalColumn: "org_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vacancies",
                columns: table => new
                {
                    vacancy_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    address_capital = table.Column<string>(maxLength: 255, nullable: true),
                    address_city = table.Column<string>(maxLength: 255, nullable: true),
                    address_country = table.Column<string>(maxLength: 255, nullable: true),
                    address_line_1 = table.Column<string>(maxLength: 255, nullable: true),
                    address_line_2 = table.Column<string>(maxLength: 255, nullable: true),
                    address_postal_code = table.Column<string>(maxLength: 255, nullable: true),
                    banner = table.Column<string>(maxLength: 255, nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    description = table.Column<string>(maxLength: 400, nullable: true),
                    engagement = table.Column<int>(nullable: true),
                    flexible_date = table.Column<short>(nullable: false, defaultValueSql: "0"),
                    is_deleted = table.Column<short>(nullable: false, defaultValueSql: "0"),
                    logo = table.Column<string>(maxLength: 255, nullable: true),
                    name = table.Column<string>(maxLength: 255, nullable: false),
                    number_required = table.Column<int>(nullable: false),
                    occupancy_kind = table.Column<int>(nullable: true),
                    offer = table.Column<string>(maxLength: 400, nullable: true),
                    org_id = table.Column<int>(nullable: false),
                    vacancy_visibility_end_date = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    vacancy_visibility_start_date = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    website = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacancies_vacancy_id", x => x.vacancy_id);
                    table.ForeignKey(
                        name: "vacancies$FK_Reference_23",
                        column: x => x.org_id,
                        principalTable: "organization",
                        principalColumn: "org_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_contacts",
                columns: table => new
                {
                    user_contacts_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    contact_date = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    from_user_id = table.Column<int>(nullable: true),
                    message = table.Column<string>(nullable: true),
                    org_contact_id = table.Column<int>(nullable: true),
                    suggested_vacancy_id = table.Column<int>(nullable: true),
                    to_user_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_contacts_id", x => x.user_contacts_id);
                    table.ForeignKey(
                        name: "user_contacts$FK_Reference_49",
                        column: x => x.from_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "user_contacts$FK_Reference_50",
                        column: x => x.org_contact_id,
                        principalTable: "org_contacts",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "user_contacts$FK_Reference_52",
                        column: x => x.suggested_vacancy_id,
                        principalTable: "vacancies",
                        principalColumn: "vacancy_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "user_contacts$FK_Reference_51",
                        column: x => x.to_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vacancy_calendar",
                columns: table => new
                {
                    vacancy_calendar_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    date_from = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    date_to = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    vacancy_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacancy_calendar", x => x.vacancy_calendar_id);
                    table.ForeignKey(
                        name: "vacancy_calendar$FK_Reference_34",
                        column: x => x.vacancy_id,
                        principalTable: "vacancies",
                        principalColumn: "vacancy_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vacancy_invitations",
                columns: table => new
                {
                    invitation_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    invitation_date = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    message = table.Column<string>(nullable: true),
                    status = table.Column<string>(type: "char(10)", nullable: true),
                    user_id = table.Column<int>(nullable: true),
                    vacancy_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacancy_invitations_invitation_id", x => x.invitation_id);
                    table.ForeignKey(
                        name: "vacancy_invitations$FK_Reference_33",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "vacancy_invitations$FK_Reference_32",
                        column: x => x.vacancy_id,
                        principalTable: "vacancies",
                        principalColumn: "vacancy_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vacancy_subscriptions",
                columns: table => new
                {
                    subscription_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    message = table.Column<string>(nullable: true),
                    org_rating = table.Column<int>(nullable: true),
                    org_rating_comment = table.Column<string>(maxLength: 255, nullable: true),
                    status = table.Column<int>(nullable: false),
                    subscription_date = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    user_id = table.Column<int>(nullable: true),
                    vacancy_id = table.Column<int>(nullable: true),
                    volunteer_rating = table.Column<int>(nullable: true),
                    volunteer_rating_comment = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacancy_subscriptions_subscription_id", x => x.subscription_id);
                    table.ForeignKey(
                        name: "vacancy_subscriptions$FK_Reference_31",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "vacancy_subscriptions$FK_Reference_30",
                        column: x => x.vacancy_id,
                        principalTable: "vacancies",
                        principalColumn: "vacancy_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "FK_user_education_ref",
                table: "educations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "FK_org_address_id_ref",
                table: "organization",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "FK_org_child_ref",
                table: "organization",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "FK_structuur_to_vacancy_ref",
                table: "organization",
                column: "structuur_id");

            migrationBuilder.CreateIndex(
                name: "FK_org_contacts_ref",
                table: "org_contacts",
                column: "org_id");

            migrationBuilder.CreateIndex(
                name: "FK_user_profession_ref",
                table: "professions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "FK_Reference_49",
                table: "user_contacts",
                column: "from_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_Reference_50",
                table: "user_contacts",
                column: "org_contact_id");

            migrationBuilder.CreateIndex(
                name: "FK_Reference_52",
                table: "user_contacts",
                column: "suggested_vacancy_id");

            migrationBuilder.CreateIndex(
                name: "FK_Reference_51",
                table: "user_contacts",
                column: "to_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_message_from_user_ref",
                table: "user_messages",
                column: "from_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_message_to_user_ref",
                table: "user_messages",
                column: "to_user_id");

            migrationBuilder.CreateIndex(
                name: "FK_Reference_23",
                table: "vacancies",
                column: "org_id");

            migrationBuilder.CreateIndex(
                name: "FK_Reference_34",
                table: "vacancy_calendar",
                column: "vacancy_id");

            migrationBuilder.CreateIndex(
                name: "FK_Reference_33",
                table: "vacancy_invitations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "FK_Reference_32",
                table: "vacancy_invitations",
                column: "vacancy_id");

            migrationBuilder.CreateIndex(
                name: "FK_Reference_31",
                table: "vacancy_subscriptions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "FK_Reference_30",
                table: "vacancy_subscriptions",
                column: "vacancy_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "educations");

            migrationBuilder.DropTable(
                name: "interests");

            migrationBuilder.DropTable(
                name: "news_subscribers");

            migrationBuilder.DropTable(
                name: "professions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "skills");

            migrationBuilder.DropTable(
                name: "user_contacts");

            migrationBuilder.DropTable(
                name: "user_messages");

            migrationBuilder.DropTable(
                name: "username_or_email_on_hold");

            migrationBuilder.DropTable(
                name: "user_provider");

            migrationBuilder.DropTable(
                name: "vacancy_calendar");

            migrationBuilder.DropTable(
                name: "vacancy_invitations");

            migrationBuilder.DropTable(
                name: "vacancy_subscriptions");

            migrationBuilder.DropTable(
                name: "org_contacts");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "vacancies");

            migrationBuilder.DropTable(
                name: "organization");

            migrationBuilder.DropTable(
                name: "organizational_addresses");

            migrationBuilder.DropTable(
                name: "structuur");
        }
    }
}
