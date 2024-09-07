using Domain.Entities;

namespace Services.Repositories.Abstractions
{
	/// <summary>
	/// Репозиторий работы правами.
	/// </summary>
	public interface IPermissionRepository : IRepository<Permission, Guid>
    {
    }
}
