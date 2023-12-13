using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing.Questions;

namespace TestingPlace.Model.Testing
{
    public abstract class AbstractTestEntity : IEntity, ITest
    {
        [JsonProperty] public Guid Id { get; }
        [JsonProperty] public string Name { get; } = string.Empty;
        [JsonProperty] public TestTheme Theme { get; }
        [JsonProperty] public Guid AuthorId { get; }

        public abstract IReadOnlyCollection<ITestQuestion> Questions { get; }

        public TimeSpan Time { get; }

        public AbstractTestEntity(Guid id, string name, TestTheme theme, Guid authorId, TimeSpan time)
        {
            Id = id;
            Name = name;
            Theme = theme;
            AuthorId = authorId;
            Time = time;
        }

        public abstract double GetTotalPoints();
    }
}
