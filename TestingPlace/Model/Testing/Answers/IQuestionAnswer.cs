namespace TestingPlace.Model.Testing.Answers
{
    public interface IQuestionAnswer
    {
        public float Points { get; }
        public float Check(IAnswer answer);
    }
}
