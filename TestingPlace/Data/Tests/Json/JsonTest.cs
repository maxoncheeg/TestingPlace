using System.Collections.Generic;
using TestingPlace.Model.Testing;

namespace TestingPlace.Data.Tests.Json
{
    internal class JsonTest
    {
        public TestEntity Test { get; }
        public List<JsonQuestion> Questions { get; }

        public JsonTest(TestEntity test, List<JsonQuestion> questions)
        {
            Test = test;
            Questions = questions;
        }
    }
}
