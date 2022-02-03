using System;
using System.Collections.Generic;

namespace Snt.Romashka.Contracts
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public HashSet<User> Users { get; set; }
        public HashSet<UserRole> RoleUsers { get; set; }
        public HashSet<Permission> Permissions { get; set; }
        public HashSet<RolePermission> RolePermissions { get; set; }
    }
}