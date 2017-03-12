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
            EnsureSeedOrganizations();
            //            EnsureSeedRoles(context).Wait();
            EnsureSeedUsers().Wait();
            EnsureSeedGroups();
        }

        private void EnsureSeedOrganizations()
        {
            // --> SEED Organizations
            if (_context.Organizations.Any()) return;
            
                _context.Organizations.Add(HoGent);
                _context.Organizations.Add(UGent);
                _context.Organizations.Add(Solvay);
                _context.Organizations.Add(TestOrg);

            _context.SaveChanges();
        }

        #region Organization Data

        private static readonly Organization HoGent = new Organization
        {
            Name = "HoGent",
            Logo = "https://upload.wikimedia.org/wikipedia/commons/1/10/HoGent_Logo.png",
            Btw = "BE012345678901",
            Domain = "hogent.be",
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

        private static readonly Organization UGent = new Organization
        {
            Name = "UGent",
            Logo = "https://webster.ugent.be/alumnivacatures/invoeren/static/images/logo-ugent_org.svg",
            Btw = "BE022344678901",
            Domain = "ugent.be",
            Description = "University Ghent",
            Address = new OrganizationalAddress()
            {
                AddressCity = "Gent",
                AddressCountry = "Belgium",
                AddressLine1 = "St. Pietersnieuwstraat 33",
                AddressPostalCode = "9000"
            }
        };

        private static readonly Organization Solvay = new Organization
        {
            Name = "Solvay Economics & Management",
            Logo =
                "https://lh4.googleusercontent.com/-RuXL76SEWQQ/AAAAAAAAAAI/AAAAAAAAAG4/vplxDplhGmM/s0-c-k-no-ns/photo.jpg",
            Btw = "BE827638900032",
            Domain = "solvay.edu",
            Description = "Solvay offers programs for careers in management, finance, economics, marketing & more...",
            Address = new OrganizationalAddress()
            {
                AddressCity = "Brussel",
                AddressCountry = "Belgium",
                AddressLine1 = "Franklin Rooseveltlaan 42",
                AddressPostalCode = "1050"
            }
        };

        private static readonly Organization TestOrg = new Organization
        {
            Name = "Test University",
            Btw = "BE9876545678",
            Description = "Test University is the greatest University in the world. Come make Test great again.",
            Domain = "test.be",
            Address = new OrganizationalAddress()
            {
                AddressCity = "Washington",
                AddressCountry = "US",
                AddressLine1 = "White House Street 4",
                AddressPostalCode = "1000"
            }
        };

        #endregion

        private void EnsureSeedGroups()
        {
            // --> SEED Groups
            if (_context.Groups.Any()) return;

            var groupHogent = HoGent.AddGroup("Test1");
            //var groupUGent = UGent.AddGroup("Test2");
            //var groupSolvay = Solvay.AddGroup("Test3");

            _context.SaveChanges();

            _context.Invitations.Add(new Invitation(UserTest, groupHogent));
            //_context.Invitations.Add(new Invitation(UserTest, groupUGent));
            //_context.Invitations.Add(new Invitation(UserCursist, groupSolvay));

            _context.SaveChanges();

        }

        #region Group Data



        #endregion

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
            UserTest.Organization = HoGent;
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

        private void EnsureSeedInvitations()
        {
            
        }


    }
}