using System;

namespace TestingPlace.Model.Testing
{
    public enum TestTheme
    {
        Любая,
        Математика,
        Неведомое,
        Смешной,
        Физика,
        Программирование,
        Кулинария
    }

    public class TestSolveEntity : IEntity, ITestSolve
    {
        public Guid Id { get; }
        public Guid TestId { get; }
        public Guid UserId { get; }
        public int Attempts { get; set; }
        public double BestPoints { get; set; }

        public ITest Test { get; }

        public TestSolveEntity(Guid Id, Guid TestId, Guid UserId, double BestPoints, int Attempts, ITest test)
        {
            this.Id = Id;
            this.TestId = TestId;
            this.UserId = UserId;
            this.BestPoints = BestPoints;
            this.Attempts = Attempts;
            Test = test;
        }
    }
}
