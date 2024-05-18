using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Contracts.Role;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса работы с ролями.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Получить роль. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> ДТО роли. </returns>
        Task<RoleDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создать роль.
        /// </summary>
        /// <param name="creatingRoleDto"> ДТО роли. </param>
        /// <returns> Идентификатор. </returns>
        Task<Guid> CreateAsync(CreatingRoleDto creatingRoleDto);

        /// <summary>
        /// Изменить роль.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingRoleDto"> ДТО роли. </param>
        Task UpdateAsync(Guid id, UpdatingRoleDto updatingRoleDto );

        /// <summary>
        /// Удалить роль.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);

    }
}
