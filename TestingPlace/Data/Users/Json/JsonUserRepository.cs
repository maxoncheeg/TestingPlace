using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestingPlace.Model.Users;

namespace TestingPlace.Data.Users.Json
{
    class JsonUserRepository : UserRepository
    {
        private string _savePath;

        public JsonUserRepository(string savePath)
        {
            _savePath = savePath;
            if (string.IsNullOrEmpty(_savePath))
                throw new System.Exception("Не задан путь сохранения тестов");
        }

        public bool Load()
        {
            using var stream = File.Open(_savePath, FileMode.OpenOrCreate);
            using var reader = new StreamReader(stream);

            string jsonText = reader.ReadToEnd();

            if(string.IsNullOrEmpty(jsonText))return false;

            try
            {
                List<UserEntity>? users = JsonConvert.DeserializeObject<List<UserEntity>>(jsonText, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                if (users is null) return false;

                _users = users;
            }
            catch { return false; }

            return true;
        }

        public override async Task<bool> LoadAsync()
        {
            return await Task.Run(Load);
        }

        public bool Save()
        {
            if (_users.Count <= 0) return false;

            using var stream = File.Open(_savePath, FileMode.Create);
            using var writer = new StreamWriter(stream);

            string textedTests = JsonConvert.SerializeObject(_users, Formatting.Indented, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            writer.Write(textedTests);

            return true;
        }

        public override async Task<bool> SaveAsync()
        {
            return await Task.Run(Save);
        }

    }
}
