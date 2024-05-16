using System.Collections.Generic;
using System;

namespace Domain.Entities
{
    /// <summary>
    /// Разрешение.
    /// </summary>
    public class Permission: IEntity<Guid>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary> Наименование. </summary>
        public string Name { get; set; }

        /// <summary> Описание. </summary>
        public string? Description { get; set; }

        /// <summary> Группа пользователей. </summary>
        public UserGroup UserGroup { get; set; }

        /// <summary>ID Группы пользователей. </summary>
        public Guid UserGroupId { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }
    }
}