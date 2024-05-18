using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Contracts.User;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса работы с пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО пользователя. </returns>
        Task<UserDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить пользователя по e-mail и паролю.
        /// </summary>
        /// <param name="email"> e-mail. </param>
        /// <param name="passwordhash"> хэш пароля. </param>
        /// <returns> ДТО пользователя. </returns>
        Task<UserDto> GetByLoginPasswordAsync(string email, string passwordhash);

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        /// <param name="creatingUserDto"> ДТО создаваемого пользователя. </param>
        Task<Guid> CreateAsync(CreatingUserDto creatingUserDto);

        /// <summary>
        /// Изменить пользователя.
        /// </summary>
        /// <param name="id"> Иентификатор. </param>
        /// <param name="updatingUserDto"> ДТО редактируемого пользователя. </param>
        Task UpdateAsync(Guid id, UpdatingUserDto updatingUserDto);

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);
        
        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="filterDto"> ДТО фильтра. </param>
        /// <returns> Список пользователей. </returns>
        Task<ICollection<UserDto>> GetPagedAsync(UserFilterDto filterDto);

        /// <summary>
        /// Назначить роль пользователю.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="roleId"> Иентификатор роли. </param>
        /// <param name="settingUserToRoleDto"> ДТО добавления пользователя в роль. </param>
        Task SetToRoleAsync(Guid id, Guid roleId, SettingUserToRoleDto settingUserToRoleDto);

        /// <summary>
        /// Удалить пользователя из роли.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="roleId"> Иентификатор роли. </param>
        /// <param name="deletingUserFromRoleDto"> ДТО удаления пользователя из роли. </param>
        Task DeleteFromRoleAsync(Guid id, Guid roleId, DeletingUserFromRoleDto deletingUserFromRoleDto);

        /// <summary>
        /// Назначить группу пользователю.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="userGroupId"> Иентификатор группы. </param>
        /// <param name="settingUserToUserGroupDto"> ДТО добавления пользователя в группу. </param>
        Task SetToUserGroupAsync(Guid id, Guid userGroupId, SettingUserToUserGroupDto settingUserToUserGroupDto);

        /// <summary>
        /// Удалить пользователя из роли.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="userGroupId"> Иентификатор группы. </param>
        /// <param name="deletingUserFromUserGroupDto"> ДТО удаления пользователя из группы. </param>
        Task DeleteFromUserGroupAsync(Guid id, Guid userGroupId, DeletingUserFromUserGroupDto deletingUserFromUserGroupDto);
    }
}