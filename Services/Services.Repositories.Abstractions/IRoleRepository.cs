using Domain.Entities;

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
        /// Получить список пользователей, у которых нет роли.
        /// </summary>
        /// <param name="id"> Идентификатор роли. </param>
        /// <returns> Список пользоваетелей. </returns>
        Task<List<User>> GetUserNotInRoleListAsync(Guid id);

        /// <summary>
        /// Получить список пользователей с ролью.
        /// </summary>
        /// <param name="id"> Идентификатор роли. </param>
        /// <returns> Список пользоваетелей. </returns>
        Task<List<User>> GetUserListAsync(Guid id);


    }
}
