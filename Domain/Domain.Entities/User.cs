﻿using System;

namespace Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User: IEntity<Guid>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary> Имя. </summary>
        public string Name { get; set; }

        /// <summary> Фамилия. </summary>
        public string LastName { get; set; }

        /// <summary> Отчество. </summary>
        public string? MiddleName { get; set; }

        /// <summary> Дата рождения. </summary>
        public DateOnly? BirthdayDate { get; set; }

        /// <summary> Подразделение. </summary>
        public string? Department { get; set; }

        /// <summary> Email. </summary>
        public string Email { get; set; }

        /// <summary> Хэш пароля. </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Группы пользователей.
        /// </summary>
        public List<UserGroup> UserGroups { get; set; }

        /// <summary>
        /// Роли.
        /// </summary>       
        public List<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }

    }
}