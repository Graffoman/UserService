using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Contracts.UserRole;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса работы с ролями пользователей.
    /// </summary>
    public interface IUserRoleService
    {
        /// <summary>
        /// Получить роль пользователя. 
        /// </summary>
        /// <param name="id"> Идентификатор </param>
        /// <returns> ДТО роли пользователя </returns>
        Task<UserRoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создать роль пользователя.
        /// </summary>
        /// <param name="creatingUserGroupDto"> ДТО группы пользователей. </param>
        /// <returns> Идентификатор. </returns>
        Task<Guid> CreateAsync( CreatingUserRoleDto creatingUserRoleDto);

        /// <summary>
        /// Удалить роль пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);
    }
}
