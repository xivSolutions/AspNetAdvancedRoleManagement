using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace AspNetRoleCustomization.Models
{
    public class ApplicationRoleGroup
    {

        public string RoleId { get; set; }
        public int GroupId { get; set; }

        public virtual ApplicationRole Role { get; set; }
        public virtual Group Group { get; set; }
    }
}