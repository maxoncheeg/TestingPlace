using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing;

namespace TestingPlace.Model.Users
{
    public class UserEntity : IEntity, IUser
    {
        public Guid Id { get; }
        public string Login { get; }
        public string Password { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsTeacher { get; set; }
        public IList<ITestSolve> Solves { get; set; }

        [JsonConstructor]
        public UserEntity(Guid Id, string Login, string Password, string Name, string Email, List<TestSolveEntity> testSolves)
        {
            this.Id = Id;
            this.Login = Login;
            this.Password = Password;
            this.Name = Name;
            this.Email = Email;
            if (testSolves != null)
                Solves = new List<ITestSolve>(testSolves);
            else Solves = new List<ITestSolve>();
        }

        public UserEntity(Guid Id, string Login, string Password, string Name, string Email)
        {
            this.Id = Id;
            this.Login = Login;
            this.Password = Password;
            this.Name = Name;
            this.Email = Email;
            Solves = new List<ITestSolve>();
        }
    }
}
