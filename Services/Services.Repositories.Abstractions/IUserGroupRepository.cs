using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с группами пользователей.
    /// </summary>
    public interface IUserGroupRepository: IRepository<UserGroup, Guid>
    {       
    }
}
