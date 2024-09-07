using Domain.Entities;

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
        /// <returns> Список пользователей. </returns>
        Task<List<User>> GetUserListAsync(Guid id);

        /// <summary>
        /// Получить список пользователей, не входящих в группу.
        /// </summary>
        /// <param name="id"> Идентификатор группы. </param>
        /// <returns> Список пользователей. </returns>
        Task<List<User>> GetUserNotInGroupListAsync(Guid id);
    }
}
