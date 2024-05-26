using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Services.Repositories.Abstractions;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.User;
using Services.Contracts.UserGroup;
using Services.Contracts.UserRole;

namespace Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Репозиторий работы с пользователями.
    /// </summary>
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить сущность по ID.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Пользователь. </returns>
        public override async Task<User> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<User>().AsQueryable();
                //.Include(c => c.UserGroups).AsQueryable()
                //.Include(c => c.UserRoles).AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Получить пользователя по Email, PasswordHash
        /// </summary>
        /// <param name="email"> Email пользователя. </param>
        /// <param name="passwordHash"> Hash пароля пользователя</param>
        /// <param name="cancellationToken"></param>
        /// <returns> Пользователь. </returns>
        public async Task<User> GetAsyncByEmailPassword(string email, string passwordHash)
        {
            var query = GetAll()
                .Where(c => !c.Deleted);

            query = query.Where(c => c.Email == email);
            query = query.Where(c => c.PasswordHash == passwordHash);
                        
            return await query.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="filterDto"> ДТО фильтра. </param>
        /// <returns> Список пользователей. </returns>
        public async Task<List<User>> GetPagedAsync(UserFilterDto filterDto)
        {
            var query = GetAll()
                .Where(c => !c.Deleted);
            //.Include(c => c.Lessons).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filterDto.Name))
            {
                query = query.Where(c => c.Name == filterDto.Name);
            }

            if (!string.IsNullOrWhiteSpace(filterDto.Department))
            {
                query = query.Where(c => c.Department == filterDto.Department);
            }

            query = query
                .Skip((filterDto.Page - 1) * filterDto.ItemsPerPage)
                .Take(filterDto.ItemsPerPage);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Получить полный список.
        /// </summary>
        /// <returns> Список пользователей. </returns>
        public async Task<List<User>> GetListAsync()
        {
            var query = GetAll();
            return await query.ToListAsync();
        }

    }
    
}
