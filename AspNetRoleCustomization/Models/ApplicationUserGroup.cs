using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AspNetRoleCustomization.Models
{
    public class ApplicationUserGroup
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GroupId { get; set; }

        public ApplicationUser User { get; set; }
        public Group Group { get; set; }
    }
}