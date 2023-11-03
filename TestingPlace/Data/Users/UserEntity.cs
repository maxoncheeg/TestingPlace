using System;
using System.Collections.Generic;

namespace TestingPlace.Data.Users
{
    class UserEntity : IEntity
    {
        public Guid Id { get; }
        public string Login { get; }
        public string Password { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsTeacher { get; set; }
        public List<Guid> CreatedTestsIds { get; set; } = new();
        public List<Guid> SolvedTestsIds { get; set; } = new();

        public UserEntity(Guid Id, string Login, string Password, string Name, string Email)
        {
            this.Id = Id;
            this.Login = Login;
            this.Password = Password;
            this.Name = Name;
            this.Email = Email;
        }
    }
}
