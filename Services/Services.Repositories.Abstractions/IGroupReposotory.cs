using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы группами.
    /// </summary>
    public interface IGroupReposotory : IRepository<Role, Guid>
    {
    }
}
