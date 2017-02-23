using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GoedBezigWebApp.Data;

namespace GoedBezigWebApp.Migrations
{
    [DbContext(typeof(GoedBezigDbContext))]
    [Migration("20170223194338_UserContactsId")]
    partial class UserContactsId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GoedBezigWebApp.Models.Education", b =>
                {
                    b.Property<int>("EducationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("education_id");

                    b.Property<DateTime?>("DateFrom")
                        .HasColumnName("date_from")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("DateTo")
                        .HasColumnName("date_to")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("School")
                        .HasColumnName("school")
                        .HasMaxLength(255);

                    b.Property<string>("Specialization")
                        .HasColumnName("specialization")
                        .HasMaxLength(255);

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("EducationId")
                        .HasName("PK_educations_education_id");

                    b.HasIndex("UserId")
                        .HasName("FK_user_education_ref");

                    b.ToTable("educations");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Interest", b =>
                {
                    b.Property<int>("InterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("interest_id");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(255);

                    b.Property<string>("NameBrown")
                        .HasColumnName("name_brown")
                        .HasMaxLength(255);

                    b.Property<string>("NameGrey")
                        .HasColumnName("name_grey")
                        .HasMaxLength(255);

                    b.HasKey("InterestId")
                        .HasName("PK_interests_interest_id");

                    b.ToTable("interests");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.NewsSubscriber", b =>
                {
                    b.Property<int>("SubscriberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("subscriber_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasMaxLength(255);

                    b.Property<int>("Status")
                        .HasColumnName("status");

                    b.HasKey("SubscriberId")
                        .HasName("PK_news_subscribers_subscriber_id");

                    b.ToTable("news_subscribers");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Organization", b =>
                {
                    b.Property<int>("OrgId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("org_id");

                    b.Property<int?>("AddressId")
                        .HasColumnName("address_id");

                    b.Property<string>("Btw")
                        .HasColumnName("btw")
                        .HasMaxLength(50);

                    b.Property<int?>("ContactCounter")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("contact_counter")
                        .HasDefaultValueSql("0");

                    b.Property<DateTime?>("ContactDate")
                        .HasColumnName("contact_date")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(800);

                    b.Property<string>("FacebookUrl")
                        .HasColumnName("facebook_url")
                        .HasMaxLength(255);

                    b.Property<string>("GoogleUrl")
                        .HasColumnName("google_url")
                        .HasMaxLength(255);

                    b.Property<string>("IdentificationNr")
                        .HasColumnName("identification_nr")
                        .HasMaxLength(255);

                    b.Property<string>("InstagramUrl")
                        .HasColumnName("instagram_url")
                        .HasMaxLength(255);

                    b.Property<string>("Logo")
                        .HasColumnName("logo")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(255);

                    b.Property<int>("OrgType")
                        .HasColumnName("org_type");

                    b.Property<int?>("ParentId")
                        .HasColumnName("parent_id");

                    b.Property<int?>("StructuurId")
                        .HasColumnName("structuur_id");

                    b.Property<string>("TwitterUrl")
                        .HasColumnName("twitter_url")
                        .HasMaxLength(255);

                    b.Property<string>("WebsiteUrl")
                        .HasColumnName("website_url")
                        .HasMaxLength(255);

                    b.Property<string>("YoutubeUrl")
                        .HasColumnName("youtube_url")
                        .HasMaxLength(255);

                    b.HasKey("OrgId")
                        .HasName("PK_organization_org_id");

                    b.HasIndex("AddressId")
                        .HasName("FK_org_address_id_ref");

                    b.HasIndex("ParentId")
                        .HasName("FK_org_child_ref");

                    b.HasIndex("StructuurId")
                        .HasName("FK_structuur_to_vacancy_ref");

                    b.ToTable("organization");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.OrganizationalAddress", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("address_id");

                    b.Property<string>("AddressCapital")
                        .HasColumnName("address_capital")
                        .HasMaxLength(255);

                    b.Property<string>("AddressCity")
                        .HasColumnName("address_city")
                        .HasMaxLength(255);

                    b.Property<string>("AddressCountry")
                        .IsRequired()
                        .HasColumnName("address_country")
                        .HasMaxLength(255);

                    b.Property<string>("AddressLine1")
                        .HasColumnName("address_line_1")
                        .HasMaxLength(255);

                    b.Property<string>("AddressLine2")
                        .HasColumnName("address_line_2")
                        .HasMaxLength(255);

                    b.Property<string>("AddressPostalCode")
                        .HasColumnName("address_postal_code")
                        .HasMaxLength(255);

                    b.HasKey("AddressId")
                        .HasName("PK_organizational_addresses_address_id");

                    b.ToTable("organizational_addresses");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.OrgContact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("contact_id");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(255);

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasColumnName("family_name")
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasMaxLength(255);

                    b.Property<string>("Function")
                        .HasColumnName("function")
                        .HasMaxLength(255);

                    b.Property<int?>("OrgId")
                        .HasColumnName("org_id");

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasMaxLength(255);

                    b.HasKey("ContactId")
                        .HasName("PK_org_contacts_contact_id");

                    b.HasIndex("OrgId")
                        .HasName("FK_org_contacts_ref");

                    b.ToTable("org_contacts");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Profession", b =>
                {
                    b.Property<int>("ProfessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("profession_id");

                    b.Property<DateTime?>("DateFrom")
                        .HasColumnName("date_from")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("DateTo")
                        .HasColumnName("date_to")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("profession")
                        .HasMaxLength(255);

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("ProfessionId")
                        .HasName("PK_professions_profession_id");

                    b.HasIndex("UserId")
                        .HasName("FK_user_profession_ref");

                    b.ToTable("professions");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("role_id");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(255);

                    b.HasKey("RoleId")
                        .HasName("PK_roles_role_id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(255);

                    b.HasKey("SkillId")
                        .HasName("PK_skills_skill_id");

                    b.ToTable("skills");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Structuur", b =>
                {
                    b.Property<int>("StructuurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("structuur_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(255);

                    b.HasKey("StructuurId");

                    b.ToTable("structuur");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_id");

                    b.Property<string>("AddressCapital")
                        .HasColumnName("address_capital")
                        .HasMaxLength(255);

                    b.Property<string>("AddressCity")
                        .HasColumnName("address_city")
                        .HasMaxLength(255);

                    b.Property<string>("AddressCountry")
                        .HasColumnName("address_country")
                        .HasMaxLength(255);

                    b.Property<string>("AddressLine1")
                        .HasColumnName("address_line1")
                        .HasMaxLength(255);

                    b.Property<string>("AddressLine2")
                        .HasColumnName("address_line2")
                        .HasMaxLength(255);

                    b.Property<string>("AddressPostalCode")
                        .HasColumnName("address_postal_code")
                        .HasMaxLength(255);

                    b.Property<short?>("AuthLevel")
                        .HasColumnName("auth_level");

                    b.Property<string>("BankAccount")
                        .HasColumnName("bank_account")
                        .HasMaxLength(255);

                    b.Property<string>("Banned")
                        .HasColumnName("banned")
                        .HasMaxLength(1);

                    b.Property<string>("BicCode")
                        .HasColumnName("bic_code")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnName("birthdate")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasMaxLength(255);

                    b.Property<int?>("Engagement")
                        .HasColumnName("engagement");

                    b.Property<string>("FacebookLogin")
                        .HasColumnName("facebook_login")
                        .HasMaxLength(255);

                    b.Property<string>("FamilyName")
                        .HasColumnName("family_name")
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name")
                        .HasMaxLength(255);

                    b.Property<string>("Gender")
                        .HasColumnName("gender")
                        .HasMaxLength(1);

                    b.Property<string>("GoogleLogin")
                        .HasColumnName("google_login")
                        .HasMaxLength(255);

                    b.Property<int?>("IsAccountActive")
                        .HasColumnName("is_account_active");

                    b.Property<int?>("IsAgendaPublic")
                        .HasColumnName("is_agenda_public");

                    b.Property<int?>("IsProfilePublic")
                        .HasColumnName("is_profile_public");

                    b.Property<string>("JobSearchDescription")
                        .HasColumnName("job_search_description");

                    b.Property<string>("Language")
                        .HasColumnName("language")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnName("last_login")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnName("modified_at")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("NationalNumber")
                        .HasColumnName("national_number")
                        .HasMaxLength(255);

                    b.Property<string>("Nationality")
                        .HasColumnName("nationality")
                        .HasMaxLength(255);

                    b.Property<string>("OauthProvider")
                        .HasColumnName("oauth_provider")
                        .HasMaxLength(255);

                    b.Property<string>("OauthUid")
                        .HasColumnName("oauth_uid")
                        .HasMaxLength(255);

                    b.Property<string>("Passwd")
                        .HasColumnName("passwd")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("PasswdModifiedAt")
                        .HasColumnName("passwd_modified_at")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("PasswdRecoveryCode")
                        .HasColumnName("passwd_recovery_code")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("PasswdRecoveryDate")
                        .HasColumnName("passwd_recovery_date")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasMaxLength(255);

                    b.Property<string>("ProfilePicture")
                        .HasColumnName("profile_picture")
                        .HasMaxLength(255);

                    b.Property<string>("ShortIntroduction")
                        .HasColumnName("short_introduction");

                    b.Property<string>("TwitterLogin")
                        .HasColumnName("twitter_login")
                        .HasMaxLength(255);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("username")
                        .HasMaxLength(255);

                    b.Property<string>("VolunteerLocation")
                        .HasColumnName("volunteer_location")
                        .HasMaxLength(255);

                    b.Property<int?>("VolunteerLocationMaxDistance")
                        .HasColumnName("volunteer_location_max_distance");

                    b.Property<string>("YoutubeLogin")
                        .HasColumnName("youtube_login")
                        .HasMaxLength(255);

                    b.HasKey("UserId")
                        .HasName("PK_users_user_id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.UserContact", b =>
                {
                    b.Property<int>("UserContactsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_contacts_id");

                    b.Property<DateTime>("ContactDate")
                        .HasColumnName("contact_date")
                        .HasColumnType("datetime2(0)");

                    b.Property<int?>("FromUserId")
                        .HasColumnName("from_user_id");

                    b.Property<string>("Message")
                        .HasColumnName("message");

                    b.Property<int?>("OrgContactId")
                        .HasColumnName("org_contact_id");

                    b.Property<int?>("SuggestedVacancyId")
                        .HasColumnName("suggested_vacancy_id");

                    b.Property<int?>("ToUserId")
                        .HasColumnName("to_user_id");

                    b.HasKey("UserContactsId")
                        .HasName("user_contacts_id");

                    b.HasIndex("FromUserId")
                        .HasName("FK_Reference_49");

                    b.HasIndex("OrgContactId")
                        .HasName("FK_Reference_50");

                    b.HasIndex("SuggestedVacancyId")
                        .HasName("FK_Reference_52");

                    b.HasIndex("ToUserId")
                        .HasName("FK_Reference_51");

                    b.ToTable("user_contacts");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.UserMessage", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("message_id");

                    b.Property<int>("FromUserId")
                        .HasColumnName("from_user_id");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnName("message");

                    b.Property<DateTime?>("ReadDate")
                        .HasColumnName("read_date")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime>("SentDate")
                        .HasColumnName("sent_date")
                        .HasColumnType("datetime2(0)");

                    b.Property<int>("ToUserId")
                        .HasColumnName("to_user_id");

                    b.HasKey("MessageId")
                        .HasName("PK_user_messages_message_id");

                    b.HasIndex("FromUserId")
                        .HasName("FK_message_from_user_ref");

                    b.HasIndex("ToUserId")
                        .HasName("FK_message_to_user_ref");

                    b.ToTable("user_messages");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.UsernameOrEmailOnHold", b =>
                {
                    b.Property<long>("Ai")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ai");

                    b.Property<DateTime>("Time")
                        .HasColumnName("time")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("UsernameOrEmail")
                        .IsRequired()
                        .HasColumnName("username_or_email")
                        .HasMaxLength(255);

                    b.HasKey("Ai")
                        .HasName("PK_username_or_email_on_hold_ai");

                    b.ToTable("username_or_email_on_hold");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.UserProvider", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnName("provider")
                        .HasMaxLength(50);

                    b.Property<string>("ProviderUid")
                        .IsRequired()
                        .HasColumnName("provider_uid")
                        .HasMaxLength(255);

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("user_provider");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Vacancy", b =>
                {
                    b.Property<int>("VacancyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("vacancy_id");

                    b.Property<string>("AddressCapital")
                        .HasColumnName("address_capital")
                        .HasMaxLength(255);

                    b.Property<string>("AddressCity")
                        .HasColumnName("address_city")
                        .HasMaxLength(255);

                    b.Property<string>("AddressCountry")
                        .HasColumnName("address_country")
                        .HasMaxLength(255);

                    b.Property<string>("AddressLine1")
                        .HasColumnName("address_line_1")
                        .HasMaxLength(255);

                    b.Property<string>("AddressLine2")
                        .HasColumnName("address_line_2")
                        .HasMaxLength(255);

                    b.Property<string>("AddressPostalCode")
                        .HasColumnName("address_postal_code")
                        .HasMaxLength(255);

                    b.Property<string>("Banner")
                        .HasColumnName("banner")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnName("create_time")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(400);

                    b.Property<int?>("Engagement")
                        .HasColumnName("engagement");

                    b.Property<short>("FlexibleDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("flexible_date")
                        .HasDefaultValueSql("0");

                    b.Property<short>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("is_deleted")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Logo")
                        .HasColumnName("logo")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(255);

                    b.Property<int>("NumberRequired")
                        .HasColumnName("number_required");

                    b.Property<int?>("OccupancyKind")
                        .HasColumnName("occupancy_kind");

                    b.Property<string>("Offer")
                        .HasColumnName("offer")
                        .HasMaxLength(400);

                    b.Property<int>("OrgId")
                        .HasColumnName("org_id");

                    b.Property<DateTime?>("VacancyVisibilityEndDate")
                        .HasColumnName("vacancy_visibility_end_date")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("VacancyVisibilityStartDate")
                        .HasColumnName("vacancy_visibility_start_date")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Website")
                        .HasColumnName("website")
                        .HasMaxLength(255);

                    b.HasKey("VacancyId")
                        .HasName("PK_vacancies_vacancy_id");

                    b.HasIndex("OrgId")
                        .HasName("FK_Reference_23");

                    b.ToTable("vacancies");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.VacancyCalendar", b =>
                {
                    b.Property<int>("VacancyCalendarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("vacancy_calendar_id");

                    b.Property<DateTime?>("DateFrom")
                        .HasColumnName("date_from")
                        .HasColumnType("datetime2(0)");

                    b.Property<DateTime?>("DateTo")
                        .HasColumnName("date_to")
                        .HasColumnType("datetime2(0)");

                    b.Property<int?>("VacancyId")
                        .HasColumnName("vacancy_id");

                    b.HasKey("VacancyCalendarId");

                    b.HasIndex("VacancyId")
                        .HasName("FK_Reference_34");

                    b.ToTable("vacancy_calendar");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.VacancyInvitation", b =>
                {
                    b.Property<int>("InvitationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("invitation_id");

                    b.Property<DateTime?>("InvitationDate")
                        .HasColumnName("invitation_date")
                        .HasColumnType("datetime2(0)");

                    b.Property<string>("Message")
                        .HasColumnName("message");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasColumnType("char(10)");

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id");

                    b.Property<int?>("VacancyId")
                        .HasColumnName("vacancy_id");

                    b.HasKey("InvitationId")
                        .HasName("PK_vacancy_invitations_invitation_id");

                    b.HasIndex("UserId")
                        .HasName("FK_Reference_33");

                    b.HasIndex("VacancyId")
                        .HasName("FK_Reference_32");

                    b.ToTable("vacancy_invitations");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.VacancySubscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("subscription_id");

                    b.Property<string>("Message")
                        .HasColumnName("message");

                    b.Property<int?>("OrgRating")
                        .HasColumnName("org_rating");

                    b.Property<string>("OrgRatingComment")
                        .HasColumnName("org_rating_comment")
                        .HasMaxLength(255);

                    b.Property<int>("Status")
                        .HasColumnName("status");

                    b.Property<DateTime>("SubscriptionDate")
                        .HasColumnName("subscription_date")
                        .HasColumnType("datetime2(0)");

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id");

                    b.Property<int?>("VacancyId")
                        .HasColumnName("vacancy_id");

                    b.Property<int?>("VolunteerRating")
                        .HasColumnName("volunteer_rating");

                    b.Property<string>("VolunteerRatingComment")
                        .HasColumnName("volunteer_rating_comment")
                        .HasMaxLength(255);

                    b.HasKey("SubscriptionId")
                        .HasName("PK_vacancy_subscriptions_subscription_id");

                    b.HasIndex("UserId")
                        .HasName("FK_Reference_31");

                    b.HasIndex("VacancyId")
                        .HasName("FK_Reference_30");

                    b.ToTable("vacancy_subscriptions");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Education", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.User", "User")
                        .WithMany("Educations")
                        .HasForeignKey("UserId")
                        .HasConstraintName("educations$FK_user_education_ref");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Organization", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.OrganizationalAddress", "Address")
                        .WithMany("Organization")
                        .HasForeignKey("AddressId");

                    b.HasOne("GoedBezigWebApp.Models.Organization", "Parent")
                        .WithMany("InverseParent")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("organization$FK_org_child_ref");

                    b.HasOne("GoedBezigWebApp.Models.Structuur", "Structuur")
                        .WithMany("Organization")
                        .HasForeignKey("StructuurId")
                        .HasConstraintName("organization$FK_structuur_to_vacancy_ref");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.OrgContact", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.Organization", "Org")
                        .WithMany("OrgContacts")
                        .HasForeignKey("OrgId");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Profession", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.User", "User")
                        .WithMany("Professions")
                        .HasForeignKey("UserId")
                        .HasConstraintName("professions$FK_user_profession_ref");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.UserContact", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.User", "FromUser")
                        .WithMany("UserContactsFromUser")
                        .HasForeignKey("FromUserId")
                        .HasConstraintName("user_contacts$FK_Reference_49");

                    b.HasOne("GoedBezigWebApp.Models.OrgContact", "OrgContact")
                        .WithMany("UserContacts")
                        .HasForeignKey("OrgContactId")
                        .HasConstraintName("user_contacts$FK_Reference_50");

                    b.HasOne("GoedBezigWebApp.Models.Vacancy", "SuggestedVacancy")
                        .WithMany("UserContacts")
                        .HasForeignKey("SuggestedVacancyId")
                        .HasConstraintName("user_contacts$FK_Reference_52");

                    b.HasOne("GoedBezigWebApp.Models.User", "ToUser")
                        .WithMany("UserContactsToUser")
                        .HasForeignKey("ToUserId")
                        .HasConstraintName("user_contacts$FK_Reference_51");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.UserMessage", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.User", "FromUser")
                        .WithMany("UserMessagesFromUser")
                        .HasForeignKey("FromUserId")
                        .HasConstraintName("user_messages$FK_message_from_user_ref");

                    b.HasOne("GoedBezigWebApp.Models.User", "ToUser")
                        .WithMany("UserMessagesToUser")
                        .HasForeignKey("ToUserId")
                        .HasConstraintName("user_messages$FK_message_to_user_ref");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Vacancy", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.Organization", "Org")
                        .WithMany("Vacancies")
                        .HasForeignKey("OrgId")
                        .HasConstraintName("vacancies$FK_Reference_23");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.VacancyCalendar", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.Vacancy", "Vacancy")
                        .WithMany("VacancyCalendar")
                        .HasForeignKey("VacancyId")
                        .HasConstraintName("vacancy_calendar$FK_Reference_34");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.VacancyInvitation", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.User", "User")
                        .WithMany("VacancyInvitations")
                        .HasForeignKey("UserId")
                        .HasConstraintName("vacancy_invitations$FK_Reference_33");

                    b.HasOne("GoedBezigWebApp.Models.Vacancy", "Vacancy")
                        .WithMany("VacancyInvitations")
                        .HasForeignKey("VacancyId")
                        .HasConstraintName("vacancy_invitations$FK_Reference_32");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.VacancySubscription", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.User", "User")
                        .WithMany("VacancySubscriptions")
                        .HasForeignKey("UserId")
                        .HasConstraintName("vacancy_subscriptions$FK_Reference_31");

                    b.HasOne("GoedBezigWebApp.Models.Vacancy", "Vacancy")
                        .WithMany("VacancySubscriptions")
                        .HasForeignKey("VacancyId")
                        .HasConstraintName("vacancy_subscriptions$FK_Reference_30");
                });
        }
    }
}
