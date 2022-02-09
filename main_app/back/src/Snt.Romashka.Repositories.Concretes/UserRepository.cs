using System;
using System.Linq;
using System.Threading.Tasks;
using Snt.Romashka.Contracts;
using Snt.Romashka.Db.Core.Abstracts;
using Snt.Romashka.Repositories.Abstracts;

namespace Snt.Romashka.Repositories.Concretes
{
    public class UserRepository: IUserRepository
    {
        private readonly ICommonDb _commonDb;

        public UserRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }

        public Task<User[]> GetAll()
        {
            return _commonDb.QueryAsync<User>("select user_id, login, email, created, is_active, fio from [user]");
        }

        public Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Upsert(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> SetPassword(string loginOrEmail, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public Task<Role[]> GetRoles(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Role[]> GetRoles(string login)
        {
            throw new NotImplementedException();
        }

        public Task<User[]> GetByIds(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<User[]> GetPage(int pageNumber, int pageSize, bool? isActive)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetTotalCount(bool? isActive)
        {
            throw new NotImplementedException();
        }
    }
}