using System;
using System.Configuration;
using System.IO;

namespace TestingPlace.Data.Logger
{
    internal class TxtLogWriter : ILogWriter
    {
        private readonly string _path;

        public TxtLogWriter(string path = null) =>  
            _path = path ?? ConfigurationManager.AppSettings["txtLogPath"];
        
        public bool Write(string message)
        {
            try
            {
                using var stream = File.Open(_path, FileMode.Append);
                using var writer = new StreamWriter(stream);

                message = $" DATE- {DateTime.Now.ToShortDateString()} |" +
                    $" TIME- {DateTime.Now.ToLongTimeString()} |" +
                    $" MESSAGE- {message}";

                writer.WriteLine(message);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
