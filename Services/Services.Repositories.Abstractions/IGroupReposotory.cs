using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы группами.
    /// </summary>
    public interface IGroupReposotory : IRepository<Group, Guid>
    {
        /// <summary>
        /// Получить полный список.
        /// </summary>
        /// <returns> Список групп. </returns>
        Task<List<Group>> GetListAsync();

        /// <summary>
        /// Получить список пользователей группы.
        /// </summary>
        /// <param name="id"> Идентификатор группы. </param>
        /// <returns> Список групп. </returns>
        Task<List<User>> GetUserListAsync(Guid id);
    }
}
