using System;
using System.Linq;
using GoedBezigWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Data
{
    public static class ApplicationDbInitializer
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            //// --> SEED Users & Roles
            //if (!context.Roles.Any())
            //{
            //    string[] roles = new string[]
            //    {
            //        "Vrijwilliger",
            //        "Cursist"
            //    };

            //    var roleStore = new RoleStore<Role>(context);

            //    foreach (var role in roles)
            //    {
                    
            //    }
            //}

            // --> SEED Organizational Addresses
            if (!context.OrganizationalAddresses.Any())
            {
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

            // --> SEED Organizations

            if (!context.Organizations.Any())
            {
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

            // --> SEED Users
            if (!context.Users.Any())
            {
                // TODO Add users to identity https://blog.falafel.com/seed-database-initial-users-mvc-5/

                var hasher = new PasswordHasher<User>();

                var users = new User[]
                {
                    // DateTime today = DateTime.Today;  // de bedoeling was om de datum van vandaag op te halen maar voor de ene of andere reden werkt het niet
                    // Hier per ongeluk ook een apckegae system.runtime toegevoegd maar vind niet direct terug waar ik het moet verwijderen. 
                    new User
                    {
                        UserName = "test@test.com",
                        Email = "test@test.com",
                        FirstName = "Test",
                        FamilyName = "Test"
                    },
                    new User
                    {
                        UserName = "milandimax@gmail.com",
                        Email = "milandimax@gmail.com",
                        FirstName = "Milan",
                        FamilyName = "Dima"
                    },
                    new User
                    {
                        UserName = "tom@vdbussche.net",
                        Email = "tom@vdbussche.net",
                        FirstName = "Tom",
                        FamilyName = "Vandenbussche"
                    },
                    new User
                    {
                        UserName = "max.devloo@lightspeedhq.com",
                        Email = "max.devloo@lightspeedhq.com",
                        FirstName = "Maximiliaan",
                        FamilyName = "Devloo"
                    },
                    new User
                    {
                        UserName = "bartjevm@gmail.com",
                        Email = "bartjevm@gmail.com",
                        FirstName = "Bart",
                        FamilyName = "Vanmarcke"
                    }
                };

                foreach (var u in users)
                {
                    u.PasswordHash = hasher.HashPassword(u, "Test!123");
                    context.Users.Add(u);
                }

                context.SaveChanges();
            }

            // --> SEED Groups
            if (!context.Groups.Any())
            {
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
        }
    }
}