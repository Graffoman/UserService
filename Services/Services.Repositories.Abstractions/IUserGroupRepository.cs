using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Services.Contracts.UserGroup;

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
        /// <returns> Пользователь. </returns>
        Task<UserGroup> GetByUserIdGroupIdAsync(Guid userId, Guid groupId);

    }

    
}
