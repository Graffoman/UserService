using Services.Contracts.Group;
using Services.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    /// <summary>
    /// Интерфейс сервиса работы с группами.
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// Получить группу. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>       
        /// <returns> ДТО группы. </returns>
        Task<GroupDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Создать группу.
        /// </summary>
        /// <param name="creatingGroupDto"> ДТО группы. </param>
        /// <returns> Идентификатор. </returns>
        Task<Guid> CreateAsync(CreatingGroupDto creatingGroupDto);

        /// <summary>
        /// Изменить группу.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingGroupDto"> ДТО группы. </param>
        Task UpdateAsync(Guid id, UpdatingGroupDto updatingGroupDto);

        /// <summary>
        /// Удалить группу.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Получить полный список.
        /// </summary>
        /// <returns> Список групп. </returns>
        Task<ICollection<GroupDto>> GetListAsync();

        /// <summary>
        /// Получить список пользователей группы.
        /// </summary>
        /// <param name="id"> Идентификатор группы </param>
        /// <returns> Список пользователей. </returns>
        Task<ICollection<UserDto>> GetUserListAsync(Guid id);

        /// <summary>
        /// Получить список пользователей, не состоящих в группе.
        /// </summary>
        /// <param name="id"> Идентификатор группы </param>
        /// <returns> Список пользователей. </returns>
        Task<ICollection<UserDto>> GetUserNotInGroupListAsync(Guid id);
    }
}
