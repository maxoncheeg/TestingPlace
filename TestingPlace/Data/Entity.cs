using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPlace.Data
{
    public abstract class Entity
    {
        private readonly Guid _id;

        public Guid Id { get => _id; }

        public Entity() => _id = Guid.NewGuid();

        public Entity(Guid id) => _id = id;
    }
}
