using WebApi.Models.User;
using WebApi.Models.Group;

namespace WebApi.Models.UserGroup
{
    public class UserGroupModel
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
        /// Идентификатор группы.
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// Группа.
        /// </summary>
        public GroupModel Group { get; set; }
    }
}
