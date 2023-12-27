namespace TestingPlace.Model.Testing.Answers
{
    public interface IAnswer
    {
        public string Text { get; }
        public bool Check();
    }
}
