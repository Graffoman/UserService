﻿using Services.Repositories.Abstractions;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

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
        public override async Task<Group>? GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = Context.Set<Group>().AsQueryable();
              //.Include(c => c.UserGroups).AsQueryable()
           
            return await query.SingleOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Получить полный список.
        /// </summary>
        /// <returns> Список групп. </returns>
        public async Task<List<Group>>? GetListAsync()
        {
            var query = GetAll()
                 .Where(c => !c.Deleted);
            return await query.ToListAsync();
        }

        /// <summary>
        /// Получить список пользователей группы.
        /// </summary>
        /// <param name="id"> Идентификатор группы. </param>
        /// <returns> Список пользоваетелей. </returns>
        public async Task<List<User>>? GetUserListAsync(Guid id)
        {
            var users = Context.Set<User>().AsQueryable()
                            .Where(c => !c.Deleted);
            var usergroups = Context.Set<UserGroup>().AsQueryable()
                            .Where(c => c.GroupId == id);

            List<Guid> userSearchListIds = usergroups.Select(x => x.UserId).ToList();

            users = users.Where(x => userSearchListIds.Contains(x.Id));

            return await users.ToListAsync();
        }

        /// <summary>
        /// Получить список пользователей, не входящих в группу.
        /// </summary>
        /// <param name="id"> Идентификатор группы. </param>
        /// <returns> Список пользователей. </returns>
        public async Task<List<User>>? GetUserNotInGroupListAsync(Guid id)
        {
            var allusers = Context.Set<User>().AsQueryable()
                            .Where(c => !c.Deleted);

            var usergroups = Context.Set<UserGroup>().AsQueryable()
                             .Where(c => c.GroupId == id);

            List<Guid> userSearchListIds = usergroups.Select(x => x.UserId).ToList();

            var usersingroup = allusers.Where(x => userSearchListIds.Contains(x.Id));

            return await allusers.Except(usersingroup).ToListAsync();
        }
    }

}

