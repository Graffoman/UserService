using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.User
{
    public class SettingUserToUserGroupDto
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public Guid UserGroupId { get; set; }
    }
}
