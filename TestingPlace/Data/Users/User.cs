using System;
using System.Collections.Generic;

namespace TestingPlace.Data.Users
{
    class User : IEntity
    {
        public Guid Id { get; }
        public string Login { get; }
        public string Password { get; }
        public List<Guid> CreatedTestsIds { get; }
        public List<Guid> SolvedTestsIds { get; }
    }
}
