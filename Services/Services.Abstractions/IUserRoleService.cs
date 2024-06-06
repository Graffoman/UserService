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
        /// Добавить роль пользователю.
        /// </summary>
        /// <param name="creatingUserRoleDto"> ДТО добавления роли пользоватьеля. </param>
        Task<Guid> AddUserToRoleAsync(CreatingUserRoleDto creatingUserRoleDto);

        /// <summary>
        /// Удалить роль пользователя.
        /// </summary>
        /// <param name="creatingUserRoleDto"> ДТО добавления роли пользоватьеля. </param>
        Task DeleteUserFromRoleAsync(CreatingUserRoleDto creatingUserRoleDto);
    }
}
