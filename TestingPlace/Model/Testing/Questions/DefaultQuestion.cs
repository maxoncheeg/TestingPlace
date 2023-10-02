using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions
{
    public class DefaultQuestion : Question, ITestQuestion
	{
        private DefaultQuestion(Guid id, Guid testId, string text, DefaultQuestionAnswer answer, List<DefaultQuestionAnswer> incorrectAnswers) 
            : base(id, testId, answer, text, nameof(DefaultQuestion))
        {
            _answers = new(incorrectAnswers) { _answer as IAnswer };
            ShuffleAnswers(new());
        }

        public static DefaultQuestion Create(Guid testId, string text, DefaultQuestionAnswer answer, List<DefaultQuestionAnswer> incorrectAnswers)
            => new(Guid.NewGuid(), testId, text, answer, incorrectAnswers);

        public static DefaultQuestion Create(Guid id, Guid testId, string text, DefaultQuestionAnswer answer, List<DefaultQuestionAnswer> incorrectAnswers)
            => new(id, testId, text, answer, incorrectAnswers);

        public double GetPoints() => (_answer as DefaultQuestionAnswer).Points;

        public double Answer(IAnswer[] answer) => _answer.Check(answer);
    }
}
