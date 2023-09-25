using System;

namespace TestingPlace.Data.Users
{
    public class LoggedUser : Entity
    {
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
