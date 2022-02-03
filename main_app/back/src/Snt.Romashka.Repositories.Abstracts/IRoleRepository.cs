using System;
using System.Threading.Tasks;
using Snt.Romashka.Contracts;

namespace Snt.Romashka.Repositories.Abstracts
{
    public interface IRoleRepository
    {
        Task<Permission[]> GetPermissions(Guid[] roleIds);
        Task<Role[]> GetAll();
    }
}