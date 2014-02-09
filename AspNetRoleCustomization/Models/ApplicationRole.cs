using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AspNetRoleCustomization.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {
            this.Groups = new HashSet<ApplicationRoleGroup>();
        }


        public ApplicationRole(string roleName) : base(roleName)
        {
            this.Groups = new HashSet<ApplicationRoleGroup>();
        }


        public virtual HashSet<ApplicationRoleGroup> Groups { get; set; }
    }
}