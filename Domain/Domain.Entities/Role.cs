using System.Collections.Generic;
using System;

namespace Domain.Entities
{
    /// <summary>
    /// Роль.
    /// </summary>
    public class Role : IEntity<Guid>
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
        /// Права доступа.
        /// </summary>
        public virtual List<Permission>? Permissions { get; set; }

        /// <summary>
        /// Пользователи.
        /// </summary>
        public virtual List<User>? Users { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }
    }
}
