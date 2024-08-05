using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Contracts.User;
using Services.Contracts.Group;
using Services.Contracts.Role;

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
        Task<UserDto?> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить пользователя по e-mail и паролю.
        /// </summary>
        /// <param name="userLoginDto"> Дто логина пользователя </param>        
        /// <returns> ДТО пользователя. </returns>
        Task<UserDto?> Login(UserLoginDto userLoginDto);

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
        /// Получить полный список.
        /// </summary>
        /// <returns> Список пользователей. </returns>
        Task<ICollection<UserDto>> GetListAsync();

        /// <summary>
        /// Получить список групп пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Список групп пользователя. </returns>
        Task<ICollection<GroupDto>> GetGroupListAsync(Guid id);

        /// <summary>
        /// Получить список ролей пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Список ролей пользователя. </returns>
        Task<ICollection<RoleDto>> GetRoleListAsync(Guid id);
    }
}
