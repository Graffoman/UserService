using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Contracts.UserGroup;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса работы с группами пользователя.
    /// </summary>
    public interface IUserGroupService
    {
        /// <summary>
        /// Получить группу пользователя. 
        /// </summary>
        /// <param name="id"> Идентификатор </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns>ДТО группы пользователей</returns>
        Task<UserGroupDto> GetByIdAsync(Guid id, Guid groupId, CancellationToken cancellationToken);

        /// <summary>
        /// Создать группу пользователей.
        /// </summary>
        /// <param name="creatingUserGroupDto"> ДТО группы пользователя. </param>
        /// <returns> Идентификатор. </returns>
        Task<Guid> CreateAsync(CreatingUserGroupDto creatingUserGroupDto);

        /// <summary>
        /// Удалить группу пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор </param>
        Task DeleteAsync(Guid id, Guid groupId);
    }
}

