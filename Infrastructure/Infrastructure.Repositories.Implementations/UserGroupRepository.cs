using Domain.Entities;
using Infrastructure.EntityFramework;
using Services.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
	public class UserGroupRepository : Repository<UserGroup, Guid>, IUserGroupRepository
    {
        public UserGroupRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить сущность по ID.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Группа пользователя. </returns>
        public override async Task<UserGroup>? GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<UserGroup>().AsQueryable();
            //.Include(c => c.UserGroups).AsQueryable()
            //.Include(c => c.UserRoles).AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Получить связь пользователя и группы
        /// </summary>
        /// <param name="userId"> ID пользователя. </param>     
        /// /// <param name="groupId"> ID группы. </param>  
        /// <returns> Пользователь. </returns>
        public async Task<UserGroup>? GetByUserIdGroupIdAsync(Guid userId, Guid groupId)
        {
            var query = GetAll();         

            query = query.Where(c => c.UserId == userId);
            query = query.Where(c => c.GroupId == groupId);

            return await query.SingleOrDefaultAsync();

        }
    }
}
