using System;
using System.Collections.Generic;
using TestingPlace.Data.Tests;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions
{
    public abstract class Question : QuestionEntity
    {
        protected IQuestionAnswer _answer;
        protected List<IAnswer> _answers = new();

        public IReadOnlyList<IAnswer> Answers { get => _answers; }

        public Question(Guid id, Guid testId, IQuestionAnswer answer, string text, string type) : base(id, type, text, testId)
        {
            if (answer is null) throw new ArgumentNullException(nameof(answer));

            _answer = answer;
        }

        public void ShuffleAnswers(Random random, int steps = 5) => _answers.RandomShuffle(random, steps);
    }
}
