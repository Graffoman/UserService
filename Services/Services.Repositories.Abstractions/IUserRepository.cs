using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Services.Contracts.User;
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

    }
}
