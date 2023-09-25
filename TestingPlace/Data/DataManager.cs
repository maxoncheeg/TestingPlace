using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlace.Data.Tests;

namespace TestingPlace.Data
{
    internal class DataManager
    {
        private static DataManager _manager = null;

        private readonly ITestRepository _testRepository;

        private DataManager(ITestRepository testRepository)
        {
            _testRepository = testRepository;
            _manager = this;
        }

        public static DataManager Instance(ITestRepository testRepository) => new(testRepository);

        public static DataManager Instance() => _manager;

        public bool SaveAll()
        {
            bool result = true;

            if (!_testRepository.Save()) result = false;

            return result;
        }

        public bool LoadAll()
        {
            bool result = true;

            if (!_testRepository.Load()) result = false;

            return result;
        }

        public async Task<bool> SaveAllAsync()//доделать
        {
            bool result = true;

            if (!await _testRepository.SaveAsync()) result = false;

            return result;
        }

        public async Task<bool> LoadAllAsync()//доделать
        {
            bool result = true;

            if (!await _testRepository.LoadAsync()) result = false;

            return result;
        }
    }
}
