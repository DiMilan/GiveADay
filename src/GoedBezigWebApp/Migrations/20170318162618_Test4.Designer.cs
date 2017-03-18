using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GoedBezigWebApp.Data;
using GoedBezigWebApp.Models;

namespace GoedBezigWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170318162618_Test4")]
    partial class Test4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GoedBezigWebApp.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Accepted");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("GroupName")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GroupName");

                    b.ToTable("events");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Group", b =>
                {
                    b.Property<string>("GroupName")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("GroupName")
                        .HasMaxLength(100);

                    b.Property<bool>("ClosedGroup");

                    b.Property<string>("CompanyAddress");

                    b.Property<string>("CompanyContactEmail");

                    b.Property<string>("CompanyContactName");

                    b.Property<string>("CompanyContactSurname");

                    b.Property<string>("CompanyContactTitle");

                    b.Property<string>("CompanyEmail");

                    b.Property<string>("CompanyName");

                    b.Property<string>("CompanyWebsite");

                    b.Property<int?>("GBOrganizationOrgId");

                    b.Property<string>("Motivation")
                        .HasColumnName("Motivatie")
                        .HasMaxLength(1000);

                    b.Property<int>("StateType")
                        .HasColumnName("MotivationStatus");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnName("CreationTime");

                    b.HasKey("GroupName");

                    b.HasIndex("GBOrganizationOrgId");

                    b.ToTable("groups");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Invitation", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("GroupId");

                    b.Property<int>("Status");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("user_groups");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<int?>("EventId")
                        .IsRequired();

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("messages");
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

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasMaxLength(800);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Domain");

                    b.Property<string>("Logo")
                        .HasColumnName("logo")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(255);

                    b.HasKey("OrgId")
                        .HasName("PK_organization_org_id");

                    b.HasIndex("AddressId")
                        .HasName("FK_org_address_id_ref");

                    b.ToTable("organization");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Organization");
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

            modelBuilder.Entity("GoedBezigWebApp.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("role_id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasAnnotation("SqlServer:TableName", "roles");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FamilyName")
                        .HasColumnName("family_name");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name");

                    b.Property<string>("GroupName");

                    b.Property<string>("LectorUserId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<int?>("OrganizationOrgId");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("phone");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasColumnName("username")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("GroupName");

                    b.HasIndex("LectorUserId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("OrganizationOrgId");

                    b.ToTable("AspNetUsers");

                    b.HasAnnotation("SqlServer:TableName", "users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");

                    b.HasAnnotation("SqlServer:TableName", "role_claims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");

                    b.HasAnnotation("SqlServer:TableName", "user_claims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");

                    b.HasAnnotation("SqlServer:TableName", "user_logins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasAnnotation("SqlServer:TableName", "user_roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");

                    b.HasAnnotation("SqlServer:TableName", "user_tokens");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.GBOrganization", b =>
                {
                    b.HasBaseType("GoedBezigWebApp.Models.Organization");

                    b.Property<bool>("ClosedGroups");

                    b.ToTable("GBOrganization");

                    b.HasDiscriminator().HasValue("GBOrganization");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Event", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.Group", "Group")
                        .WithMany("Events")
                        .HasForeignKey("GroupName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Group", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.GBOrganization", "GBOrganization")
                        .WithMany("Groups")
                        .HasForeignKey("GBOrganizationOrgId");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Invitation", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.Group", "Group")
                        .WithMany("Invitations")
                        .HasForeignKey("GroupId");

                    b.HasOne("GoedBezigWebApp.Models.User", "User")
                        .WithMany("Invitations")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Message", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.Event", "Event")
                        .WithMany("Messages")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.Organization", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.OrganizationalAddress", "Address")
                        .WithMany("Organization")
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("GoedBezigWebApp.Models.User", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupName");

                    b.HasOne("GoedBezigWebApp.Models.User", "LectorUser")
                        .WithMany()
                        .HasForeignKey("LectorUserId");

                    b.HasOne("GoedBezigWebApp.Models.GBOrganization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationOrgId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("GoedBezigWebApp.Models.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GoedBezigWebApp.Models.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
