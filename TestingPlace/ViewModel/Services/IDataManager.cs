using System.Threading.Tasks;
using TestingPlace.Data.Tests;
using TestingPlace.Data.Users;
using TestingPlace.Model.Users;

namespace TestingPlace.ViewModel.Services
{
    public interface IDataManager
    {
        public TestRepository TestRepository { get; }
        public UserRepository UserRepository { get; }
        public UserEntity? CurrentUser { get; }

        public Task SaveAllAsync();
        public Task LoadAllAsync();

        public bool TryAuthorizeUser(string login, string password);
        public bool TryRegisterUser(string login, string password, string name, string email, bool isTeacher);
    }
}
