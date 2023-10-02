using System;

namespace TestingPlace.Data.Tests
{
    public class TestEntity : IEntity
    {
        public Guid Id { get; }
        public string Name { get; } = string.Empty;

        public TestEntity(Guid id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
