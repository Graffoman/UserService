using Services.Repositories.Abstractions;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Contracts.User;

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
        public override async Task<User>? GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<User>().AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Получить пользователя по Email, PasswordHash
        /// </summary>
        /// <param name="email"> Email пользователя. </param>
        /// <param name="passwordHash"> Hash пароля пользователя</param>
        /// <param name="cancellationToken"></param>
        /// <returns> Пользователь. </returns>
        public async Task<User>? LoginAsync(UserLoginDto userLoginDto, string PasswordHash)
        {
            var query = GetAll()
                .Where(c => !c.Deleted);

            query = query.Where(c => c.Email == userLoginDto.Email);
            query = query.Where(c => c.PasswordHash == PasswordHash);

            return await query.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="filterDto"> ДТО фильтра. </param>
        /// <returns> Список пользователей. </returns>
        public async Task<List<User>>? GetPagedAsync(UserFilterDto filterDto)
        {
            var query = GetAll()
                .Where(c => !c.Deleted);
           
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
        public async Task<List<User>>? GetListAsync()
        {
            var query = GetAll()
                .Where(c => !c.Deleted);
            return await query.ToListAsync();
        }

        /// <summary>
        /// Получить список групп пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Список групп. </returns>
        public async Task<List<Group>>? GetGroupListAsync(Guid id)
        {
            var groups = Context.Set<Group>().AsQueryable()
                            .Where(c => !c.Deleted);
            var usergroups = Context.Set<UserGroup>().AsQueryable()
                            .Where(c => c.UserId == id);

            List<Guid> groupSearchListIds = usergroups.Select(x => x.GroupId).ToList();

            groups = groups.Where(x => groupSearchListIds.Contains(x.Id));

            return await groups.ToListAsync();
        }

        /// <summary>
        /// Получить список ролей пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Список ролей. </returns>
        public async Task<List<Role>>? GetRoleListAsync(Guid id)
        {
            var roles = Context.Set<Role>().AsQueryable()
                            .Where(c => !c.Deleted);
            var userroles = Context.Set<UserRole>().AsQueryable()
                            .Where(c => c.UserId == id);

            List<Guid> roleSearchListIds = userroles.Select(x => x.RoleId).ToList();

            roles = roles.Where(x => roleSearchListIds.Contains(x.Id));

            return await roles.ToListAsync();
        }

        /// <summary>
        /// Получить пользователя по email
        /// </summary>
        /// <param name="email"> email пользователя. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Пользователь. </returns>
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var query = Context.Set<User>().AsQueryable();
            return await query.SingleOrDefaultAsync(c => c.Email == email);
        }
    }
    
}
