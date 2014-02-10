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
            var userRoles = user.Roles.ToList();
            foreach (var role in userRoles)
            {
                _userManager.RemoveFromRole(userId, role.Role.Name);
            }
        }

        
        public void RemoveFromRole(string userId, string roleName)
        {
            _userManager.RemoveFromRole(userId, roleName);
        }


        public void DeleteRole(string roleId)
        {
            var roleGroups = _db.Groups.Where(g => g.Roles.Any(r => r.RoleId == roleId));
            var roleUsers = _db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId));
            var role = _db.ApplicationRoles.Find(roleId);

            foreach(var user in roleUsers)
            {
                this.RemoveFromRole(user.Id, role.Name);
            }

            foreach(var group in roleGroups)
            {
                var arg = new ApplicationRoleGroup()
                {
                    RoleId = roleId,
                    GroupId = group.Id,
                    Role = role,
                    Group = group
                };

                group.Roles.Remove(arg);
            }
            _db.ApplicationRoles.Remove(role);
            _db.SaveChanges();
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


        public void ClearUserGroups(string userId)
        {
            this.ClearUserRoles(userId);
            var user = _db.Users.Find(userId);
            user.Groups.Clear();
            _db.SaveChanges();
        }


        public void AddUserToGroup(string userId, int roleGroupId)
        {
            var group = _db.Groups.Find(roleGroupId);
            var user = _db.Users.Find(userId);

            var userGroup = new ApplicationUserGroup()
            {
                Group = group,
                GroupId = group.Id,
                User = user,
                UserId = user.Id
            };

            foreach(var role in group.Roles)
            {
                _userManager.AddToRole(userId, role.Role.Name);
            }
            user.Groups.Add(userGroup);
            _db.SaveChanges();
        }


        public void ClearGroupRoles(int groupId)
        {
            var group = _db.Groups.Find(groupId);
            var rolesToRemove = group.Roles.ToList();
            var groupUsers = _db.Users.Where(u => u.Groups.Any(g => g.GroupId == group.Id));
            foreach(var user in groupUsers)
            {
                foreach(var role in group.Roles)
                {
                    this.RemoveFromRole(user.Id, role.Role.Name);
                }
            }
            group.Roles.Clear();
            _db.SaveChanges();
        }


        public void AddRoleToGroup(int groupId, string roleName)
        {
            var group = _db.Groups.Find(groupId);
            var role = _db.Roles.First(r => r.Name == roleName);
            var newgroupRole = new ApplicationRoleGroup()
            {
                GroupId = group.Id,
                Group = group,
                RoleId = role.Id,
                Role = (ApplicationRole)role
            };
            group.Roles.Add(newgroupRole);
            _db.SaveChanges();

            // Add all of the users in this group to the new role:
            var groupUsers = _db.Users.Where(u => u.Groups.Any(g => g.GroupId == group.Id));
            foreach(var user in groupUsers)
            {
                this.AddUserToRole(user.Id, role.Name);
            }
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