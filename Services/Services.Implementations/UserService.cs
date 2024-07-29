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
using Services.Contracts.User;
using static MassTransit.Logging.OperationName;
using System.Security.Cryptography;
using Services.Contracts.UserRole;
using System.Text;
using Services.Contracts.Group;
using Services.Contracts.Role;

namespace Services.Implementations
{

    /// <summary>
    /// Cервис работы с пользователями.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IBusControl _busControl;

        public UserService(
            IMapper mapper,
            IUserRepository userRepository,
            IBusControl busControl)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _busControl = busControl;
        }

        /// <summary>
        /// Функция хэширования
        /// </summary>
        /// <param name="input> Текст, для которого вычисляется хэш </param>
        /// <returns> Хэш пароля. </returns>
        public static string CreateSHA256(string input)
        {
            using SHA256 hash = SHA256.Create();
            return Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }

        /// <summary>
        /// Получить пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО пользователя. </returns>
        public async Task<UserDto>? GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id, CancellationToken.None);

            UserDto userdto = _mapper.Map<User, UserDto>(user);
            ICollection<Role> roleentities = await _userRepository.GetRoleListAsync(id);
            userdto.Roles = _mapper.Map<ICollection<Role>, ICollection<RoleDto>>(roleentities).ToList();

            ICollection<Group> groupentities = await _userRepository.GetGroupListAsync(id);
            userdto.Groups = _mapper.Map<ICollection<Group>, ICollection<GroupDto>>(groupentities).ToList();

            return userdto;
        }

        /// <summary>
        /// Получить пользователя по e-mail и паролю.
        /// </summary>
        /// <param name="userLoginDto"> ДТО логина пользователя </param>
        /// <returns> ДТО пользователя. </returns>
        public async Task<UserDto>? Login(UserLoginDto userLoginDto)
        {
            string PasswordHash = CreateSHA256(userLoginDto.Password);
            var user = await _userRepository.LoginAsync(userLoginDto, PasswordHash);
            return _mapper.Map<User, UserDto>(user);
        }

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        /// <param name="creatingUserDto"> ДТО создаваемого пользователя. </param>
        public async Task<Guid> CreateAsync(CreatingUserDto creatingUserDto)
        {
            var user = _mapper.Map<CreatingUserDto, User>(creatingUserDto);
            user.Id = Guid.NewGuid();
            user.PasswordHash = CreateSHA256(creatingUserDto.Password);
            var createdUser = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            /*
            await _busControl.Publish(new MessageDto
            {
                Content = $"User {createdUser.Id} with name {createdUser.Name} is added"
            });
            */
            return createdUser.Id;
        }

        /// <summary>
        /// Изменить пользователя.
        /// </summary>
        /// <param name="id"> Иентификатор. </param>
        /// <param name="updatingUserDto"> ДТО редактируемого пользователя. </param>
        public async Task UpdateAsync(Guid id, UpdatingUserDto updatingUserDto)
        {
            var user = await _userRepository.GetAsync(id, CancellationToken.None);
            if (user == null)
            {
                throw new Exception($"Пользователь с идентфикатором {id} не найден");
            }

            user.Name = updatingUserDto.Name;
            user.LastName = updatingUserDto.LastName;
            user.MiddleName = updatingUserDto.MiddleName;
            user.BirthdayDate = updatingUserDto.BirthdayDate;
            user.Department = updatingUserDto.Department;
            user.Email = updatingUserDto.Email;
            if (!string.IsNullOrEmpty(updatingUserDto.Password))
            { 
                user.PasswordHash = CreateSHA256(updatingUserDto.Password);
            }

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        public async Task DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id, CancellationToken.None);
            if (user == null)
            {
                throw new Exception($"Пользователь с идентфикатором {id} не найден");
            }
            user.Deleted = true;
            await _userRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="filterDto"> ДТО фильтра. </param>
        /// <returns> Список пользователей. </returns>
        public async Task<ICollection<UserDto>>? GetPagedAsync(UserFilterDto filterDto)
        {
            ICollection<User> entities = await _userRepository.GetPagedAsync(filterDto);
            return _mapper.Map<ICollection<User>, ICollection<UserDto>>(entities);
        }

        /// <summary>
        /// Получить полный список.
        /// </summary>
        /// <returns> Список пользователей. </returns>
        public async Task<ICollection<UserDto>>? GetListAsync()
        {
            ICollection<User> entities = await _userRepository.GetListAsync();
            return _mapper.Map<ICollection<User>, ICollection<UserDto>>(entities);
        }

        /// <summary>
        /// Получить список групп пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Список групп  пользователя. </returns>
        public async Task<ICollection<GroupDto>>? GetGroupListAsync(Guid id)
        {
            ICollection<Group> entities = await _userRepository.GetGroupListAsync(id);
            return _mapper.Map<ICollection<Group>, ICollection<GroupDto>>(entities);
        }

        /// <summary>
        /// Получить список ролей пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Список ролей пользователя. </returns>
        public async Task<ICollection<RoleDto>>? GetRoleListAsync(Guid id)
        {
            ICollection<Role> entities = await _userRepository.GetRoleListAsync(id);
            return _mapper.Map<ICollection<Role>, ICollection<RoleDto>>(entities);
        }
    }

}
