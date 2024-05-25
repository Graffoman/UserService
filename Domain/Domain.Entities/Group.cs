using System.Collections.Generic;
using System;

namespace Domain.Entities
{
    /// <summary>
    /// Группа.
    /// </summary>
    public class Group : IEntity<Guid>
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
        public virtual List<UserGroup> UserGroups { get; set; }


        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }
    }
}
