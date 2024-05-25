using System.Collections.Generic;

namespace WebApi.Models.Role
{
    public class CreatingRoleModel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }
    }
}
