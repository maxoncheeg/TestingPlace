using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing;

namespace TestingPlace.Model.Users
{
    public class UserEntity : IEntity
    {
        public Guid Id { get; }
        public string Login { get; }
        public string Password { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsTeacher { get; set; }
        public List<TestSolveEntity> Solves { get; set; } = new();

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
