using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserRole: IEntity<Guid>
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
        /// Пользователи.
        /// </summary>
        public User User { get; set; }


        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Пользователи.
        /// </summary>
        public Role Role { get; set; }
    }
}
