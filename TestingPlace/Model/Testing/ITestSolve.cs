namespace TestingPlace.Model.Testing
{
    public interface ITestSolve
    {
        public int Attempts { get; set; }
        public double BestPoints { get; set; }
        public ITest Test { get; }
    }
}
