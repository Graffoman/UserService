﻿namespace Services.Contracts.Role
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
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }

    }
}

