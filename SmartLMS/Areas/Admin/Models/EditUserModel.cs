using Microsoft.AspNet.Identity.EntityFramework;
using SmartLMS.Infrastructure.BaseTypes;
using SmartLMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLMS.Areas.Admin.Models
{
    public class EditUserModel : SaveableModel
    {
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; }
        public IList<IdentityRole> AllRoles { get; set; }
    }

    public class SaveProfileModel
    {
        public UserProfile User { get; set; }
    }

    public class UserProfile
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string DisplayName { get; set; }

        public bool EmailOptin { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Twitter { get; set; }

        public string TwitterHandle { get; set; }

        public string Facebook { get; set; }

        public string GooglePlus { get; set; }

        public string LinkedIn { get; set; }

        public string Bio { get; set; }

        public string JobTitle { get; set; }

        public string WebsiteUrl { get; set; }

        public string VATNumber { get; set; }

        public string ClientCode { get; set; }

        public string Notes { get; set; }

    }
}