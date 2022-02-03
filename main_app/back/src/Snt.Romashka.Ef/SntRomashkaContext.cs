using Microsoft.EntityFrameworkCore;
using Snt.Romashka.Contracts;

namespace Snt.Romashka.Ef
{
    public class SntRomashkaContext: DbContext
    {
        private readonly DbContextOptions _options;

        public SntRomashkaContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SntRomashkaContext).Assembly);
        }
    }
}
