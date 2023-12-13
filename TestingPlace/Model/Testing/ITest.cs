using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing.Questions;

namespace TestingPlace.Model.Testing
{
    public interface ITest
    {
        public string Name { get; }
        public TestTheme Theme { get; }
        public TimeSpan Time { get; }
        public IReadOnlyCollection<ITestQuestion> Questions { get; }

        public double GetTotalPoints();
    }
}
