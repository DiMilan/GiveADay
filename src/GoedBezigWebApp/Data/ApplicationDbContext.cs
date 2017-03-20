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
        public virtual DbSet<GbOrganization> GbOrganizations { get; set; }
        public virtual DbSet<ExternalOrganization> ExternalOrganizations { get; set; }
        public virtual DbSet<OrganizationContact> OrganizationContacts { get; set; }
        public virtual DbSet<OrganizationalAddress> OrganizationalAddresses { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityTask> ActivityTasks { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            MapIdentity(modelBuilder);
            MapUser(modelBuilder.Entity<User>().ForSqlServerToTable("users"));
            MapRole(modelBuilder.Entity<Role>().ForSqlServerToTable("roles"));
            MapGroup(modelBuilder.Entity<Group>());
            MapOrganization(modelBuilder.Entity<Organization>());
            MapGBOrganization(modelBuilder.Entity<GbOrganization>());
            MapExternalOrganization(modelBuilder.Entity<ExternalOrganization>());
            MapOrganizationContact(modelBuilder.Entity<OrganizationContact>());
            MapOrganizationalAddress(modelBuilder.Entity<OrganizationalAddress>());
            MapUserGroup(modelBuilder.Entity<Invitation>());
            MapActivity(modelBuilder.Entity<Activity>());
            MapActivityTasks(modelBuilder.Entity<ActivityTask>());
            MapActivityTasksUser(modelBuilder.Entity<ActivityTaskUser>());
            MapEvent(modelBuilder.Entity<Event>());
            MapMessage(modelBuilder.Entity<Message>());
        }


        private static void MapIdentity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ForSqlServerToTable("users");
            modelBuilder.Entity<Role>().ForSqlServerToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ForSqlServerToTable("user_roles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ForSqlServerToTable("user_logins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ForSqlServerToTable("user_claims");
            modelBuilder.Entity<IdentityUserToken<string>>().ForSqlServerToTable("user_tokens");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ForSqlServerToTable("role_claims");

        }


        private static void MapUser(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.Id).HasColumnName("user_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone");
            entity.Property(e => e.UserName).HasColumnName("username");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.FamilyName).HasColumnName("family_name");

            entity.HasMany(u => u.Invitations).WithOne(i => i.User);
            entity.HasOne(u => u.Organization).WithMany(o => o.Users);
            entity.HasOne(o => o.LectorUser).WithMany();
            entity.HasMany(o => o.ActivityTaskUsers).WithOne(o => o.User);
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

            g.HasKey(gr => gr.GroupName);

            g.Property(t => t.GroupName)
                .HasColumnName("GroupName")
                .IsRequired()
                .HasMaxLength(100);

            g.Property(t => t.Timestamp)
                .HasColumnName("CreationTime")
                .IsRequired();

            g.Property(t => t.Motivation)
                .HasColumnName("Motivatie")
                .HasMaxLength(10000);

            g.Property(p => p.StateType)
                .HasColumnName("GroupState");

            g.HasOne(p => p.GbOrganization)
                .WithMany(a => a.Groups)
                .IsRequired(false);


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

        private static void MapGBOrganization(EntityTypeBuilder<GbOrganization> entity)
        {
            entity.HasMany(e => e.Users).WithOne(u => u.Organization);
        }

        private static void MapExternalOrganization(EntityTypeBuilder<ExternalOrganization> entity)
        {
           entity.HasMany(o => o.Contacts).WithOne(e => e.Organization).OnDelete(DeleteBehavior.Cascade);
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

        private static void MapOrganizationContact(EntityTypeBuilder<OrganizationContact> entity)
        {
            entity.HasKey(e => e.ContactId)
                .HasName("PK_organization_contacts_contact_id");

            entity.ToTable("organization_contacts");

            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.HasOne(e => e.Organization).WithMany(o => o.Contacts).IsRequired();
        }

        private static void MapUserGroup(EntityTypeBuilder<Invitation> ug)
        {
            ug.ToTable("user_groups");
            ug.HasKey(t => new { t.UserId, t.GroupId });
            ug.HasOne(t => t.User)
                .WithMany(u => u.Invitations)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            ug.HasOne(t => t.Group)
                .WithMany(g => g.Invitations)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void MapActivity(EntityTypeBuilder<Activity> type)
        {
            type.ToTable("events");
            type.HasKey(e => e.Id);
            type.Property(e => e.Title).IsRequired();
            type.Property(e => e.Description).IsRequired();
            type.HasDiscriminator<string>("Type")
                .HasValue<Event>("Event")
                .HasValue<Activity>("Activity");
            type.HasMany(t => t.Messages).WithOne(t => t.Activity);
        }

        private void MapActivityTasks(EntityTypeBuilder<ActivityTask> at)
        {
            at.HasKey(k => k.Id);
            at.Property(p => p.Id).ValueGeneratedOnAdd();

            at.Property(p => p.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasMaxLength(255);

            at.Property(p => p.FromDateTime)
                .IsRequired()
                .HasColumnName("fromDateTime");

            at.Property(p => p.ToDateTime)
                .IsRequired()
                .HasColumnName("toDateTime");
            at.HasOne(o => o.Activity)
                .WithMany()
                .HasForeignKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

        }

        private void MapActivityTasksUser(EntityTypeBuilder<ActivityTaskUser> atu)
        {
            atu.HasKey(x => new {x.UserId, x.ActivityTaskId});

            atu.HasOne(o => o.User)
                .WithMany(m => m.ActivityTaskUsers)
                .HasForeignKey(fk => fk.UserId);

            atu.HasOne(o => o.ActivityTask)
                .WithMany(m => m.ActivityTaskUsers)
                .HasForeignKey(fk => fk.ActivityTaskId);
        }

        private static void MapEvent(EntityTypeBuilder<Event> type)
        {
            type.Property(t => t.Date).HasAnnotation("BackingField", "Date");
        }

        private static void MapMessage(EntityTypeBuilder<Message> type)
        {
            type.ToTable("messages");
            type.HasKey(m => m.Id);
            type.Property(m => m.Content).IsRequired();
            type.Property(m => m.Time).IsRequired();
            type.HasOne(m => m.Activity)
                .WithMany(e => e.Messages)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
