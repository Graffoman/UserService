using Services.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Contracts.UserGroup
{
    public class CreatingUserGroupDto
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public Guid GroupId { get; set; }
    }
}
