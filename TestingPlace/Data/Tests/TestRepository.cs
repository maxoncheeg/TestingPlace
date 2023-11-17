using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestingPlace.Model.Testing;

namespace TestingPlace.Data.Tests
{
    public abstract class TestRepository : IBaseRepository<Test>
    {
        protected List<Test> _tests = new();

        public bool Add(Test entity)
        {
            if (entity == null || _tests.Find(x => x.Id == entity.Id) != null) return false;

            _tests.Add(entity);
            return true;
        }

        public bool Delete(Test entity)
        {
            if(_tests.Contains(entity))
                _tests.Remove(entity);

            return false;
        }

        public Test? Get(Guid id)
        {
            return _tests.FirstOrDefault(test => test.Id == id);
        }

        public IReadOnlyCollection<Test> GetAll()
        {
            return _tests;
        }

        public IReadOnlyCollection<Test> GetBy(Predicate<Test> predicate)
        {
            List<Test> tests = new();

            foreach (var item in _tests)
                if(predicate(item))tests.Add(item);

            return tests;
        }
        public bool Update(Test entity)
        {
            for (int i = 0; i < _tests.Count; i++)
                if (_tests[i] == entity)
                {
                    _tests[i] = entity;
                    return true;
                }

            return false;
        }

        public abstract Task<bool> SaveAsync();

        public abstract Task<bool> LoadAsync();
    }
}
