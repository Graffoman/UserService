using Services.Repositories.Abstractions;
using Services.Abstractions;
using AutoMapper;
using Domain.Entities;
using Services.Contracts.User;
using System.Security.Cryptography;
using System.Text;
using Services.Contracts.Group;
using Services.Contracts.Role;
using Newtonsoft.Json;
using RabbitMQ.Abstractions;

namespace Services.Implementations
{

	/// <summary>
	/// Cервис работы с пользователями.
	/// </summary>
	public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRabbitMqProducer _rabbitMqProducer;

        public UserService(
            IMapper mapper,
            IUserRepository userRepository,
            IRabbitMqProducer rabbitMqProducer)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _rabbitMqProducer = rabbitMqProducer;
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
        /// Функция валидации email
        /// </summary>
        /// /// <param name="email> email </param>
        /// <returns> Хэш пароля. </returns>
        public static bool IsEmailValid(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Получить пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО пользователя. </returns>
        public async Task<UserDto?> GetByIdAsync(Guid id)
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
        public async Task<UserDto?> Login(UserLoginDto userLoginDto)
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
            var testuser = await _userRepository.GetByEmailAsync(creatingUserDto.Email, CancellationToken.None);
            if (testuser != null)
            {
                throw new Exception($"Пользователь с email {creatingUserDto.Email} уже существует");
            }

            if (!IsEmailValid(creatingUserDto.Email))
            {
                throw new Exception($"Email {creatingUserDto.Email} невалиден");
            }

            var user = _mapper.Map<CreatingUserDto, User>(creatingUserDto);
            user.Id = Guid.NewGuid();
            user.PasswordHash = CreateSHA256(creatingUserDto.Password);
            var createdUser = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var rmquser = new UserMessage
            {
                UserId = user.Id.ToString(),
                FirstName = creatingUserDto.Name,
                LastName = creatingUserDto.LastName,
                Department = creatingUserDto.Department,
                Email = creatingUserDto.Email
            };
            var message = JsonConvert.SerializeObject(rmquser);
            _rabbitMqProducer.SendMessage(message);

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
           
            if (!IsEmailValid(updatingUserDto.Email))
            {
                throw new Exception($"Email {updatingUserDto.Email} невалиден");
            }                

            var testuser = await _userRepository.GetByEmailAsync(updatingUserDto.Email, CancellationToken.None);
            if (!testuser.Id.Equals(id))
            {
                throw new Exception($"Пользователь с email {updatingUserDto.Email} уже существует");
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

            var rmquser = new UserMessage
            {
                UserId = id.ToString(),
                FirstName = user.Name,
                LastName = user.LastName,
                Department = user.Department,
                Email = user.Email
            };
            var message = JsonConvert.SerializeObject(rmquser);
            _rabbitMqProducer.SendMessage(message);
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

        /// <summary>
        /// Получить список групп пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Список групп  пользователя. </returns>
        public async Task<ICollection<GroupDto>> GetGroupListAsync(Guid id)
        {
            ICollection<Group> entities = await _userRepository.GetGroupListAsync(id);
            return _mapper.Map<ICollection<Group>, ICollection<GroupDto>>(entities);
        }

        /// <summary>
        /// Получить список ролей пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> Список ролей пользователя. </returns>
        public async Task<ICollection<RoleDto>> GetRoleListAsync(Guid id)
        {
            ICollection<Role> entities = await _userRepository.GetRoleListAsync(id);
            return _mapper.Map<ICollection<Role>, ICollection<RoleDto>>(entities);
        }
    }

}
