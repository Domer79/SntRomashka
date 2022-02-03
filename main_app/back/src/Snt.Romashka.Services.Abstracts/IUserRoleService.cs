using System;
using System.Threading.Tasks;

namespace Snt.Romashka.Services.Abstracts
{
    public interface IUserRoleService
    {
        Task Add(Guid userId, Guid roleId);
        Task<bool> Delete(Guid userId, Guid roleId);
        Task<bool> Check(Guid userId, Guid roleId);
    }
}