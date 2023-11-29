using System.Collections.Generic;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;

namespace TestingPlace.Data.Tests.Json
{
    public class JsonQuestion
    {
        public QuestionEntity Question { get; }

        public List<AnswerEntity> Answers { get; } = new();

        public JsonQuestion(QuestionEntity question, List<AnswerEntity> answers)
        {
            Question = question;
            Answers = answers;
        }
    }
}
