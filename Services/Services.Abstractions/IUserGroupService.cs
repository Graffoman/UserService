using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Contracts.Role;
using Services.Contracts.UserGroup;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса работы группами пользователей.
    /// </summary>
    public interface IUserGroupService
    {
        /// <summary>
        /// Получить группу пользователей. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> ДТО группы пользователей. </returns>
        Task<UserGroupDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создать группу пользователей.
        /// </summary>
        /// <param name="creatingUserGroupDto"> ДТО группы пользователей. </param>
        /// <returns> Идентификатор. </returns>
        Task<Guid> CreateAsync(CreatingUserGroupDto creatingUserGroupDto);

        /// <summary>
        /// Изменить группу пользователей.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingUserGroupDto"> ДТО группы пользователей. </param>
        Task UpdateAsync(Guid id, UpdatingUserGroupDto updatingUserGroupDto);

        /// <summary>
        /// Удалить группу пользователей.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);
    }
}
