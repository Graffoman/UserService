using Domain.Entities;

namespace Services.Repositories.Abstractions
{
	public interface IUserRoleRepository : IRepository<UserRole, Guid>
    {
        /// <summary>
        /// Получить связь пользователя и роли
        /// </summary>
        /// <param name="userId"> ID пользователя. </param>     
        /// /// <param name="roleId"> ID роли. </param>  
        /// <returns> Cвязь пользователя и роли. </returns>
        Task<UserRole> GetByUserIdRoleIdAsync(Guid userId, Guid roleId);
    }
}
