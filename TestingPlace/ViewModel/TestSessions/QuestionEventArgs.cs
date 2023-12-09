using System;
using TestingPlace.Model.Testing.Questions;

namespace TestingPlace.ViewModel.TestSessions
{
    public class QuestionEventArgs(ITestQuestion question) : EventArgs
    {
        public ITestQuestion Question { get; private set; } = question;
    }
}
