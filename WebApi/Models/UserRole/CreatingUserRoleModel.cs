using Services.Contracts.Role;
using Services.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models.UserRole
{
    public class CreatingUserRoleModel
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public Guid RoleId { get; set; }


    }
}
