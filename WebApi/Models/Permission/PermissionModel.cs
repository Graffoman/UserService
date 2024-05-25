using System.Collections.Generic;
using WebApi.Models.Role;

namespace WebApi.Models.Permission
{
    /// <summary>
    /// ДТО Роли.
    /// </summary>
    public class PermissionModel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary> Наименование. </summary>
        public string Name { get; set; }

        /// <summary> Описание. </summary>
        public string? Description { get; set; }

        /// <summary> Роль. </summary>
        public RoleModel Role { get; set; }

        /// <summary>ID роли. </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }
    }
}
