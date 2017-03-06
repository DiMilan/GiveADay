using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoedBezigWebApp.Data
{
    public static class ApplicationDbInitializer
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            // Seed entity data in correct order!
            EnsureSeedOrganizationalAddresses(context);
            EnsureSeedOrganizations(context);
            EnsureSeedGroups(context);
//            EnsureSeedRoles(context).Wait();
            EnsureSeedUsers(context).Wait();
        }

        private static void EnsureSeedOrganizationalAddresses(ApplicationDbContext context)
        {
            // --> SEED Organizational Addresses
            if (context.OrganizationalAddresses.Any()) return;

            var organizationalAddresses = new OrganizationalAddress[]
            {
                new OrganizationalAddress()
                {
                    AddressCity = "Gent",
                    AddressCountry = "Belgium",
                    AddressLine1 = "Voskenslaan 270",
                    AddressPostalCode = "9000"
                },
                new OrganizationalAddress()
                {
                    AddressCity = "Gent",
                    AddressCountry = "Belgium",
                    AddressLine1 = "St. Pietersnieuwstraat 33",
                    AddressPostalCode = "9000"
                },
                new OrganizationalAddress()
                {
                    AddressCity = "Brussel",
                    AddressCountry = "Belgium",
                    AddressLine1 = "Franklin Rooseveltlaan 42",
                    AddressPostalCode = "1050"
                },
            };

            foreach (var oa in organizationalAddresses)
            {
                context.OrganizationalAddresses.Add(oa);
            }

            context.SaveChanges();
        }

        private static void EnsureSeedOrganizations(ApplicationDbContext context)
        {
            // --> SEED Organizations

            if (context.Organizations.Any()) return;

            var organizations = new Organization[]
            {
                new Organization
                {
                    Name = "HoGent",
                    Logo = "https://upload.wikimedia.org/wikipedia/commons/1/10/HoGent_Logo.png",
                    Btw = "BE012345678901",
                    Description = "University College in Ghent",
                    AddressId = 1
                },
                new Organization
                {
                    Name = "UGent",
                    Logo = "https://webster.ugent.be/alumnivacatures/invoeren/static/images/logo-ugent_org.svg",
                    Btw = "BE022344678901",
                    Description = "University Ghent",
                    AddressId = 2
                },
                new Organization
                {
                    Name = "Solvay Economics & Management",
                    Logo = "https://lh4.googleusercontent.com/-RuXL76SEWQQ/AAAAAAAAAAI/AAAAAAAAAG4/vplxDplhGmM/s0-c-k-no-ns/photo.jpg",
                    Btw = "BE827638900032",
                    Description = "Solvay offers programs for careers in management, finance, economics, marketing & more...",
                    AddressId = 3
                }
            };

            foreach (var o in organizations)
            {
                context.Organizations.Add(o);
            }

            context.SaveChanges();
        }

        private static void EnsureSeedGroups(ApplicationDbContext context)
        {
            // --> SEED Groups
            if (context.Groups.Any()) return;

            var groups = new Group[]
            {
                // DateTime today = DateTime.Today;  // de bedoeling was om de datum van vandaag op te halen maar voor de ene of andere reden werkt het niet
                // Hier per ongeluk ook een apckegae system.runtime toegevoegd maar vind niet direct terug waar ik het moet verwijderen. 
                new Group
                {
                    Name = "2017Groep1",
                    Timestamp = DateTime.Now,
                    ClosedGroup = true
                },
                new Group
                {
                    Name = "2017Groep2",
                    Timestamp = DateTime.UtcNow,
                    ClosedGroup = true
                },
                new Group
                {
                    Name = "2017Groep3",
                    Timestamp = DateTime.MinValue,
                    ClosedGroup = true
                },
                new Group
                {
                    Name = "Test",
                    Timestamp = DateTime.MaxValue,
                    ClosedGroup = true
                },
            };

            foreach (var g in groups)
            {
                context.Groups.Add(g);
            }

            context.SaveChanges();
        }

        private static async Task EnsureSeedRoles(ApplicationDbContext context)
        {
            // --> SEED Roles
            if (context.Roles.Any()) return;

            foreach (var role in Roles)
            {
                var roleStore = new ApplicationRoleStore(context);

                await roleStore.CreateAsync(role);
            }

            await context.SaveChangesAsync();
        }

        private static async Task EnsureSeedUsers(ApplicationDbContext context)
        {
            // --> SEED Users
            foreach (var user in Users)
            {
                var passwordHasher = new PasswordHasher<User>();

                user.PasswordHash = passwordHasher.HashPassword(user, DefaultPassword);

                var userStore = new ApplicationUserStore(context);

                await userStore.CreateAsync(user);

                // TODO Fix mapping roles to users
                //foreach (var role in UserRoles[user])
                //{
                //    await userStore.AddToRoleAsync(user, role.ToUpper());
                //}
                //await userStore.AddToRoleAsync(user, UserRoles[user]);
                //await userManager.AddToRolesAsync(user, UserRoles[user]);
            }

            await context.SaveChangesAsync();
        }

        #region UserData

        private const string DefaultPassword = "test";

        private const string RoleNameVrijwilliger = "vrijwilliger";
        private const string RoleNameCursist = "cursist";

        private static readonly Role RoleVrijwilliger = new Role(RoleNameVrijwilliger);
        private static readonly Role RoleCursist = new Role(RoleNameCursist);

        private static readonly Role[] Roles = new Role[] { RoleVrijwilliger, RoleCursist };

        private static readonly User UserTest = GenerateTestUser("test", "test.be");
        private static readonly User UserVrijwilliger = GenerateTestUser("vrijwilliger", "test.be");
        private static readonly User UserCursist = GenerateTestUser("cursist", "test.be");
        private static readonly User UserMilan = GenerateTestUser("milandimax", "gmail.com", "Milan", "Dima");
        private static readonly User UserTom = GenerateTestUser("tom", "vdbussche.net", "Tom", "Vandenbussche");
        private static readonly User UserMax = GenerateTestUser("max.devloo", "lightspeedhq.com", "Maximiliaan", "Devloo");
        private static readonly User UserBart = GenerateTestUser("bartjevm", "gmail.com", "Bart", "Vanmarcke");

        private static readonly User[] Users = new User[] { UserTest, UserVrijwilliger, UserCursist, UserMilan, UserTom, UserMax, UserBart };

        private static readonly Dictionary<User, string[]> UserRoles = new Dictionary<User, string[]>()
        {
            { UserTest, new string[] {} },
            { UserVrijwilliger, new string[] { RoleNameVrijwilliger } },
            { UserCursist, new string[] { RoleNameCursist } },
            { UserMilan, new string[] {} },
            { UserTom, new string[] {} },
            { UserMax, new string[] {} },
            { UserBart, new string[] {} },
        };

        #endregion

        #region User Helper Methods

        private static User GenerateTestUser(string username, string domain)
        {
            return GenerateTestUser(username, domain, username, "test");
        }

        private static User GenerateTestUser(string username, string domain, string name, string lastName)
        {
            var email = string.Format("{0}@{1}", username, domain);
            return new User()
            {
                FirstName = name,
                FamilyName = lastName,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                LockoutEnabled = true
            };
        }

        #endregion
    }
}