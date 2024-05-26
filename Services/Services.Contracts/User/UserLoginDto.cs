using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.User
{
    public class UserLoginDto
    {
        /// <summary> Email. </summary>
        public string Email { get; set; }

        /// <summary> Пароль </summary>
        public string Password{ get; set; }
    }
}
