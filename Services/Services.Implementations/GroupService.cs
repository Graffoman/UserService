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
using Services.Contracts.Group;
using static MassTransit.Logging.OperationName;
using System.Security.Cryptography;
using Services.Contracts.User;

namespace Services.Implementations
{
    public class GroupService : IGroupService
    { 
        private readonly IMapper _mapper;
        private readonly IGroupReposotory _groupRepository;
        private readonly IBusControl _busControl;

        public GroupService(
            IMapper mapper,
            IGroupReposotory groupRepository,
            IBusControl busControl)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _busControl = busControl;
        }

        /// <summary>
        /// Получить группу. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> ДТО группы. </returns>
        public async Task<GroupDto> GetByIdAsync(Guid id)
        {
            var group = await _groupRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<Group, GroupDto>(group);
        }

        /// <summary>
        /// Создать группу.
        /// </summary>
        /// <param name="creatingGroupDto"> ДТО группы. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<Guid> CreateAsync(CreatingGroupDto creatingGroupDto) 
        {
            var group = _mapper.Map<CreatingGroupDto, Group>(creatingGroupDto);
            group.Id = Guid.NewGuid();
            var createdGroup = await _groupRepository.AddAsync(group);
            await _groupRepository.SaveChangesAsync();
            /*
            await _busControl.Publish(new MessageDto
            {
                Content = $"Group {createdGroup.Id} with name {createdGroup.Name} is added"
            });
            */
            return createdGroup.Id;
        }

        /// <summary>
        /// Изменить группу.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingGroupDto"> ДТО группы. </param>
        public async Task UpdateAsync(Guid id, UpdatingGroupDto updatingGroupDto)
        {
            var user = await _groupRepository.GetAsync(id, CancellationToken.None);
            if (user == null)
            {
                throw new Exception($"Группа с идентфикатором {id} не найдена");
            }

            user.Name = updatingGroupDto.Name;

            _groupRepository.Update(user);
            await _groupRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить группу.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        public async Task DeleteAsync(Guid id)
        {
            var user = await _groupRepository.GetAsync(id, CancellationToken.None);
            if (user == null)
            {
                throw new Exception($"Группа с идентфикатором {id} не найдена");
            }
            user.Deleted = true;
            await _groupRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Получить полный список.
        /// </summary>
        /// <returns> Список групп. </returns>
        public async Task<ICollection<GroupDto>> GetListAsync()
        {
            ICollection<Group> entities = await _groupRepository.GetListAsync();
            return _mapper.Map<ICollection<Group>, ICollection<GroupDto>>(entities);
        }

        /// <summary>
        /// Получить список пользователей группы.
        /// </summary>
        /// <param name="id"> Идентификатор группы</param>
        /// <returns> Список  пользователей. </returns>
        public async Task<ICollection<UserDto>> GetUserListAsync(Guid id)
        {
            ICollection<User> entities = await _groupRepository.GetUserListAsync(id);
            return _mapper.Map<ICollection<User>, ICollection<UserDto>>(entities);
        }

    }

}
