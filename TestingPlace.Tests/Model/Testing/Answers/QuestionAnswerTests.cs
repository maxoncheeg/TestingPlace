using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Tests.Model.Testing.Answers
{
	class TestQuestionAnswer : IQuestionAnswer
	{
		public double Check(IQuestionAnswer answer)
		{
			throw new NotImplementedException();
		}
	}
	[TestClass]
	public class QuestionAnswerTests
	{
		[TestMethod]
		public void Check_True_Test()
		{
			QuestionAnswer answer = QuestionAnswer.Create("test", 10);
			IQuestionAnswer iAnswer = QuestionAnswer.Create("test", 10);

			Assert.IsTrue(answer.Equals(iAnswer));
		}

		[TestMethod]
		public void Check_False_Test()
		{
			QuestionAnswer answer = QuestionAnswer.Create("test", 10);
			IQuestionAnswer iAnswer = QuestionAnswer.Create("test", 10);

			Assert.IsFalse(answer.Equals(iAnswer));
		}
	}
}
