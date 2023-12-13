using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlace.Model.Testing;

namespace TestingPlace.Model.Users
{
    public interface IUser
    {
        public string Login { get; }
        public string Password { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsTeacher { get; set; }
        public IList<ITestSolve> Solves { get; set; }
    }
}
