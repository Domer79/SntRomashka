using System;
using System.Collections.Generic;

namespace Snt.Romashka.Contracts
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fio { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateBlocked { get; set; }
        public byte[] Password { get; set; }
        public bool IsActive { get; set; }
        public HashSet<Role> Roles { get; set; }
        public HashSet<UserRole> UserRoles { get; set; }
        public HashSet<Token> Tokens { get; set; }
    }
}