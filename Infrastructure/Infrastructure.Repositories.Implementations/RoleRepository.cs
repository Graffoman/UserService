using Services.Repositories.Abstractions;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class RoleRepository :Repository<Role, Guid>, IRoleRepository
    {
        public RoleRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить сущность по ID.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Группа. </returns>
        public override async Task<Role> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<Role>().AsQueryable();
            //.Include(c => c.UserGroups).AsQueryable()

            return await query.SingleOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Получить полный список.
        /// </summary>
        /// <returns> Список ролей. </returns>
        public async Task<List<Role>> GetListAsync()
        {
            var query = GetAll();
            return await query.ToListAsync();
        }
    }
}
