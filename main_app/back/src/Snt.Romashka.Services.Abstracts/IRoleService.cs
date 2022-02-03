using System;
using System.Threading.Tasks;
using Snt.Romashka.Contracts;

namespace Snt.Romashka.Services.Abstracts
{
    public interface IRoleService
    {
        Task<Permission[]> GetPermissions(Guid[] roleIds);
        Task<Role[]> GetRoles();
        Task<bool> SetRole(Guid roleId, Guid userId, bool set);
    }
}