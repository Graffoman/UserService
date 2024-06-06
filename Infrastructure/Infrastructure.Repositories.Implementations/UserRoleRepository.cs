using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class UserRoleRepository : Repository<UserRole, Guid>, IUserRoleRepository
    {
        public UserRoleRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить сущность по ID.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Роль пользователя. </returns>
        public override async Task<UserRole> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<UserRole>().AsQueryable();
            //.Include(c => c.UserRoles).AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Получить связь пользователя и роли
        /// </summary>
        /// <param name="userId"> ID пользователя. </param>     
        /// /// <param name="roleId"> ID роли. </param>  
        /// <returns> Связь пользователя и роли. </returns>
        public async Task<UserRole> GetByUserIdRoleIdAsync(Guid userId, Guid roleId)
        {
            var query = GetAll();

            query = query.Where(c => c.UserId == userId);
            query = query.Where(c => c.RoleId == roleId);

            return await query.SingleOrDefaultAsync();

        }
    }
}
