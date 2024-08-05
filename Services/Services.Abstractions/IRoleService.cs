using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Contracts.Group;
using Services.Contracts.Role;
using Services.Contracts.User;

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
        Task<RoleDto?> GetByIdAsync(Guid id);

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

        /// <summary>
        /// Получить полный список.
        /// </summary>
        /// <returns> Список ролей. </returns>
        Task<ICollection<RoleDto>> GetListAsync();

        /// <summary>
        /// Получить список пользователей с ролью.
        /// </summary>
        /// <param name="id"> Идентификатор роли </param>
        /// <returns> Список пользователей. </returns>
        Task<ICollection<UserDto>> GetUserListAsync(Guid id);

        /// <summary>
        /// Получить список пользователей, у которых нет роли.
        /// </summary>
        /// <param name="id"> Идентификатор роли </param>
        /// <returns> Список пользователей. </returns>
        Task<ICollection<UserDto>> GetUserNotInRoleListAsync(Guid id);

    }
}
