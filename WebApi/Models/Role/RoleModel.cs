using Services.Contracts.UserRole;
using System.Collections.Generic;
using WebApi.Models.Permission;

namespace WebApi.Models.Role
{
    /// <summary>
    /// ДТО Роли.
    /// </summary>
    public class RoleModel
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
        public List<PermissionModel> Permissions { get; set; }

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

