using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using TestingPlace.Data.Logger;
using TestingPlace.Model.Testing;

namespace TestingPlace.Data.Tests
{
    internal class SerializableTestRepository : LogMaker, ITestRepository
    {
        private string? _path;
        public List<Test> Tests { get; set; }

        public SerializableTestRepository(string path = null) : base(new(new TxtLogWriter()))
        {
            _path = path ?? ConfigurationManager.AppSettings["serializePath"];
            Tests = new List<Test>();
        }

        public bool Load()
        {
            try
            {
                using var stream = File.OpenRead(_path);
                BinaryFormatter formatter = new();
                //Tests = formatter.Deserialize(stream) as List<Test>;
            }
            catch
            {
                Log($"Не удалось загрузить тесты из {_path}");
                return false;
            }

            Log($"Загружены тесты: {Tests.Count}");
            return true;
        }

        public bool Save()
        {
            try
            {
                using var stream = File.Open(_path, FileMode.Create);
                BinaryFormatter formatter = new();
                //formatter.Serialize(stream, Tests);
            }
            catch
            {
                Log($"Не удалось сохранить тесты: {Tests.Count}. Путь {_path}");
                return false;
            }

            Log($"Сохранены тесты: {Tests.Count}");
            return true;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                using var stream = File.Open(_path, FileMode.Create);
                BinaryFormatter formatter = new();
                //await Task.Run(() => formatter.Serialize(stream, Tests));
            }
            catch
            {
                Log($"Не удалось сохранить тесты: {Tests.Count}. Путь {_path}");
                return false;
            }

            Log($"Сохранены тесты: {Tests.Count}");
            return true;
        }

        public async Task<bool> LoadAsync()
        {
            try
            {
                using var stream = File.OpenRead(_path);
                BinaryFormatter formatter = new();
                //await Task.Run(() => Tests = formatter.Deserialize(stream) as List<Test>);
            }
            catch
            {
                Log($"Не удалось загрузить тесты из {_path}");
                return false;
            }

            Log($"Загружены тесты: {Tests.Count}");
            return true;
        }
    }
}
