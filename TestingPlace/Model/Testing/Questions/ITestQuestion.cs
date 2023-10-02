using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions
{
	public interface ITestQuestion
	{
		public double GetPoints();
		public double Answer(IAnswer[] answer);
	}
}
