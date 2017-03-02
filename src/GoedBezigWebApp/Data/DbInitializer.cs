using System;
using System.Linq;
using GoedBezigWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            // IPV Created eigenlijk context.Database.Migrate(); + functie voor Migrte van ApplicationDbContext


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

                foreach (OrganizationalAddress oa in organizationalAddresses)
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
//                        IdentificationNr = "2876545678",
                        Description = "University College in Ghent",
                        AddressId = 1
                    },
                    new Organization
                    {
                        Name = "UGent",
                        Logo = "https://webster.ugent.be/alumnivacatures/invoeren/static/images/logo-ugent_org.svg",
                        Btw = "BE022344678901",
//                        IdentificationNr = "9838920030",
                        Description = "University Ghent",
                        AddressId = 2
                    },
                    new Organization
                    {
                        Name = "Solvay Economics & Management",
                        Logo = "https://lh4.googleusercontent.com/-RuXL76SEWQQ/AAAAAAAAAAI/AAAAAAAAAG4/vplxDplhGmM/s0-c-k-no-ns/photo.jpg",
                        Btw = "BE827638900032",
//                        IdentificationNr = "66483899483",
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
                //var users = new User[]
                //{
                //    // DateTime today = DateTime.Today;  // de bedoeling was om de datum van vandaag op te halen maar voor de ene of andere reden werkt het niet
                //    // Hier per ongeluk ook een apckegae system.runtime toegevoegd maar vind niet direct terug waar ik het moet verwijderen. 
                //    new User
                //    {
                //        Username = "milan",
                //        Email = "milandimax@gmail.com",
                //        CreatedAt = DateTime.Today,
                //        FirstName = "Milan",
                //        FamilyName = "Dima"
                //    },
                //    new User
                //    {
                //        Username = "tom",
                //        Email = "tom@vdbussche.net",
                //        CreatedAt = DateTime.Today,
                //        FirstName = "Tom",
                //        FamilyName = "Vandenbussche"
                //    },
                //    new User
                //    {
                //        Username = "max",
                //        Email = "max.devloo@lightspeedhq.com",
                //        CreatedAt = DateTime.Today,
                //        FirstName = "Maximiliaan",
                //        FamilyName = "Devloo"
                //    },
                //    new User
                //    {
                //        Username = "bart",
                //        Email = "bartjevm@gmail.com",
                //        CreatedAt = DateTime.Today,
                //        FirstName = "Bart",
                //        FamilyName = "Vanmarcke"
                //    }
                //};

                //foreach (User u in users)
                //{
                //    context.Users.Add(u);
                //}

                //context.SaveChanges();
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

                foreach (Group g in groups)
                {
                    context.Groups.Add(g);
                }

                context.SaveChanges();
            }
        }
    }
}