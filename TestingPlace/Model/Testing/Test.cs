using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing.Questions;

namespace TestingPlace.Model.Testing
{
	[Serializable]
	internal class Test
	{
		private List<ITestQuestion> _questions;

		public ITestQuestion this[int index]
		{
			get => _questions [index];
		}

		public int QuestionCount { get => _questions.Count; }

		public Test(List<ITestQuestion> questions)
		{
            _questions = questions;
		}
    }
}
