using System.Configuration;
using System.Windows;
using TestingPlace.Data.Tests;
using TestingPlace.Data.Tests.Json;
using TestingPlace.Data.Users;
using TestingPlace.Data.Users.Json;
using TestingPlace.View;
using TestingPlace.ViewModel.Managers;

namespace TestingPlace
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IDataManager _dataManager;
        private TestRepository _testRepository;
        private UserRepository _usersRepository;

        public App() : base()
        {
            string testsSavePath = ConfigurationManager.AppSettings["jsonTestPath"] ?? string.Empty;
            string usersSavePath = ConfigurationManager.AppSettings["jsonUserPath"] ?? string.Empty;

            _testRepository = new JsonTestRepository(testsSavePath);
            _usersRepository = new JsonUserRepository(usersSavePath);

            _dataManager = DataManager.Instance(_testRepository, _usersRepository);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await _dataManager.LoadAllAsync();

            StartupWindow window = new(_dataManager);
            window.Show();
        }
    }
}
