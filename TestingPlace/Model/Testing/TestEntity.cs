using System;
using TestingPlace.Model;

namespace TestingPlace.Model.Testing
{
    public enum TestTheme
    {
        Любая,
        Математика,
        Неведомое,
        Смешной,
        Физика,
        Программирование,
        Кулинария
    }

    public class TestEntity : IEntity
    {
        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public TestTheme Theme { get; }
        public Guid AuthorId { get; }

        public TestEntity(Guid id, string name, TestTheme theme, Guid authorId)
        {
            Id = id;
            Name = name;
            Theme = theme;
            AuthorId = authorId;
        }


    }
}
