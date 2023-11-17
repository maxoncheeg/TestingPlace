using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions
{
    public class DefaultQuestion : Question, ITestQuestion
    {
        private DefaultQuestion(Guid id, Guid testId, string text, QuestionAnswer answer, List<QuestionAnswer> incorrectAnswers)
            : base(id, testId, answer, text, nameof(DefaultQuestion))
        {
            _answers = new(incorrectAnswers) { _answer as QuestionAnswer };
            ShuffleAnswers(new());
        }

        public static DefaultQuestion Create(Guid testId, string text, QuestionAnswer answer, List<QuestionAnswer> incorrectAnswers)
            => new(Guid.NewGuid(), testId, text, answer, incorrectAnswers);

        public static DefaultQuestion Create(Guid id, Guid testId, string text, QuestionAnswer answer, List<QuestionAnswer> incorrectAnswers)
            => new(id, testId, text, answer, incorrectAnswers);

        public double GetPoints() => (_answer as QuestionAnswer)?.Points ?? 0;

        public double Answer(IQuestionAnswer answer) => _answer.Check(answer);
    }
}
