using Domain.Entities;

namespace Services.Repositories.Abstractions
{
	/// <summary>
	/// Репозиторий работы с группами пользоваиеля.
	/// </summary>
	public interface IUserGroupRepository: IRepository<UserGroup, Guid>
    {
        /// <summary>
        /// Получить связь пользователя и группы
        /// </summary>
        /// <param name="userId"> ID пользователя. </param>     
        /// /// <param name="groupId"> ID группы. </param>  
        /// <returns> Cвязь пользователя и группы. </returns>
        Task<UserGroup> GetByUserIdGroupIdAsync(Guid userId, Guid groupId);

    }

    
}
