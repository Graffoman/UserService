﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Services.Contracts.User;
using Services.Contracts.Group;
using Services.Contracts.UserGroup;
using Services.Contracts.UserRole;

namespace Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с пользователями.
    /// </summary>
    public interface IUserRepository: IRepository<User, Guid>
    {
        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="filterDto"> ДТО фильтра. </param>
        /// <returns> Список пользователей. </returns>
        Task<List<User>> GetPagedAsync(UserFilterDto filterDto);

        /// <summary>
        /// Получить пользователя по Email, PasswordHash
        /// </summary>
        /// <param name="userSigningInDto"> ДТО логина. </param>       
        /// <returns> Пользователь. </returns>
        Task<User> LoginAsync(UserLoginDto userLoginDto, string PasswordHash);

        /// <summary>
        /// Получить полный список.
        /// </summary>
        /// <returns> Список пользователей. </returns>
        Task<List<User>> GetListAsync();

        /// <summary>
        /// Получить список групп пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Список групп. </returns>
        Task<List<Group>> GetGroupListAsync(Guid id);

        /// <summary>
        /// Получить список ролей пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Список ролей. </returns>
        Task<List<Role>> GetRoleListAsync(Guid id);

    }
}
