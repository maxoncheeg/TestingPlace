using System.Windows;
using TestingPlace.Data;
using TestingPlace.Data.Tests.Json;
using TestingPlace.Data.Users.Json;

namespace TestingPlace
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DataManager dataManager = 
                DataManager.Instance(new JsonTestRepository(), new JsonUserRepository());
            dataManager.LoadAllAsync().Wait();
        }
    }
}
