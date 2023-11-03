




using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestingPlace.Data.Users.Json
{
    class JsonUserRepository : UserRepository
    {
        private string _savePath;

        public JsonUserRepository()
        {
            _savePath = ConfigurationManager.AppSettings["jsonUserPath"] ?? string.Empty;
            if (string.IsNullOrEmpty(_savePath))
                throw new System.Exception("Не задан путь сохранения тестов");
        }

        public bool Load()
        {
            using var stream = File.Open(_savePath, FileMode.Open);
            using var reader = new StreamReader(stream);

            string jsonText = reader.ReadToEnd();

            if(string.IsNullOrEmpty(jsonText))return false;

            try
            {
                List<UserEntity>? users = JsonSerializer.Deserialize<List<UserEntity>>(jsonText);

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

            string textedTests = JsonSerializer.Serialize(_users, new JsonSerializerOptions() { WriteIndented = true });
            writer.Write(textedTests);

            return true;
        }

        public override async Task<bool> SaveAsync()
        {
            return await Task.Run(Save);
        }

    }
}
