using System.Collections.Generic;
using System;
using Services.Contracts.Permission;

namespace Services.Contracts.Role
{
    /// <summary>
    /// ДТО Роли.
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Права доступа.
        /// </summary>
        public List<PermissionDto>? Permissions { get; set; }

    }
}

