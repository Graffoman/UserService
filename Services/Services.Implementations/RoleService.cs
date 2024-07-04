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
using Services.Contracts.Role;
using static MassTransit.Logging.OperationName;
using System.Security.Cryptography;
using Services.Contracts.User;
using Services.Contracts.Role;

namespace Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IBusControl _busControl;

        public RoleService(
            IMapper mapper,
            IRoleRepository roleRepository,
            IBusControl busControl)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _busControl = busControl;
        }

        /// <summary>
        /// Получить роль. 
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> ДТО роли. </returns>
        public async Task<RoleDto> GetByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<Role, RoleDto>(role);
        }

        /// <summary>
        /// Создать роль.
        /// </summary>
        /// <param name="creatingRoleDto"> ДТО роли. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<Guid> CreateAsync(CreatingRoleDto creatingRoleDto)
        {
            var role = _mapper.Map<CreatingRoleDto, Role>(creatingRoleDto);
            role.Id = Guid.NewGuid();
            var createdRole = await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();
            /*
            await _busControl.Publish(new MessageDto
            {
                Content = $"Role {createdRole.Id} with name {createdRole.Name} is added"
            });
            */
            return createdRole.Id;
        }

        /// <summary>
        /// Изменить роль.
        /// </summary>
        /// <param name="id"> Идентификатор роли. </param>
        /// <param name="updatingRoleDto"> ДТО роли. </param>
        public async Task UpdateAsync(Guid id, UpdatingRoleDto updatingRoleDto)
        {
            var user = await _roleRepository.GetAsync(id, CancellationToken.None);
            if (user == null)
            {
                throw new Exception($"Роль с идентфикатором {id} не найдена");
            }

            user.Name = updatingRoleDto.Name;

            _roleRepository.Update(user);
            await _roleRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить роль.
        /// </summary>
        /// <param name="id"> Идентификатор роли. </param>
        public async Task DeleteAsync(Guid id)
        {
            var user = await _roleRepository.GetAsync(id, CancellationToken.None);
            if (user == null)
            {
                throw new Exception($"Роль с идентфикатором {id} не найдена");
            }
            user.Deleted = true;
            await _roleRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Получить полный список ролей.
        /// </summary>
        /// <returns> Список ролей. </returns>
        public async Task<ICollection<RoleDto>> GetListAsync()
        {
            ICollection<Role> entities = await _roleRepository.GetListAsync();
            return _mapper.Map<ICollection<Role>, ICollection<RoleDto>>(entities);
        }

        /// <summary>
        /// Получить список пользователей с ролью.
        /// </summary>
        /// <param name="id"> Идентификатор роли </param>
        /// <returns> Список пользователей. </returns>
        public async Task<ICollection<UserDto>> GetUserListAsync(Guid id)
        {
            ICollection<User> entities = await _roleRepository.GetUserListAsync(id);
            return _mapper.Map<ICollection<User>, ICollection<UserDto>>(entities);
        }

        /// <summary>
        /// Получить список пользователей, у которых нет роли.
        /// </summary>
        /// <param name="id"> Идентификатор роли </param>
        /// <returns> Список пользователей. </returns>
        public async Task<ICollection<UserDto>> GetUserNotInRoleListAsync(Guid id)          
        {
            ICollection<User> entities = await _roleRepository.GetUserNotInRoleListAsync(id);
            return _mapper.Map<ICollection<User>, ICollection<UserDto>>(entities);
        }
}
}
