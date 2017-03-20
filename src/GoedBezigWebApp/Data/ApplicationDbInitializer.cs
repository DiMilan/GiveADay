using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoedBezigWebApp.Models;
using GoedBezigWebApp.Models.GroupState;
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
            EnsureSeedInvitations();
            EnsureSeedActivities();
        }

        private void EnsureSeedOrganizations()
        {
            // --> SEED Organizations
            if (_context.Organizations.Any()) return;

            KebabHouse.Contacts.Add(new OrganizationContact("Jan", "Janssens", "CFO", "jan.janssens@mdware.org", KebabHouse));
            KebabHouse.Contacts.Add(new OrganizationContact("Piet", "Pieters", "CEO", "piet.pieters@mdware.org", KebabHouse));

            _context.GbOrganizations.Add(HoGent);
            _context.GbOrganizations.Add(UGent);
            _context.GbOrganizations.Add(Solvay);
            _context.GbOrganizations.Add(TestOrg);
            _context.ExternalOrganizations.Add(KebabHouse);
            _context.ExternalOrganizations.Add(PitaHouse);
            _context.ExternalOrganizations.Add(PizzaRoma);

            _context.SaveChanges();
        }

        #region Organization Data

        private static readonly GbOrganization HoGent = new GbOrganization
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

        private static readonly GbOrganization UGent = new GbOrganization
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

        private static readonly GbOrganization Solvay = new GbOrganization
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

        private static readonly GbOrganization TestOrg = new GbOrganization
        {
            Name = "Test University",
            Logo = "http://users.hogent.be/~533031md/eportfolio/spost/images/cap.png",
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

        private ExternalOrganization KebabHouse = new ExternalOrganization
        {
            Name = "Kebab House",
            Logo = "https://eggthedail.files.wordpress.com/2010/08/100_2041.jpg",
            Btw = "BE045785456",
            Description = "Kebab schnijden is onze specialiteit. Mohammed zegt dat ook altijd. Ma zegt hij, zoals ik kebab...",
            HasGbLabel = false,
            Address = new OrganizationalAddress()
            {
                AddressCity = "Wevelgem",
                AddressCountry = "België",
                AddressLine1 = "Kortrijksestraat 32",
                AddressPostalCode = "8560"
            },
            Contacts = new List<OrganizationContact>()
        };

        private ExternalOrganization PitaHouse = new ExternalOrganization
        {
            Name = "Pita House",
            Logo = "http://www.pitahouse.ca/wp-content/uploads/2016/05/cropped-thumbnail_PitaHouse_FINAL_LOGO2-1.jpg",
            Btw = "BE045785456",
            Description = "Pita vullen is onze specialiteit, njammie!",
            HasGbLabel = false,
            Address = new OrganizationalAddress()
            {
                AddressCity = "Antwerpen",
                AddressCountry = "België",
                AddressLine1 = "Steenstraat 102",
                AddressPostalCode = "3000"
            },
            Contacts = new List<OrganizationContact>()
        };

        private static readonly ExternalOrganization PizzaRoma = new ExternalOrganization
        {
            Name = "Pizza Roma",
            Logo = "http://www.pizzaroma.be/assets/images/pizza-roma-gent.png",
            Btw = "BE066585456",
            Description = "Pizza Roma, de nr. 1 pizzamaker uit Gent!",
            HasGbLabel = true,
            Address = new OrganizationalAddress()
            {
                AddressCity = "Gent",
                AddressCountry = "België",
                AddressLine1 = "Hoogstraat 12",
                AddressPostalCode = "9000"
            }
        };

        #endregion

        private void EnsureSeedGroups()
        {
            // --> SEED Groups
            if (_context.Groups.Any()) return;
            GroupHoGent1.ClosedGroup = false;
            GroupUGent.Motivation = "TestMotivation";
            GroupUGent.GroupState = new ApprovedState(GroupUGent);

            _context.Groups.Add(GroupHoGent1);
            _context.Groups.Add(GroupHoGent2);
            _context.Groups.Add(GroupHoGent3);
            _context.Groups.Add(GroupUGent);
            _context.Groups.Add(GroupSolvay);

            _context.SaveChanges();
        }

        #region Group Data

        private static readonly Group GroupHoGent1 = new Group("GroupHogent1", true)
        {
            GbOrganization = HoGent
        };

        private static readonly Group GroupHoGent2 = new Group("GroupHogent2", true)
        {
            GbOrganization = HoGent
        };

        private static readonly Group GroupHoGent3 = new Group("GroupHogent3", true)
        {
            GbOrganization = HoGent
        };

        private static readonly Group GroupUGent = new Group("GroupUgent", true)
        {
            GbOrganization = UGent
        };

        private static readonly Group GroupSolvay = new Group("GroupSolvay", false)
        {
            GbOrganization = Solvay
        };

        #endregion


        private void EnsureSeedInvitations()
        {
            if (_context.Invitations.Any()) return;

            var invitation2 = new Invitation(UserTest, GroupHoGent2);
            var invitation3 = new Invitation(UserTest, GroupHoGent3);

            var acceptedInvitation = new Invitation(UserHogent, GroupHoGent1) { Status = InvitationStatus.Accepted };

            _context.Add(invitation2);
            _context.Add(invitation3);
            _context.Add(acceptedInvitation);

            _context.SaveChanges();
        }

        private void EnsureSeedActivities()
        {
            if (_context.Activities.Any()) return;

            var activity1 = new Activity("Activity1", "Activity description", GroupHoGent1);
            var activity2 = new Activity("Activity2", "Activity description", GroupHoGent1);
            var activity3 = new Activity("Activity3", "Activity description", GroupHoGent1);

            var event1 = new Event("Event1", "Event description", DateTime.Today.AddDays(5), GroupHoGent1);
            var event2 = new Event("Event2", "Event description", DateTime.Today.AddDays(25), GroupHoGent1);
            var event3 = new Event("Event3", "Event description", DateTime.Today.AddDays(45), GroupHoGent1);

            _context.Activities.Add(activity1);
            _context.Activities.Add(activity2);
            _context.Activities.Add(activity3);
            _context.Activities.Add(event1);
            _context.Activities.Add(event2);
            _context.Activities.Add(event3);

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

            UserTest.Organization = HoGent;
            UserHogent.Organization = HoGent;
            UserBart.Organization = HoGent;
            UserMax.Organization = HoGent;
            UserMilan.Organization = HoGent;
            UserTom.Organization = HoGent;

            await _context.SaveChangesAsync();
        }

        #region UserData

        private const string DefaultPassword = "test";

        private const string RoleNameVrijwilliger = "vrijwilliger";
        private const string RoleNameCursist = "cursist";

        private static readonly Role RoleVrijwilliger = new Role(RoleNameVrijwilliger);
        private static readonly Role RoleCursist = new Role(RoleNameCursist);

        private static readonly Role[] Roles = new Role[] { RoleVrijwilliger, RoleCursist };

        private static readonly User UserLector = GenerateTestUser("giveaday", "mdware.org");
        private static readonly User UserTest = GenerateTestUser("test", "test.be");
        private static readonly User UserHogent = GenerateTestUser("test", "hogent.be");
        private static readonly User UserVrijwilliger = GenerateTestUser("vrijwilliger", "test.be");
        private static readonly User UserCursist = GenerateTestUser("cursist", "test.be");
        private static readonly User UserMilan = GenerateTestUser("milandimax", "gmail.com", "Milan", "Dima");
        private static readonly User UserTom = GenerateTestUser("tom", "vdbussche.net", "Tom", "Vandenbussche");
        private static readonly User UserMax = GenerateTestUser("max.devloo", "lightspeedhq.com", "Maximiliaan", "Devloo");
        private static readonly User UserBart = GenerateTestUser("bartjevm", "gmail.com", "Bart", "Vanmarcke");

        private static readonly User[] Users = new User[] { UserLector, UserTest, UserHogent, UserVrijwilliger, UserCursist, UserMilan, UserTom, UserMax, UserBart };

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
                LectorUser = UserLector
            };
        }

        #endregion

    }
}