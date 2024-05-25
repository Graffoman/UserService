using Services.Contracts.Role;
using Services.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.UserRole
{
    public class UserRoleDto
    {

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public UserDto User { get; set; }


        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Роль.
        /// </summary>
        public RoleDto Role { get; set; }
    }
}
