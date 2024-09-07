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
