using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NUnit.Framework;
using Snt.Romashka.Contracts;
using Snt.Romashka.Database.Core;
using Snt.Romashka.Db.Core.Abstracts;
using Snt.Romashka.Repositories.Abstracts;
using Snt.Romashka.Repositories.Concretes;

namespace Snt.Romashka.Repositories.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private IUserRepository _repo;
        private ICommonDb _commonDb;
        private IConnectionFactory _connectionFactory;
        private CommonDbTransactionWrapper _tran;

        [OneTimeSetUp]
        public void BaseSetUp()
        {
            _connectionFactory = new SqlConnectionFactory(new TestSettings());
            _commonDb = new CommonDb(_connectionFactory);
            _repo = new UserRepository(_commonDb);
            _tran = _commonDb.BeginTransaction();
        }
        
        [OneTimeTearDown]
        public void BaseTearDown()
        {
            _tran.Rollback();
            _tran.Dispose();
        }

        [Test]
        public async Task GetAllTest()
        {
            Helper.InsertUser(_commonDb, new User()
            {
                UserId = Guid.NewGuid(),
                Login = "login1",
                Email = "email@mail.ru",
                Created = DateTime.UtcNow,
                IsActive = true,
                Fio = "Иванов Иван Иванович"
            });

            var users = await _repo.GetAll();
            Assert.AreEqual(1, users.Length);
        }
    }
}