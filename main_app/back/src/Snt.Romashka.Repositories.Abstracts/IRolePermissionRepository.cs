using System;
using System.Threading.Tasks;

namespace Snt.Romashka.Repositories.Abstracts
{
    public interface IRolePermissionRepository
    {
        Task Add(Guid roleId, Guid permissionId);
        Task<bool> Delete(Guid roleId, Guid permissionId);
    }
}