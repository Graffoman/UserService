using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Contracts.User;

namespace Services.Contracts.UserGroup
{
    public class UserGroupDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пользователи.
        /// </summary>
        public virtual List<UserDto>? Users { get; set; }
    }
}
