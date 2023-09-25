using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions
{
    [Serializable]
    public abstract class Question
    {
        protected IQuestionAnswer _answer;
        protected List<IAnswer> _answers;

        public string Text { get; private set; }
        public IReadOnlyList<IAnswer> Answers { get => _answers; }

        public Question(IQuestionAnswer answer, string text)
        {
            _answer = answer;
            Text = text;
        }

        public void ShuffleAnswers(Random random, int steps = 5) => _answers.RandomShuffle(random, steps);
    }
}
