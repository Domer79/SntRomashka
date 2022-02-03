using System.Threading.Tasks;

namespace Snt.Romashka.Services.Abstracts
{
    public interface IPermissionService
    {
        /// <summary>
        /// Обновление политик безопасности в БД
        /// </summary>
        /// <returns></returns>
        Task RefreshPolicy();
    }
}