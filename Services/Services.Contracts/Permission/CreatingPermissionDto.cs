﻿using Services.Contracts.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Permission
{
    public class CreatingPermissionDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary> Наименование. </summary>
        public string Name { get; set; }

        /// <summary> Описание. </summary>
        public string? Description { get; set; }

        /// <summary>ID роли. </summary>
        public Guid RoleId { get; set; }
    }
}
