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
        public string Text => throw new NotImplementedException();

        public double Points { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
			QuestionAnswer answer = new QuestionAnswer(Guid.NewGuid(), Guid.NewGuid(), "test", 10);
			IQuestionAnswer iAnswer = new QuestionAnswer(Guid.NewGuid(), Guid.NewGuid(), "test", 10);

			Assert.IsTrue(answer.Equals(iAnswer));
		}

		[TestMethod]
		public void Check_False_Test()
		{
            QuestionAnswer answer = new QuestionAnswer(Guid.NewGuid(), Guid.NewGuid(), "test", 10);
            IQuestionAnswer iAnswer = new QuestionAnswer(Guid.NewGuid(), Guid.NewGuid(), "test1", 10);

            Assert.IsFalse(answer.Equals(iAnswer));
		}
	}
}
