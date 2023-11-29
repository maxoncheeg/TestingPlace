using System;
using TestingPlace.Model;

namespace TestingPlace.Model.Testing
{
    public class TestSolveEntity : IEntity
    {
        public Guid Id { get; }
        public Guid TestId { get; }
        public Guid UserId { get; }
        public int Attempts { get; set; }
        public double BestPoints { get; set; }

        public TestSolveEntity(Guid Id, Guid TestId, Guid UserId, double BestPoints, int Attempts)
        {
            this.Id = Id;
            this.TestId = TestId;
            this.UserId = UserId;
            this.BestPoints = BestPoints;
            this.Attempts = Attempts;
        }
    }
}
