using System;
using System.Collections;
using System.Collections.Generic;
using TestingPlace.Data.Tests;
using TestingPlace.Model.Testing.Questions;

namespace TestingPlace.Model.Testing
{
	internal class Test : TestEntity, IEnumerable
	{
		private List<ITestQuestion> _questions = new();

		public ITestQuestion this[int index]
		{
			get => _questions[index];
		}

		public int QuestionCount { get => _questions.Count; }

		private Test(Guid Id, string Name, List<ITestQuestion> _questions)
			: base(Id, Name)
		{
			this._questions = new(_questions);
        }

		public static Test Create(Guid id, string name, List<ITestQuestion> questions) => 
			new(id, name, questions);

        public static Test Create(string name, List<ITestQuestion> questions) =>
			new(Guid.NewGuid(), name, questions);

        public IEnumerator GetEnumerator()
        {
			for (int i = 0; i < QuestionCount; i++)
				yield return this[i];
        }
    }
}
