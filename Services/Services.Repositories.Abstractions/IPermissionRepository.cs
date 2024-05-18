using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы правами.
    /// </summary>
    public interface IPermissionRepository : IRepository<Permission, Guid>
    {
    }
}
