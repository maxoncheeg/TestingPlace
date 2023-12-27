using System;

namespace TestingPlace.Model.Testing
{
    public interface ITestSolve
    {
        public int Attempts { get; set; }
        public Guid TestId { get; }
        public double BestPoints { get; set; }
    }
}
