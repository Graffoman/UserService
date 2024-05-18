using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Services.Contracts.User;

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
    }
}
