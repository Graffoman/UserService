﻿namespace RabbitMQ.Abstractions
{
    public class UserMessage
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
    }
}
