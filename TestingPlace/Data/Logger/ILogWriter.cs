using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPlace.Data.Logger
{
    internal interface ILogWriter
    {
        public bool Write(string message);
    }
}
