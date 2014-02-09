using System;
using System.Linq;
using System.Web;
using AspNetRoleCustomization.Models;
using System.ComponentModel.DataAnnotations;

// New namespace imports:
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace AspNetRoleCustomization.Models
{
    // Wrapper for SelectGroupEditorViewModel to select user group membership:
    public class SelectUserGroupsViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<SelectGroupEditorViewModel> Groups { get; set; }

        public SelectUserGroupsViewModel()
        {
            this.Groups = new List<SelectGroupEditorViewModel>();
        }

        public SelectUserGroupsViewModel(ApplicationUser user)
            : this()
        {
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;

            var Db = new ApplicationDbContext();

            // Add all available groups to the public list:
            var allGroups = Db.Groups;
            foreach (var role in allGroups)
            {
                // An EditorViewModel will be used by Editor Template:
                var rvm = new SelectGroupEditorViewModel(role);
                this.Groups.Add(rvm);
            }           

            // Set the Selected property to true where user is already a member:
            foreach (var group in user.Groups)
            {
                var checkUserRole =
                    this.Groups.Find(r => r.GroupName == group.Group.Name);
                checkUserRole.Selected = true;
            }
        }
    }


    // Used to display a single role group with a checkbox, within a list structure:
    public class SelectGroupEditorViewModel
    {
        public SelectGroupEditorViewModel() { }
        public SelectGroupEditorViewModel(Group group)
        {
            this.GroupName = group.Name;
            this.GroupId = group.Id;
        }

        public bool Selected { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        public string GroupName { get; set; }
    }


    //public class SelectGroupRolesViewModel
    //{
    //    public SelectGroupRolesViewModel()
    //    {
    //        this.Roles = new List<SelectRoleEditorViewModel>();
    //    }



    //    public SelectGroupRolesViewModel(Group group)
    //        : this()
    //    {
    //        this.GroupId = group.Id;
    //        this.GroupName = group.Name;

    //        var Db = new ApplicationDbContext();

    //            Add all available roles to the list of EditorViewModels:
    //        var allRoles = Db.Roles;
    //        foreach (var role in allRoles)
    //        {
    //                An EditorViewModel will be used by Editor Template:
    //            var rvm = new SelectRoleEditorViewModel(role);
    //            this.Roles.Add(rvm);
    //        }

    //        foreach (var roleGroup in group.Roles)
    //        {
    //            var checkGroupRole =
    //                this.Roles.Find(r => r.RoleName == roleGroup.Role.Id);
    //            checkGroupRole.Selected = true;
    //        }
    //    }

    //    public int GroupId { get; set; }
    //    public string GroupName { get; set; }
    //    public List<SelectRoleEditorViewModel> Roles { get; set; }
    //    }
    //}




    //// Used to display a single role with a checkbox, within a list structure:
    //public class SelectRoleEditorViewModel
    //{
    //    public SelectRoleEditorViewModel() { }
    //    public SelectRoleEditorViewModel(IdentityRole role)
    //    {
    //        this.RoleName = role.Name;
    //    }

    //    public bool Selected { get; set; }

    //    [Required]
    //    public string RoleName { get; set; }
    //}
}