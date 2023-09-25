using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions
{
    [Serializable]
    public class DefaultQuestion : Question, ITestQuestion
	{
        public DefaultQuestion(DefaultQuestionAnswer answer, List<Answer> incorrectAnswers, string text) 
            : base(answer, text)
        {
            _answers = new(incorrectAnswers) { _answer as IAnswer };
            ShuffleAnswers(new());
        }

        public float GetPoints() => _answer.Points;

        public float Answer(IAnswer answer) => _answer.Check(answer);
    }
}
