using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.User
{
    public class SettingUserToRoleDto
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
