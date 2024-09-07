using Services.Contracts.Permission;

namespace Services.Abstractions
{
	/// <summary>
	/// Интерфейс сервиса работы с правами доступа.
	/// </summary>
	public interface IPermissionService
    {
        /// <summary>
        /// Получить право доступа. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> ДТО права доступа. </returns>
        Task<PermissionDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создать право доступа.
        /// </summary>
        /// <param name="creatingPermissionDto"> ДТО права доступа. </param>
        /// <returns> Идентификатор. </returns>
        Task<Guid> CreateAsync(CreatingPermissionDto creatingPermissionDto);

        /// <summary>
        /// Изменить право доступа.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingPermissionDto"> ДТО права доступа. </param>
        Task UpdateAsync(Guid id, UpdatingPermissionDto updatingPermissionDto);

        /// <summary>
        /// Удалить право доступа.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);
        
    }
}