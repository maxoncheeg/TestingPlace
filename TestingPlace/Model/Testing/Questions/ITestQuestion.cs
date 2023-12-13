using System.Collections.Generic;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions
{
	public interface ITestQuestion
	{
        public string Type { get; }
        public string Text { get; }
        public IReadOnlyCollection<IQuestionAnswer> Answers { get; }

        public double GetPoints();
		public double Answer(IQuestionAnswer answer);
	}
}
