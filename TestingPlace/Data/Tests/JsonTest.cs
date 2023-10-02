using System.Collections.Generic;

namespace TestingPlace.Data.Tests
{
    internal class JsonTest
    {
        public TestEntity Test { get; set; }
        public List<QuestionEntity> Questions { get; set; } = new();

        public List<AnswerEntity> Answers { get; set; } = new();
    }
}
