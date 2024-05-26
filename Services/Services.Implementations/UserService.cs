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
        /// <param name="password> Пароль. </param>
        /// <returns> Хэш пароля. </returns>
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        /// <summary>
        /// Получить пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО пользователя. </returns>
        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<User, UserDto>(user);
        }

        /// <summary>
        /// Получить пользователя по e-mail и паролю.
        /// </summary>
        /// <param name="userLoginDto"> ДТО логина пользователя </param>
        /// <returns> ДТО пользователя. </returns>
        public async Task<UserDto> Login(UserLoginDto userLoginDto)
        {            
            string PasswordHash = HashPassword(userLoginDto.Password);
            var user = await _userRepository.Login(userLoginDto, PasswordHash);
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
            user.PasswordHash = HashPassword(creatingUserDto.Password);
            var createdUser = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            /*
            await _busControl.Publish(new MessageDto
            {
                Content = $"Course {createdCourse.Id} with name {createdCourse.Name} is added"
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
            user.PasswordHash = HashPassword(updatingUserDto.Password);

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
            user.Deleted = true;
            await _userRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="filterDto"> ДТО фильтра. </param>
        /// <returns> Список пользователей. </returns>
        public async Task<ICollection<UserDto>> GetPagedAsync(UserFilterDto filterDto)
        {
            ICollection<User> entities = await _userRepository.GetPagedAsync(filterDto);
            return _mapper.Map<ICollection<User>, ICollection<UserDto>>(entities);
        }

        /// <summary>
        /// Получить полный список.
        /// </summary>
        /// <returns> Список пользователей. </returns>
        public async Task<ICollection<UserDto>> GetListAsync()
        {
            ICollection<User> entities = await _userRepository.GetListAsync();
            return _mapper.Map<ICollection<User>, ICollection<UserDto>>(entities);
        }

    }

}
