using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Entities.Permissions
{
    public static class ApplicationPermissions
    {
        public static ReadOnlyCollection<ApplicationPermission> AllPermissions;

        public static ApplicationPermission AdminPermission = new ApplicationPermission("Admin Permission", "admin", "Permission for administrator");

        static ApplicationPermissions()
        {
            List<ApplicationPermission> allPermissions = new List<ApplicationPermission>()
            {
                AdminPermission
            };

            AllPermissions = allPermissions.AsReadOnly();
        }

        public static ApplicationPermission GetPermissionByName(string permissionName)
        {
            return AllPermissions.Where(p => p.Name == permissionName).FirstOrDefault();
        }

        public static ApplicationPermission GetPermissionByValue(string permissionValue)
        {
            return AllPermissions.Where(p => p.Value == permissionValue).FirstOrDefault();
        }

        public static string[] GetAllPermissionValues()
        {
            return AllPermissions.Select(p => p.Value).ToArray();
        }
    }
}
