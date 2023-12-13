using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions
{
    public class DefaultQuestion : AbstractQuestionEntity, ITestQuestion
    {
        public DefaultQuestion(Guid id, Guid testId, string text, QuestionAnswer answer, List<IQuestionAnswer> incorrectAnswers)
            : base(id, testId, answer, incorrectAnswers, text, nameof(DefaultQuestion))
        {
            ShuffleAnswers(new());
        }
    }
}
