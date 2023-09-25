using System.Collections.Generic;
using System.Threading.Tasks;
using TestingPlace.Model.Testing;

namespace TestingPlace.Data.Tests
{
    internal interface ITestRepository
    {
        public List<Test> Tests { get; set; }

        public bool Save();
        public bool Load();

        public Task<bool> SaveAsync();
        public Task<bool> LoadAsync();
    }
}
