namespace TestingPlace.Data.Logger
{
    internal class Logger
    {
        private readonly ILogWriter _writer;

        public Logger(ILogWriter writer) =>     
            _writer = writer;
        
        public bool Write(string message) => _writer.Write(message);
    }
}
