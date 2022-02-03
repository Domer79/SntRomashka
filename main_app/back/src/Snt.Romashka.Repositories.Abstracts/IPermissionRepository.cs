using System.Threading.Tasks;
using Snt.Romashka.Contracts;

namespace Snt.Romashka.Repositories.Abstracts
{
    public interface IPermissionRepository
    {
        Task<Permission> GetPermission(SecurityPolicy securityPolicy);
        Task<Permission[]> GetAll();
        Task<Permission> Upsert(Permission permission);
        Task<bool> Delete(Permission permission);
        Task<bool> Delete(SecurityPolicy policy);
    }
}