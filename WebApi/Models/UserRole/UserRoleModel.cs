using WebApi.Models.Role;
using WebApi.Models.User;

namespace WebApi.Models.UserRole
{
    public class UserRoleModel
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
        /// Пользователь.
        /// </summary>
        public UserModel User { get; set; }


        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Роль.
        /// </summary>
        public RoleModel Role { get; set; }
    }
}
