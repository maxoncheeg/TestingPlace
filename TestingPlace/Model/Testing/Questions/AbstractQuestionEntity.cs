using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions
{
    public abstract class AbstractQuestionEntity : IEntity, ITestQuestion
    {
        [JsonProperty("answer")] protected IQuestionAnswer _answer;
        [JsonProperty("incorrectAnswers")] protected List<IQuestionAnswer> _incorrectAnswers;
        private List<IQuestionAnswer> _answerList;

        public Guid Id { get; }
        public string? ImagePath { get; set; }
        public Guid TestId { get; }
        public string Type { get; }
        public string Text { get; }
        public IReadOnlyList<IQuestionAnswer> Answers => _answerList;

        IReadOnlyCollection<IQuestionAnswer> ITestQuestion.Answers => throw new NotImplementedException();

        public AbstractQuestionEntity(Guid id, Guid testId, IQuestionAnswer answer, List<IQuestionAnswer> incorrectAnswers, string text, string type)
        {
            if (answer is null) throw new ArgumentNullException(nameof(answer));

            Id = id;
            Type = type;
            Text = text;
            TestId = testId;
            _incorrectAnswers = incorrectAnswers;

            _answer = answer;
            _answerList = new(incorrectAnswers) { _answer };
        }

        public void ShuffleAnswers(Random random, int steps = 5) => _answerList.RandomShuffle(random, steps);

        public double GetPoints() => _answerList.Sum(answer => answer.Points);

        public double Answer(IQuestionAnswer answer) => _answer.Check(answer);
    }
}
