using System;

namespace TestingPlace.Model.Testing.Answers
{
    interface IAnswer
    {
        public string Text { get; }
        public string? ImagePath { get; set; }
        public double Points { get; set; }
    }
}
