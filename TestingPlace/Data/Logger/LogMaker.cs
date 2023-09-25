namespace TestingPlace.Data.Logger
{
    internal abstract class LogMaker : ILogMaker
    {
        private readonly Logger _logger;

        public LogMaker(Logger logger) => _logger = logger;

        public bool Log(string message) => _logger.Write(message);
    }
}
