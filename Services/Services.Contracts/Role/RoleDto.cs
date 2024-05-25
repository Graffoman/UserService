using System.Collections.Generic;
using System;
using Services.Contracts.Permission;
using Services.Contracts.UserRole;

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
        public List<PermissionDto> Permissions { get; set; }

        /// <summary>
        /// Роли пользователей.
        /// </summary>
        public List<UserRoleDto> UserRoles { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }

    }
}

