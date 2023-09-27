using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TestingPlace.Model.Testing.Questions;

namespace TestingPlace.Model.Testing
{
	[Serializable]
	internal class Test
	{
		[XmlAttribute]
		private List<ITestQuestion> _questions;

		public string Name { get; set; }

		public ITestQuestion this[int index]
		{
			get => _questions[index];
		}

		public int QuestionCount { get => _questions.Count; }

		public Test() { }

		public Test(List<ITestQuestion> questions)
		{
            _questions = questions;
		}
    }
}
