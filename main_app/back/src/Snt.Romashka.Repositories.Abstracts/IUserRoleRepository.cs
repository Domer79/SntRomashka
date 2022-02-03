using System;
using System.Threading.Tasks;

namespace Snt.Romashka.Repositories.Abstracts
{
    public interface IUserRoleRepository
    {
        Task Add(Guid userId, Guid roleId);
        Task<bool> Delete(Guid userId, Guid roleId);
        Task<bool> Check(Guid userId, Guid roleId);
    }
}