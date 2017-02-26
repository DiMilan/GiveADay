using System;
using System.Linq;
using GoedBezigWebApp.Models;

namespace GoedBezigWebApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GoedBezigDbContext context)
        {
            context.Database.EnsureCreated();

            // --> SEED Organizational Addresses
            if (!context.OrganizationalAddresses.Any())
            {
                var organizational_addresses = new OrganizationalAddress[]
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
                };

                foreach (OrganizationalAddress oa in organizational_addresses)
                {
                    context.OrganizationalAddresses.Add(oa);
                }
                context.SaveChanges();

            }

            // --> SEED Organizations

            if (!context.Organization.Any())
            {
                var organizations = new Organization[]
                {
                    new Organization
                    {
                        Name = "HoGent",
                        Logo = "https://upload.wikimedia.org/wikipedia/commons/1/10/HoGent_Logo.png",
                        Btw = "BE012345678901",
                        IdentificationNr = "2876545678",
                        Description = "University College in Ghent",
                        AddressId = 1
                    },
                    new Organization
                    {
                        Name = "UGent",
                        Logo = "https://webster.ugent.be/alumnivacatures/invoeren/static/images/logo-ugent_org.svg",
                        Btw = "BE022344678901",
                        IdentificationNr = "9838920030",
                        Description = "University Ghent",
                        AddressId = 2
                    },
                };

                foreach (Organization o in organizations)
                {
                    context.Organization.Add(o);
                }
                context.SaveChanges();
            }

            // --> SEED Users
            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    // DateTime today = DateTime.Today;  // de bedoeling was om de datum van vandaag op te halen maar voor de ene of andere reden werkt het niet
                    // Hier per ongeluk ook een apckegae system.runtime toegevoegd maar vind niet direct terug waar ik het moet verwijderen. 
                    new User
                    {
                        Username = "milan",
                        Email = "milandimax@gmail.com",
                        CreatedAt = DateTime.Today,
                        FirstName = "Milan",
                        FamilyName = "Dima"
                    },
                    new User
                    {
                        Username = "tom",
                        Email = "tom@vdbussche.net",
                        CreatedAt = DateTime.Today,
                        FirstName = "Tom",
                        FamilyName = "Vandenbussche"
                    },
                    new User
                    {
                        Username = "max",
                        Email = "max.devloo@lightspeedhq.com",
                        CreatedAt = DateTime.Today,
                        FirstName = "Maximiliaan",
                        FamilyName = "Devloo"
                    },
                    new User
                    {
                        Username = "bart",
                        Email = "bartjevm@gmail.com",
                        CreatedAt = DateTime.Today,
                        FirstName = "Bart",
                        FamilyName = "Vanmarcke"
                    }
                };

                foreach (User u in users)
                {
                    context.Users.Add(u);
                }

                context.SaveChanges();
            }
        }
    }
}