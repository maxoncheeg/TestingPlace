namespace TestingPlace.Model.Testing.Answers
{
    public interface IQuestionAnswer
    {
        public string Text { get; }
        public double Points { get; set; }
        public double Check(IQuestionAnswer answer);
    }
}
