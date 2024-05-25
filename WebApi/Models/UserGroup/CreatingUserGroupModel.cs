using Services.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApi.Models.UserGroup
{
    public class CreatingUserGroupModel
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
