using System;
using System.Collections.Generic;

namespace Snt.Romashka.Contracts
{
    public class Permission
    {
        public Guid Id { get; set; }
        public SecurityPolicy Politic { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public HashSet<Role> Roles { get; set; }
        public HashSet<RolePermission> PermissionRoles { get; set; }
    }
}