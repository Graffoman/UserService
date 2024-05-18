using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.User
{
    public class CreatingUserDto
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
    }
}
