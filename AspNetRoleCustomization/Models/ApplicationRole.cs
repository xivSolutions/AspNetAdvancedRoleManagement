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
            this.Groups = new HashSet<Group>();
        }


        public ApplicationRole(string roleName) : base(roleName)
        {
            this.Groups = new HashSet<Group>();
        }


        public HashSet<Group> Groups { get; set; }
    }
}