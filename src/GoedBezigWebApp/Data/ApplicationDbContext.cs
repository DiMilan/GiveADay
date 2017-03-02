using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GoedBezigWebApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoedBezigWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ForSqlServerToTable("users");
            modelBuilder.Entity<IdentityRole>().ForSqlServerToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ForSqlServerToTable("user_roles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ForSqlServerToTable("user_logins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ForSqlServerToTable("user_claims");
            modelBuilder.Entity<IdentityUserToken<string>>().ForSqlServerToTable("user_tokens");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ForSqlServerToTable("role_claims");

            MapUser(modelBuilder.Entity<User>().ForSqlServerToTable("users"));
            MapRole(modelBuilder.Entity<Role>().ForSqlServerToTable("roles"));
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
    }
}
