using Services.Contracts.Group;
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
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> ДТО группы. </returns>
        Task<GroupDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

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
    }
}
