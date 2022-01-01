using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace eCommerce_Asp.Net_MVC.Roles
{
    public class UserRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            Context context = new Context();
            var corporateUser = context.CorporateAccounts.FirstOrDefault(x => x.User.Email == username);
            if (corporateUser == null)
            {
                var customerUser = context.CustomerAccounts.FirstOrDefault(x => x.User.Email == username);
                if (customerUser == null)
                {
                    var adminUser = context.AdminAccounts.FirstOrDefault(x => x.User.Email == username);
                    return new string[] { adminUser.Role };
                }
                return new string[] { customerUser.Role };
            }
            return new string[] { corporateUser.Role };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}