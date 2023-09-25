using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions
{
	public interface ITestQuestion
	{
		public float GetPoints();
		public float Answer(IAnswer answer);
	}
}
