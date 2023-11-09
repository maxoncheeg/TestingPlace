﻿using System;
using TestingPlace.Model.Testing.Questions;

namespace TestingPlace.Model.Testing.TestSessions
{
    internal class QuestionEventArgs(ITestQuestion question) : EventArgs
    {
        public ITestQuestion Question { get; private set; } = question;
    }
}
