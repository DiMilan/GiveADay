using System;
using GoedBezigWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoedBezigWebApp.Data
{
    public partial class GoedBezigDbContext : DbContext
    {
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Interest> Interests { get; set; }
        public virtual DbSet<NewsSubscriber> NewsSubscribers { get; set; }
        public virtual DbSet<OrgContact> OrgContacts { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<OrganizationalAddress> OrganizationalAddresses { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Structuur> Structuur { get; set; }
        public virtual DbSet<UserContact> UserContacts { get; set; }
        public virtual DbSet<UserMessage> UserMessages { get; set; }
        public virtual DbSet<UserProvider> UserProvider { get; set; }
        public virtual DbSet<UsernameOrEmailOnHold> UsernameOrEmailOnHold { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vacancy> Vacancies { get; set; }
        public virtual DbSet<VacancyCalendar> VacancyCalendar { get; set; }
        public virtual DbSet<VacancyInvitation> VacancyInvitations { get; set; }
        public virtual DbSet<VacancySubscription> VacancySubscriptions { get; set; }
        public virtual DbSet<Group> Groups { get; set; }

        // Unable to generate entity type for table 'dbo.engagement'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.org_contact_for_addresses'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.organization_interests'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.user_calendar'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.user_interests'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.user_roles'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.user_saved_vacancies'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.user_skills'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.vacancy_contacts'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.vacancy_interests'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.vacancy_skills'. Please see the warning messages.

        public GoedBezigDbContext(DbContextOptions<GoedBezigDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasKey(e => e.EducationId)
                    .HasName("PK_educations_education_id");

                entity.ToTable("educations");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_user_education_ref");

                entity.Property(e => e.EducationId).HasColumnName("education_id");

                entity.Property(e => e.DateFrom)
                    .HasColumnName("date_from")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.DateTo)
                    .HasColumnName("date_to")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.School)
                    .HasColumnName("school")
                    .HasMaxLength(255);

                entity.Property(e => e.Specialization)
                    .HasColumnName("specialization")
                    .HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Educations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("educations$FK_user_education_ref");
            });

            modelBuilder.Entity<Interest>(entity =>
            {
                entity.HasKey(e => e.InterestId)
                    .HasName("PK_interests_interest_id");

                entity.ToTable("interests");

                entity.Property(e => e.InterestId).HasColumnName("interest_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.NameBrown)
                    .HasColumnName("name_brown")
                    .HasMaxLength(255);

                entity.Property(e => e.NameGrey)
                    .HasColumnName("name_grey")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<NewsSubscriber>(entity =>
            {
                entity.HasKey(e => e.SubscriberId)
                    .HasName("PK_news_subscribers_subscriber_id");

                entity.ToTable("news_subscribers");

                entity.Property(e => e.SubscriberId).HasColumnName("subscriber_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<OrgContact>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .HasName("PK_org_contacts_contact_id");

                entity.ToTable("org_contacts");

                entity.HasIndex(e => e.OrgId)
                    .HasName("FK_org_contacts_ref");

                entity.Property(e => e.ContactId).HasColumnName("contact_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.FamilyName)
                    .IsRequired()
                    .HasColumnName("family_name")
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(255);

                entity.Property(e => e.Function)
                    .HasColumnName("function")
                    .HasMaxLength(255);

                entity.Property(e => e.OrgId).HasColumnName("org_id");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.OrgContacts)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("org_contacts$FK_org_contacts_ref");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.OrgId)
                    .HasName("PK_organization_org_id");

                entity.ToTable("organization");

                entity.HasIndex(e => e.AddressId)
                    .HasName("FK_org_address_id_ref");

                entity.HasIndex(e => e.ParentId)
                    .HasName("FK_org_child_ref");

                entity.HasIndex(e => e.StructuurId)
                    .HasName("FK_structuur_to_vacancy_ref");

                entity.Property(e => e.OrgId).HasColumnName("org_id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Btw)
                    .HasColumnName("btw")
                    .HasMaxLength(50);

                entity.Property(e => e.ContactCounter)
                    .HasColumnName("contact_counter")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ContactDate)
                    .HasColumnName("contact_date")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(800);

                entity.Property(e => e.FacebookUrl)
                    .HasColumnName("facebook_url")
                    .HasMaxLength(255);

                entity.Property(e => e.GoogleUrl)
                    .HasColumnName("google_url")
                    .HasMaxLength(255);

                entity.Property(e => e.IdentificationNr)
                    .HasColumnName("identification_nr")
                    .HasMaxLength(255);

                entity.Property(e => e.InstagramUrl)
                    .HasColumnName("instagram_url")
                    .HasMaxLength(255);

                entity.Property(e => e.Logo)
                    .HasColumnName("logo")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.OrgType).HasColumnName("org_type");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.StructuurId).HasColumnName("structuur_id");

                entity.Property(e => e.TwitterUrl)
                    .HasColumnName("twitter_url")
                    .HasMaxLength(255);

                entity.Property(e => e.WebsiteUrl)
                    .HasColumnName("website_url")
                    .HasMaxLength(255);

                entity.Property(e => e.YoutubeUrl)
                    .HasColumnName("youtube_url")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Organization)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("organization$FK_org_address_id_ref");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("organization$FK_org_child_ref");

                entity.HasOne(d => d.Structuur)
                    .WithMany(p => p.Organization)
                    .HasForeignKey(d => d.StructuurId)
                    .HasConstraintName("organization$FK_structuur_to_vacancy_ref");
            });

            modelBuilder.Entity<OrganizationalAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK_organizational_addresses_address_id");

                entity.ToTable("organizational_addresses");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.AddressCapital)
                    .HasColumnName("address_capital")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressCity)
                    .HasColumnName("address_city")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressCountry)
                    .IsRequired()
                    .HasColumnName("address_country")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressLine1)
                    .HasColumnName("address_line_1")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("address_line_2")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressPostalCode)
                    .HasColumnName("address_postal_code")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Profession>(entity =>
            {
                entity.HasKey(e => e.ProfessionId)
                    .HasName("PK_professions_profession_id");

                entity.ToTable("professions");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_user_profession_ref");

                entity.Property(e => e.ProfessionId).HasColumnName("profession_id");

                entity.Property(e => e.DateFrom)
                    .HasColumnName("date_from")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.DateTo)
                    .HasColumnName("date_to")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("profession")
                    .HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Professions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("professions$FK_user_profession_ref");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_roles_role_id");

                entity.ToTable("roles");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => e.SkillId)
                    .HasName("PK_skills_skill_id");

                entity.ToTable("skills");

                entity.Property(e => e.SkillId).HasColumnName("skill_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Structuur>(entity =>
            {
                entity.ToTable("structuur");

                entity.Property(e => e.StructuurId).HasColumnName("structuur_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UserContact>(entity =>
            {
                entity.HasKey(e => e.UserContactsId)
                    .HasName("user_contacts_id");

                entity.ToTable("user_contacts");

                entity.HasIndex(e => e.FromUserId)
                    .HasName("FK_Reference_49");

                entity.HasIndex(e => e.OrgContactId)
                    .HasName("FK_Reference_50");

                entity.HasIndex(e => e.SuggestedVacancyId)
                    .HasName("FK_Reference_52");

                entity.HasIndex(e => e.ToUserId)
                    .HasName("FK_Reference_51");

                entity.Property(e => e.UserContactsId).HasColumnName("user_contacts_id");

                entity.Property(e => e.ContactDate)
                    .HasColumnName("contact_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.FromUserId).HasColumnName("from_user_id");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.OrgContactId).HasColumnName("org_contact_id");

                entity.Property(e => e.SuggestedVacancyId).HasColumnName("suggested_vacancy_id");

                entity.Property(e => e.ToUserId).HasColumnName("to_user_id");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.UserContactsFromUser)
                    .HasForeignKey(d => d.FromUserId)
                    .HasConstraintName("user_contacts$FK_Reference_49");

                entity.HasOne(d => d.OrgContact)
                    .WithMany(p => p.UserContacts)
                    .HasForeignKey(d => d.OrgContactId)
                    .HasConstraintName("user_contacts$FK_Reference_50");

                entity.HasOne(d => d.SuggestedVacancy)
                    .WithMany(p => p.UserContacts)
                    .HasForeignKey(d => d.SuggestedVacancyId)
                    .HasConstraintName("user_contacts$FK_Reference_52");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.UserContactsToUser)
                    .HasForeignKey(d => d.ToUserId)
                    .HasConstraintName("user_contacts$FK_Reference_51");
            });

            modelBuilder.Entity<UserMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK_user_messages_message_id");

                entity.ToTable("user_messages");

                entity.HasIndex(e => e.FromUserId)
                    .HasName("FK_message_from_user_ref");

                entity.HasIndex(e => e.ToUserId)
                    .HasName("FK_message_to_user_ref");

                entity.Property(e => e.MessageId).HasColumnName("message_id");

                entity.Property(e => e.FromUserId).HasColumnName("from_user_id");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message");

                entity.Property(e => e.ReadDate)
                    .HasColumnName("read_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.SentDate)
                    .HasColumnName("sent_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ToUserId).HasColumnName("to_user_id");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.UserMessagesFromUser)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("user_messages$FK_message_from_user_ref");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.UserMessagesToUser)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("user_messages$FK_message_to_user_ref");
            });

            modelBuilder.Entity<UserProvider>(entity =>
            {
                entity.ToTable("user_provider");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Provider)
                    .IsRequired()
                    .HasColumnName("provider")
                    .HasMaxLength(50);

                entity.Property(e => e.ProviderUid)
                    .IsRequired()
                    .HasColumnName("provider_uid")
                    .HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<UsernameOrEmailOnHold>(entity =>
            {
                entity.HasKey(e => e.Ai)
                    .HasName("PK_username_or_email_on_hold_ai");

                entity.ToTable("username_or_email_on_hold");

                entity.Property(e => e.Ai).HasColumnName("ai");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.UsernameOrEmail)
                    .IsRequired()
                    .HasColumnName("username_or_email")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_users_user_id");

                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AddressCapital)
                    .HasColumnName("address_capital")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressCity)
                    .HasColumnName("address_city")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressCountry)
                    .HasColumnName("address_country")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressLine1)
                    .HasColumnName("address_line1")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("address_line2")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressPostalCode)
                    .HasColumnName("address_postal_code")
                    .HasMaxLength(255);

                entity.Property(e => e.AuthLevel).HasColumnName("auth_level");

                entity.Property(e => e.BankAccount)
                    .HasColumnName("bank_account")
                    .HasMaxLength(255);

                entity.Property(e => e.Banned)
                    .HasColumnName("banned")
                    .HasMaxLength(1);

                entity.Property(e => e.BicCode)
                    .HasColumnName("bic_code")
                    .HasMaxLength(255);

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.Engagement).HasColumnName("engagement");

                entity.Property(e => e.FacebookLogin)
                    .HasColumnName("facebook_login")
                    .HasMaxLength(255);

                entity.Property(e => e.FamilyName)
                    .HasColumnName("family_name")
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(255);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(1);

                entity.Property(e => e.GoogleLogin)
                    .HasColumnName("google_login")
                    .HasMaxLength(255);

                entity.Property(e => e.IsAccountActive).HasColumnName("is_account_active");

                entity.Property(e => e.IsAgendaPublic).HasColumnName("is_agenda_public");

                entity.Property(e => e.IsProfilePublic).HasColumnName("is_profile_public");

                entity.Property(e => e.JobSearchDescription).HasColumnName("job_search_description");

                entity.Property(e => e.Language)
                    .HasColumnName("language")
                    .HasMaxLength(255);

                entity.Property(e => e.LastLogin)
                    .HasColumnName("last_login")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnName("modified_at")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.NationalNumber)
                    .HasColumnName("national_number")
                    .HasMaxLength(255);

                entity.Property(e => e.Nationality)
                    .HasColumnName("nationality")
                    .HasMaxLength(255);

                entity.Property(e => e.OauthProvider)
                    .HasColumnName("oauth_provider")
                    .HasMaxLength(255);

                entity.Property(e => e.OauthUid)
                    .HasColumnName("oauth_uid")
                    .HasMaxLength(255);

                entity.Property(e => e.Passwd)
                    .HasColumnName("passwd")
                    .HasMaxLength(255);

                entity.Property(e => e.PasswdModifiedAt)
                    .HasColumnName("passwd_modified_at")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.PasswdRecoveryCode)
                    .HasColumnName("passwd_recovery_code")
                    .HasMaxLength(255);

                entity.Property(e => e.PasswdRecoveryDate)
                    .HasColumnName("passwd_recovery_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(255);

                entity.Property(e => e.ProfilePicture)
                    .HasColumnName("profile_picture")
                    .HasMaxLength(255);

                entity.Property(e => e.ShortIntroduction).HasColumnName("short_introduction");

                entity.Property(e => e.TwitterLogin)
                    .HasColumnName("twitter_login")
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(255);

                entity.Property(e => e.VolunteerLocation)
                    .HasColumnName("volunteer_location")
                    .HasMaxLength(255);

                entity.Property(e => e.VolunteerLocationMaxDistance).HasColumnName("volunteer_location_max_distance");

                entity.Property(e => e.YoutubeLogin)
                    .HasColumnName("youtube_login")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Vacancy>(entity =>
            {
                entity.HasKey(e => e.VacancyId)
                    .HasName("PK_vacancies_vacancy_id");

                entity.ToTable("vacancies");

                entity.HasIndex(e => e.OrgId)
                    .HasName("FK_Reference_23");

                entity.Property(e => e.VacancyId).HasColumnName("vacancy_id");

                entity.Property(e => e.AddressCapital)
                    .HasColumnName("address_capital")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressCity)
                    .HasColumnName("address_city")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressCountry)
                    .HasColumnName("address_country")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressLine1)
                    .HasColumnName("address_line_1")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("address_line_2")
                    .HasMaxLength(255);

                entity.Property(e => e.AddressPostalCode)
                    .HasColumnName("address_postal_code")
                    .HasMaxLength(255);

                entity.Property(e => e.Banner)
                    .HasColumnName("banner")
                    .HasMaxLength(255);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(400);

                entity.Property(e => e.Engagement).HasColumnName("engagement");

                entity.Property(e => e.FlexibleDate)
                    .HasColumnName("flexible_date")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Logo)
                    .HasColumnName("logo")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.NumberRequired).HasColumnName("number_required");

                entity.Property(e => e.OccupancyKind).HasColumnName("occupancy_kind");

                entity.Property(e => e.Offer)
                    .HasColumnName("offer")
                    .HasMaxLength(400);

                entity.Property(e => e.OrgId).HasColumnName("org_id");

                entity.Property(e => e.VacancyVisibilityEndDate)
                    .HasColumnName("vacancy_visibility_end_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.VacancyVisibilityStartDate)
                    .HasColumnName("vacancy_visibility_start_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.Website)
                    .HasColumnName("website")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.Vacancies)
                    .HasForeignKey(d => d.OrgId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("vacancies$FK_Reference_23");
            });

            modelBuilder.Entity<VacancyCalendar>(entity =>
            {
                entity.ToTable("vacancy_calendar");

                entity.HasIndex(e => e.VacancyId)
                    .HasName("FK_Reference_34");

                entity.Property(e => e.VacancyCalendarId).HasColumnName("vacancy_calendar_id");

                entity.Property(e => e.DateFrom)
                    .HasColumnName("date_from")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.DateTo)
                    .HasColumnName("date_to")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.VacancyId).HasColumnName("vacancy_id");

                entity.HasOne(d => d.Vacancy)
                    .WithMany(p => p.VacancyCalendar)
                    .HasForeignKey(d => d.VacancyId)
                    .HasConstraintName("vacancy_calendar$FK_Reference_34");
            });

            modelBuilder.Entity<VacancyInvitation>(entity =>
            {
                entity.HasKey(e => e.InvitationId)
                    .HasName("PK_vacancy_invitations_invitation_id");

                entity.ToTable("vacancy_invitations");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_Reference_33");

                entity.HasIndex(e => e.VacancyId)
                    .HasName("FK_Reference_32");

                entity.Property(e => e.InvitationId).HasColumnName("invitation_id");

                entity.Property(e => e.InvitationDate)
                    .HasColumnName("invitation_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("char(10)");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.VacancyId).HasColumnName("vacancy_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VacancyInvitations)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("vacancy_invitations$FK_Reference_33");

                entity.HasOne(d => d.Vacancy)
                    .WithMany(p => p.VacancyInvitations)
                    .HasForeignKey(d => d.VacancyId)
                    .HasConstraintName("vacancy_invitations$FK_Reference_32");
            });

            modelBuilder.Entity<VacancySubscription>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId)
                    .HasName("PK_vacancy_subscriptions_subscription_id");

                entity.ToTable("vacancy_subscriptions");

                entity.HasIndex(e => e.UserId)
                    .HasName("FK_Reference_31");

                entity.HasIndex(e => e.VacancyId)
                    .HasName("FK_Reference_30");

                entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.OrgRating).HasColumnName("org_rating");

                entity.Property(e => e.OrgRatingComment)
                    .HasColumnName("org_rating_comment")
                    .HasMaxLength(255);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubscriptionDate)
                    .HasColumnName("subscription_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.VacancyId).HasColumnName("vacancy_id");

                entity.Property(e => e.VolunteerRating).HasColumnName("volunteer_rating");

                entity.Property(e => e.VolunteerRatingComment)
                    .HasColumnName("volunteer_rating_comment")
                    .HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VacancySubscriptions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("vacancy_subscriptions$FK_Reference_31");

                entity.HasOne(d => d.Vacancy)
                    .WithMany(p => p.VacancySubscriptions)
                    .HasForeignKey(d => d.VacancyId)
                    .HasConstraintName("vacancy_subscriptions$FK_Reference_30");
            });
            modelBuilder.Entity<Group>(MapGroup);
        }

        private static void MapGroup(EntityTypeBuilder<Group> g)
        {
            g.ToTable("groups");

            g.HasKey(gr => gr.GroupId);

            g.Property(t => t.Name)
                .HasColumnName("GroupName")
                .IsRequired()
                .HasMaxLength(100);

            g.Property(t => t.Timestamp)
                .HasColumnName("CreationTime")
                .IsRequired();

        }
    }
}