using System.Collections.Generic;
using Services.Contracts.UserGroup;
using Services.Contracts.Role;

namespace Services.Contracts.User
{
    /// <summary>
    /// ДТО курса.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary> Имя. </summary>
        public string Name { get; set; }

        /// <summary> Фамилия. </summary>
        public string LastName { get; set; }

        /// <summary> Отчество. </summary>
        public string? MiddleName { get; set; }

        /// <summary> Дата рождения. </summary>
        public DateOnly? BirthdayDate { get; set; }

        /// <summary> Подразделение. </summary>
        public string? Department { get; set; }

        /// <summary> Email. </summary>
        public string Email { get; set; }

        /// <summary> Хэш пароля. </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Группы пользователей.
        /// </summary>
        public virtual List<UserGroupDto>? UserGroups { get; set; }

        /// <summary>
        /// Роли.
        /// </summary>       
        public virtual List<RoleDto>? Roles { get; set; }

    }
}

