using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GoedBezigWebApp.Models.OrganizationViewModels
{
    public class OrganizationSearchViewModel
    {
        public String searchName { get; set; }
        public String searchCity { get; set; }

    }
}
