using Services.Repositories.Abstractions;
using Services.Abstractions;
using AutoMapper;
using Domain.Entities;
using Services.Contracts.UserGroup;

namespace Services.Implementations
{
	public class UserGroupService : IUserGroupService
    {
        private readonly IMapper _mapper;
        private readonly IUserGroupRepository _userGroupRepository;

        public UserGroupService(
            IMapper mapper,
            IUserGroupRepository userGroupRepository)
        {
            _mapper = mapper;
            _userGroupRepository = userGroupRepository;
        }

        /// <summary>
        /// Добавить пользователя в группу.
        /// </summary>
        /// <param name="creatingUserGroupDto"> ДТО добавления пользователя в группу. </param>
        public async Task<Guid> AddUserToGroupAsync(CreatingUserGroupDto creatingUserGroupDto)
        {
            var testgroup = await _userGroupRepository.GetByUserIdGroupIdAsync(creatingUserGroupDto.UserId, creatingUserGroupDto.GroupId);
            if (testgroup != null)
            {
                throw new Exception($"Пользователь уже состоит в группе!");
            }
            var usergroup = _mapper.Map<CreatingUserGroupDto, UserGroup>(creatingUserGroupDto);
            usergroup.Id = Guid.NewGuid();
            usergroup.UserId = creatingUserGroupDto.UserId;
            usergroup.GroupId = creatingUserGroupDto.GroupId;
            var createdusergroup = await _userGroupRepository.AddAsync(usergroup);
            await _userGroupRepository.SaveChangesAsync();
            return createdusergroup.Id;
        }

        /// <summary>
        /// Удалить пользователя из группы.
        /// </summary>
        /// <param name="creatingUserGroupDto"> ДТО добавления пользователя в группу. </param>
        public async Task DeleteUserFromGroupAsync(CreatingUserGroupDto creatingUserGroupDto)
        {
            var usergroup = await _userGroupRepository.GetByUserIdGroupIdAsync(creatingUserGroupDto.UserId, creatingUserGroupDto.GroupId);
            if (usergroup == null)
            {
                throw new Exception($"Пользователь не состоит в группе!");
            }
            _userGroupRepository.Delete(usergroup.Id);
            await _userGroupRepository.SaveChangesAsync();

        }
    }

}
