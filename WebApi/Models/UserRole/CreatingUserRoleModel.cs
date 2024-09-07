namespace WebApi.Models.UserRole
{
    public class CreatingUserRoleModel
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
