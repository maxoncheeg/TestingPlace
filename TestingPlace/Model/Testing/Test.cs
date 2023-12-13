using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestingPlace.Model.Testing.Questions;

namespace TestingPlace.Model.Testing
{
    public class Test : AbstractTestEntity
    {
        [JsonProperty("questions")] private List<ITestQuestion> _questions;

        [JsonIgnore]
        public ITestQuestion this[int index]
		{
			get => _questions[index];
		}
        [JsonIgnore]
        public int QuestionCount { get => _questions.Count; }
        [JsonIgnore]
        public override IReadOnlyCollection<ITestQuestion> Questions => _questions;

        [JsonConstructor]
        public Test(Guid Id, string Name, TestTheme theme, Guid AuthorId, List<ITestQuestion> questions, TimeSpan time)
			: base(Id, Name, theme, AuthorId, time)
		{
			_questions = new(questions);
        }

        public IEnumerator GetEnumerator()
        {
			for (int i = 0; i < QuestionCount; i++)
				yield return this[i];
        }

        public override double GetTotalPoints() => _questions.Sum(question => question.GetPoints());      
    }
}
