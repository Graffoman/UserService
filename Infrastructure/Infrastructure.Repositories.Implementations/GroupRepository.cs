using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Services.Repositories.Abstractions;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.Group;

namespace Infrastructure.Repositories.Implementations
{
    public class GroupRepository : Repository<Group, Guid>, IGroupReposotory
    {
        public GroupRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить сущность по ID.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Группа. </returns>
        public override async Task<Group> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<Group>().AsQueryable();
              //.Include(c => c.UserGroups).AsQueryable()
           
            return await query.SingleOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Получить полный список.
        /// </summary>
        /// <returns> Список пользователей. </returns>
        public async Task<List<Group>> GetListAsync()
        {
            var query = GetAll();                
            return await query.ToListAsync();
        }

    }
}
