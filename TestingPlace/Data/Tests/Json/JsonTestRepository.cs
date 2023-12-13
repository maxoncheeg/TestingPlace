using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestingPlace.Model.Testing;

namespace TestingPlace.Data.Tests.Json
{
    internal class JsonTestRepository : TestRepository
    {
        private readonly string _savePath;

        public JsonTestRepository(string savePath)
        {
            _savePath = savePath;
            if (string.IsNullOrEmpty(_savePath))
                throw new System.ArgumentException(_savePath);
        }

        public bool Load()
        {
            using var stream = File.Open(_savePath, FileMode.OpenOrCreate);
            using var reader = new StreamReader(stream);

            string jsonText = reader.ReadToEnd();
            var tests = JsonConvert.DeserializeObject<List<Test>>(jsonText, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            if (tests == null) return false;
            _tests = tests;
            return true;
        }

        public async override Task<bool> LoadAsync()
        {
            return await Task.Run(Load);
        }


        public bool Save()
        {
            using var stream = File.Open(_savePath, FileMode.Create);
            using var writer = new StreamWriter(stream);

            string textedTests = JsonConvert.SerializeObject(_tests, Formatting.Indented, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            writer.Write(textedTests);

            return true;
        }

        public async override Task<bool> SaveAsync()
        {
            return await Task.Run(Save);
        }
    }
}
