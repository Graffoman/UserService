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
        /// Добавить пользователя в группу.
        /// </summary>
        /// <param name="creatingUserGroupDto"> ДТО добавления пользоватьеля в группу. </param>
        Task<Guid> AddUserToGroupAsync(CreatingUserGroupDto creatingUserGroupDto);

        /// <summary>
        /// Удалить пользователя из группы.
        /// </summary>
        /// <param name="creatingUserGroupDto"> ДТО добавления пользоватьеля в группу. </param>
        Task DeleteUserFromGroupAsync(CreatingUserGroupDto creatingUserGroupDto);
    }
}

