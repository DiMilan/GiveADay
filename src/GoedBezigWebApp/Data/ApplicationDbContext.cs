using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GoedBezigWebApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoedBezigWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationalAddress> OrganizationalAddresses { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ForSqlServerToTable("users");
            modelBuilder.Entity<Role>().ForSqlServerToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ForSqlServerToTable("user_roles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ForSqlServerToTable("user_logins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ForSqlServerToTable("user_claims");
            modelBuilder.Entity<IdentityUserToken<string>>().ForSqlServerToTable("user_tokens");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ForSqlServerToTable("role_claims");

            MapUser(modelBuilder.Entity<User>().ForSqlServerToTable("users"));
            MapRole(modelBuilder.Entity<Role>().ForSqlServerToTable("roles"));
            MapGroup(modelBuilder.Entity<Group>());
            MapOrganization(modelBuilder.Entity<Organization>());
            MapOrganizationalAddress(modelBuilder.Entity<OrganizationalAddress>());
            MapUserGroup(modelBuilder.Entity<UserGroup>());
        }

        private static void MapUser(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.Id).HasColumnName("user_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone");
            entity.Property(e => e.UserName).HasColumnName("username");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.FamilyName).HasColumnName("family_name");
        }

        private static void MapRole(EntityTypeBuilder<Role> entity)
        {
            entity.Property(e => e.Id).HasColumnName("role_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
        }

        private static void MapGroup(EntityTypeBuilder<Group> g)
        {
            g.ToTable("groups");

            g.HasKey(gr => gr.Name);

            g.Property(t => t.Name)
                .HasColumnName("GroupName")
                .IsRequired()
                .HasMaxLength(100);

            g.Property(t => t.Timestamp)
                .HasColumnName("CreationTime")
                .IsRequired();

            g.Property(t => t.Motivation)
                .HasColumnName("Motivatie")
                .HasMaxLength(1000);

        }

        private static void MapOrganization(EntityTypeBuilder<Organization> entity)
        {
            entity.HasKey(e => e.OrgId)
                .HasName("PK_organization_org_id");

            entity.ToTable("organization");

            entity.HasIndex(e => e.AddressId)
                .HasName("FK_org_address_id_ref");

            entity.Property(e => e.OrgId).HasColumnName("org_id");

            entity.Property(e => e.AddressId).HasColumnName("address_id");

            entity.Property(e => e.Btw)
                .HasColumnName("btw")
                .HasMaxLength(50);

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(800);

            entity.Property(e => e.Logo)
                .HasColumnName("logo")
                .HasMaxLength(255);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(255);

            entity.HasOne(d => d.Address)
                .WithMany(p => p.Organization)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("organization$FK_org_address_id_ref");
        }

        private static void MapOrganizationalAddress(EntityTypeBuilder<OrganizationalAddress> entity)
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
        }

        private static void MapUserGroup(EntityTypeBuilder<UserGroup> ug)
        {
            ug.ToTable("user_groups");
            ug.HasKey(t => t.UserGroupId);
            ug.HasOne(t => t.User)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            ug.HasOne(t => t.Group)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
