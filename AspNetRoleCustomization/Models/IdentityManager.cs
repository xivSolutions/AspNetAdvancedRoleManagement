using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace AspNetRoleCustomization.Models
{
    public class IdentityManager
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        private UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));

        private RoleManager<ApplicationRole> _roleManager = new RoleManager<ApplicationRole>(
            new RoleStore<ApplicationRole>(new ApplicationDbContext()));

        public bool RoleExists(string name)
        {
            return _roleManager.RoleExists(name);
        }


        public bool CreateRole(string name)
        {
            var idResult = _roleManager.Create(new ApplicationRole(name));
            return idResult.Succeeded;
        }


        public bool CreateUser(ApplicationUser user, string password)
        {
            var idResult = _userManager.Create(user, password);
            return idResult.Succeeded;
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }


        public void ClearUserRoles(string userId)
        {
            var user = _userManager.FindById(userId);
            foreach (var role in user.Roles)
            {
                _userManager.RemoveFromRole(userId, role.Role.Name);
            }
        }

        
        public void RemoveFromRole(string userId, string roleName)
        {
            _userManager.RemoveFromRole(userId, roleName);
        }


        public void CreateGroup(string groupName)
        {
            if(this.GroupNameExists(groupName))
            {
                throw new Exception("A group by that name already exists in the database. Please choose another name.");
            }

            var newGroup = new Group(groupName);
            _db.Groups.Add(newGroup);
            _db.SaveChanges();
        }


        public bool GroupNameExists(string groupName)
        {
            var g = _db.Groups.Where(gr => gr.Name == groupName);
            if(g.Count() > 0)
            {
                return true;
            }
            return false;
        }


        //public void ClearUserRoleGroups(string userId)
        //{
        //    var groups = _db.RoleGroups.Where(g => g.Users.Any(u => u.Id == userId));
        //    foreach(var group in groups)
        //    {
        //        this.RemoveUserFromRoleGroup(userId, group);
        //    }
        //}


        //public void RemoveUserFromRoleGroup(string userId, Group group)
        //{
        //    var user = _userManager.FindById(userId);
        //    foreach(var role in group.Roles)
        //    {
        //        _userManager.RemoveFromRole(user.Id, role.Name);
        //    }
        //    var newGroup = _db.RoleGroups.Find(group.Id);
        //    var newUser = _db.Users.Find(userId);
        //    group.Users.Remove(newUser);
        //    _db.SaveChanges();
        //}


        //public void AddUserToRoleGroup(string userId, int roleGroupId)
        //{
        //    var group = _db.RoleGroups.Find(roleGroupId);
        //    var user = _db.Users.Find(userId);
        //    group.Users.Add(user);
        //    _db.SaveChanges();

        //    foreach(var role in group.Roles)
        //    {
        //        this.AddUserToRole(user.Id, role.Name);
        //    }
        //}


    }
}