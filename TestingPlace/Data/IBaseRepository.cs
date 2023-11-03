using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestingPlace.Data
{
    interface IBaseRepository<T> where T : IEntity
    {
        public bool Add(T entity);
        public bool Update(T entity);
        public bool Delete(T entity);
        public T? Get(Guid id);
        public IReadOnlyCollection<T> GetBy(Predicate<T> predicate);
        public IReadOnlyCollection<T> GetAll();

        public Task<bool> SaveAsync();
        public Task<bool> LoadAsync();
    }
}
