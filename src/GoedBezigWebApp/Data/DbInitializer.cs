﻿using System.Linq;
using GoedBezigWebApp.Models;

namespace GoedBezigWebApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GoedBezigDbContext context)
        {
            context.Database.EnsureCreated();

            // --> SEED Organizations
            // Check if organizations are present.
            if (context.Organization.Any())
            {
                return;   // DB seed has already been done
            }

            var organizations = new Organization[]
            {
            new Organization{Name = "HoGent",Logo = "https://upload.wikimedia.org/wikipedia/commons/1/10/HoGent_Logo.png",Btw = "BE012345678901",IdentificationNr = "2876545678", Description = "University College in Ghent"},
            new Organization{Name = "UGent",Logo = "http://www.analchem.ugent.be/images/logo_UGent_NL/logo_UGent_NL/logo_UGent_NL_RGB_2400_kleur.png",Btw = "BE022344678901",IdentificationNr = "9838920030", Description = "University Ghent"},

            };
            foreach (Organization o in organizations)
            {
                context.Organization.Add(o);
            }
            context.SaveChanges();

            //Add other seeds here in analogy with the organizations seed

            context.SaveChanges();
        }
    }
}