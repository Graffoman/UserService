using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Contracts.Role;

namespace Services.Contracts.Permission
{
    /// <summary>
    /// ДТО Роли.
    /// </summary>
    public class PermissionDto
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
        public RoleDto Role { get; set; }

        /// <summary>ID роли. </summary>
        public Guid RoleId { get; set; }
    }
}
