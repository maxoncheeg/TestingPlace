using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPlace.Data.Users
{
    internal class TestSolutionEntity : IEntity
    {
        public Guid Id { get; }
        public Guid TestId { get; }
        public Guid UserId { get; }
    }
}
