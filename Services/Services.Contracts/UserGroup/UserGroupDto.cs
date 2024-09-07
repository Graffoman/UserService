using Services.Contracts.User;
using Services.Contracts.Group;

namespace Services.Contracts.UserGroup
{
	public class UserGroupDto
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
        public UserDto User { get; set; }


        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Группа.
        /// </summary>
        public GroupDto Group { get; set; }
    }
}
