using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace GoedBezigWebApp.Data
{
    public class ApplicationDbInitializer
    {
        private readonly ApplicationDbContext _context;
        public ApplicationDbInitializer(ApplicationDbContext context)
        {
            _context = context;
        }
        public void SeedData()
        {

            // Seed entity data in correct order!
            //EnsureSeedOrganizationalAddresses();
            //EnsureSeedOrganizations();
            //EnsureSeedGroups();
            //            EnsureSeedRoles(context).Wait();
            EnsureSeedUsers().Wait();

            // Organisatie met associaties
            Organization hoGent = new Organization
            {
                Name = "HoGent",
                Logo = "https://upload.wikimedia.org/wikipedia/commons/1/10/HoGent_Logo.png",
                Btw = "BE012345678901",
                Description = "University College in Ghent",
                ClosedGroups = true,
                Address = new OrganizationalAddress()
                {
                    AddressCity = "Gent",
                    AddressCountry = "Belgium",
                    AddressLine1 = "Voskenslaan 270",
                    AddressPostalCode = "9000"
                }
            };
            _context.Organizations.Add(hoGent);
            UserTest.Organization = hoGent;
            _context.Users.Update(UserTest);
            Group test = hoGent.AddGroup("Test");
            _context.SaveChanges();
        }

        private void EnsureSeedOrganizationalAddresses()
        {
            // --> SEED Organizational Addresses
            if (_context.OrganizationalAddresses.Any()) return;

            var organizationalAddresses = new OrganizationalAddress[]
            {

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
                _context.OrganizationalAddresses.Add(oa);
            }

            _context.SaveChanges();
        }

        private void EnsureSeedOrganizations()
        {
            // --> SEED Organizations

            if (_context.Organizations.Any()) return;

            var organizations = new Organization[]
            {
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
                _context.Organizations.Add(o);
            }

            _context.SaveChanges();
        }

        private void EnsureSeedGroups()
        {
            // --> SEED Groups
            if (_context.Groups.Any()) return;

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
                _context.Groups.Add(g);
            }
            
            _context.SaveChanges();
        }

        private async Task EnsureSeedRoles()
        {
            // --> SEED Roles
            if (_context.Roles.Any()) return;

            foreach (var role in Roles)
            {
                var roleStore = new ApplicationRoleStore(_context);

                await roleStore.CreateAsync(role);
            }

            await _context.SaveChangesAsync();
        }

        private async Task EnsureSeedUsers()
        {
            // --> SEED Users
            foreach (var user in Users)
            {
                var passwordHasher = new PasswordHasher<User>();

                user.PasswordHash = passwordHasher.HashPassword(user, DefaultPassword);

                var userStore = new ApplicationUserStore(_context);

                await userStore.CreateAsync(user);

                // TODO Fix mapping roles to users
                //foreach (var role in UserRoles[user])
                //{
                //    await userStore.AddToRoleAsync(user, role.ToUpper());
                //}
                //await userStore.AddToRoleAsync(user, UserRoles[user]);
                //await userManager.AddToRolesAsync(user, UserRoles[user]);
            }

            await _context.SaveChangesAsync();
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
            { UserVrijwilliger, new string[] { RoleNameVrijwilliger } },
            { UserCursist, new string[] { RoleNameCursist } },
            { UserMilan, new string[] {} },
            { UserTom, new string[] {} },
            { UserMax, new string[] {} },
            { UserBart, new string[] {} },
            { UserTest, new string[] {} }
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
                LockoutEnabled = true,
            };
        }

        #endregion
    }
}