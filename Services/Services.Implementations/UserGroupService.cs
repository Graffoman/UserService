using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.Repositories.Abstractions;
using Services.Abstractions;
using AutoMapper;
using CommonNamespace;
using Domain.Entities;
using MassTransit;
using Services.Contracts.UserGroup;
using static MassTransit.Logging.OperationName;
using System.Security.Cryptography;
using Services.Contracts.UserRole;
using System.Text;

namespace Services.Implementations
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IMapper _mapper;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IBusControl _busControl;

        public UserGroupService(
            IMapper mapper,
            IUserGroupRepository userGroupRepository,
            IBusControl busControl)
        {
            _mapper = mapper;
            _userGroupRepository = userGroupRepository;
            _busControl = busControl;
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
