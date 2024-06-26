﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с ролями.
    /// </summary>
    public interface IRoleRepository : IRepository<Role, Guid>
    {
        /// <summary>
        /// Получить полный список ролей.
        /// </summary>
        /// <returns> Список ролей. </returns>
        Task<List<Role>> GetListAsync();

        /// <summary>
        /// Получить список пользователей с ролью.
        /// </summary>
        /// <param name="id"> Идентификатор роли. </param>
        /// <returns> Список пользователей. </returns>
        Task<List<User>> GetUserListAsync(Guid id);
    }
}
