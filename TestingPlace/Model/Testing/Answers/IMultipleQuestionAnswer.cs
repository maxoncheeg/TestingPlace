using System.Collections.Generic;

namespace TestingPlace.Model.Testing.Answers
{
    public interface IMultipleQuestionAnswer : IQuestionAnswer
    {
        public float Check(List<IAnswer> answers);
    }
}
