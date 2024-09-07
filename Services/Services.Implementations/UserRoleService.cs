using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts.UserRole;
using Services.Repositories.Abstractions;

namespace Services.Implementations
{
	public class UserRoleService : IUserRoleService
    {
        private readonly IMapper _mapper;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(
            IMapper mapper,
            IUserRoleRepository userRoleRepository)
        {
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// Добавить роль пользователю.
        /// </summary>
        /// <param name="creatingUserRoleDto"> ДТО добавления роли пользоватьеля. </param>
        public async Task<Guid> AddUserToRoleAsync(CreatingUserRoleDto creatingUserRoleDto)
        {
            var testrole = await _userRoleRepository.GetByUserIdRoleIdAsync(creatingUserRoleDto.UserId, creatingUserRoleDto.RoleId);
            if (testrole != null)
            {
                throw new Exception($"У пользователя уже есть эта роль!");
            }
            var userrole = _mapper.Map<CreatingUserRoleDto, UserRole>(creatingUserRoleDto);
            userrole.Id = Guid.NewGuid();
            userrole.UserId = creatingUserRoleDto.UserId;
            userrole.RoleId = creatingUserRoleDto.RoleId;
            var createduserrole = await _userRoleRepository.AddAsync(userrole);
            await _userRoleRepository.SaveChangesAsync();
            return createduserrole.Id;
        }

        /// <summary>
        /// Удалить роль пользователя.
        /// </summary>
        /// <param name="creatingUserRoleDto"> ДТО добавления роли пользоватьеля. </param>
        public async Task DeleteUserFromRoleAsync(CreatingUserRoleDto creatingUserRoleDto)
        {
            var userrole = await _userRoleRepository.GetByUserIdRoleIdAsync(creatingUserRoleDto.UserId, creatingUserRoleDto.RoleId);
            if (userrole == null)
            {
                throw new Exception($"У пользователя нет этой роли!");
            }
            _userRoleRepository.Delete(userrole.Id);
            await _userRoleRepository.SaveChangesAsync();

        }
    }
}
