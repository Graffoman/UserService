using System.Collections.Generic;
using WebApi.Models.Group;
using WebApi.Models.Role;

namespace WebApi.Models.User
{
    /// <summary>
    /// ДТО курса.
    /// </summary>
    public class UserModel
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

        /// <summary>
        /// Группы пользователей.
        /// </summary>
        public List<GroupModel> Groups { get; set; }

        /// <summary>
        /// Роли пользователей.
        /// </summary>       
        public List<RoleModel> Roles { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }

    }
}

