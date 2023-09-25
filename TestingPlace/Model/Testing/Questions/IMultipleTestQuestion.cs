using System.Collections.Generic;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions
{
    public interface IMultipleTestQuestion : ITestQuestion
    {
        public float Answer(List<IAnswer> answer);
    }
}
