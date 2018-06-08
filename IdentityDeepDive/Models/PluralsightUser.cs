using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDeepDive.Models
{
    public class PluralsightUser : IdentityUser
    {
        public string Locale { get; set; } = "en-US";
        public string OrgId { get; set; }

    }

    public class Organization
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
