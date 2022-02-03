﻿using System;
using System.Threading.Tasks;
using Snt.Romashka.Contracts;

namespace Snt.Romashka.Services.Abstracts
{
    public interface IUserService
    {
        Task<User[]> GetAll();
        Task<User> GetById(Guid id);
        Task<User[]> GetByIds(Guid[] ids);
        Task<Page<User>> GetPage(int pageNumber, int pageSize, bool? isActive);
        Task<User> GetByLogin(string login);
        Task<User> Upsert(User user);
        Task<bool> Remove(Guid userId);
        Task Remove(User user);
        Task<bool> ValidatePassword(Guid userId, string password);
        Task<bool> ValidatePassword(string login, string password);
        bool ValidatePassword(User user, string password);
        Task<bool> SetPassword(Guid userId, string password);
        Task<bool> SetPassword(string login, string password);
        Task<bool> SetPassword(User user, string password);
        Task<Role[]> GetRoles(Guid userId);
        Task<Role[]> GetRoles(string login);
    }
}