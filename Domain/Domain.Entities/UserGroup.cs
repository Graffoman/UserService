using System.Collections.Generic;
using System;

namespace Domain.Entities
{
    /// <summary>
    /// Группа пользователей.
    /// </summary>
    public class UserGroup: IEntity<Guid>
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
        /// Идентификатор группы.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Пользователи.
        /// </summary>
        public Group Group { get; set; }


    }
}