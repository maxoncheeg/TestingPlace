using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestingPlace.Data.Tests;
using TestingPlace.Data.Tests.Json;
using TestingPlace.Data.Users;
using TestingPlace.Data.Users.Json;
using TestingPlace.Model.Testing;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;
using TestingPlace.View;
using TestingPlace.View.UserControls;
using TestingPlace.View.UserControls.TestCreation;
using TestingPlace.ViewModel;
using TestingPlace.ViewModel.Services;
using TestingPlace.ViewModel.Services.Navigation;
using TestingPlace.ViewModel.TestSessions;
using TestingPlace.ViewModel.UserControls;
using TestingPlace.ViewModel.UserControls.TestCreation;

namespace TestingPlace
{
    public enum TestingPlaceWindows
    {
        StartupWindow,
        RegistrationWindow,
        MainWindow,
        TestSolveWindow,
        TestCreationWindow,
        AnswerCreationWindow
    }

    public enum TestingPlaceUserControls
    {
        MainWindow_MainMenu,
        MainWindow_TestList,
        MainWindow_Rating,
        TestCreationWindow_QuestionCreation,
        TestCreationWindow_FinalCreation
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IDataManager _dataManager;
        private INavigationService _navigationService;
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

            Current.MainWindow = new StartupWindow();

            _navigationService = new NavigationService(Current.MainWindow, new() { NavigationServiceSettings.HidePreviousWindow });
            Current.MainWindow.DataContext = new StartupViewModel(_navigationService, _dataManager);

            #region WindowsRegistration
            _navigationService.Register(
                   TestingPlaceWindows.RegistrationWindow,
                   new NavigationWindow(
                       typeof(RegistrationWindow),
                       new NavigationContent(typeof(RegistrationViewModel), new List<object>() { _navigationService, _dataManager }, null)
                       )
                   );

            _navigationService.Register(
                TestingPlaceWindows.TestSolveWindow,
                new NavigationWindow(
                    typeof(TestSolveWindow),
                    new NavigationContent(typeof(TestSolveViewModel), new List<object>() { _navigationService, _dataManager }, null)
                    )
                );

            _navigationService.Register(
                TestingPlaceWindows.AnswerCreationWindow,
                new NavigationWindow(
                    typeof(AnswerCreationWindow),
                    new NavigationContent(typeof(AnswerCreationViewModel))
                    )
                );

            _navigationService.Register(
                TestingPlaceWindows.TestCreationWindow,
                new NavigationWindow(
                    typeof(TestCreationWindow),
                    new NavigationContent(
                        typeof(TestCreationViewModel),
                        new List<object>() { _navigationService, _dataManager },
                        new List<INavigationContent>() { new NavigationContent(typeof(TestCreationSession)) }),
                    new Dictionary<Enum, INavigationWindow>()
                    {
                        { TestingPlaceUserControls.TestCreationWindow_QuestionCreation,
                          new NavigationWindow(typeof(QuestionCreationWindow), new NavigationContent(typeof(QuestionCreationViewModel))) },
                        { TestingPlaceUserControls.TestCreationWindow_FinalCreation,
                          new NavigationWindow(typeof(FinalCreationUserControl), new NavigationContent(typeof(FinalCreationViewModel))) }
                    }
                    )
               );

            _navigationService.Register(
                TestingPlaceWindows.MainWindow,
                new NavigationWindow(
                    typeof(MainWindow),
                    new NavigationContent(typeof(MainViewModel), new List<object>() { _navigationService, _dataManager }, null),
                    new Dictionary<Enum, INavigationWindow>()
                    {
                        { TestingPlaceUserControls.MainWindow_MainMenu,
                          new NavigationWindow(typeof(MainMenuControl), new NavigationContent(typeof(MainMenuViewModel))) },
                        { TestingPlaceUserControls.MainWindow_TestList,
                          new NavigationWindow(typeof(TestListControl), new NavigationContent(typeof(TestListViewModel))) }
                    }
                    )
                );
            #endregion

            Guid k = Guid.NewGuid();
            var h = _dataManager.UserRepository.Add(new Model.Users.UserEntity(k, "3", "4", "5", "6"));
            await _dataManager.SaveAllAsync();
            await _dataManager.LoadAllAsync();

            var y = _dataManager.UserRepository.GetBy(x => x.Id == k).First();
            _dataManager.UserRepository.Delete(y);
            await _dataManager.SaveAllAsync();

            Current.MainWindow.Show();
        }

        private async Task CreateBaseInfo(IDataManager manager)
        {
            var testId = Guid.NewGuid();
            Guid q1 = Guid.NewGuid(), q2 = Guid.NewGuid(), q3 = Guid.NewGuid();

            List<IQuestionAnswer> answers1 = new()
            {
                new QuestionAnswer(Guid.NewGuid(), q1, "ПИСАТЬ НА C SHARP"),
                new QuestionAnswer(Guid.NewGuid(), q1, "ПИСАТЬ НА Py SON"),
            };

            List<IQuestionAnswer> answers2 = new()
            {
                new QuestionAnswer(Guid.NewGuid(), q2, "1"),
                new QuestionAnswer(Guid.NewGuid(), q2, "2"),
                new QuestionAnswer(Guid.NewGuid(), q2, "3"),
                new QuestionAnswer(Guid.NewGuid(), q2, "4"),
                new QuestionAnswer(Guid.NewGuid(), q2, "5"),
                new QuestionAnswer(Guid.NewGuid(), q2, "6"),
                new QuestionAnswer(Guid.NewGuid(), q2, "8"),
                new QuestionAnswer(Guid.NewGuid(), q2, "9"),
            };

            List<IQuestionAnswer> answers3 = new()
            {
                new QuestionAnswer(Guid.NewGuid(), q3, "Sergio Speedster"),
                new QuestionAnswer(Guid.NewGuid(), q3, "Maxonella Jordan"),
                new QuestionAnswer(Guid.NewGuid(), q3, "Claudio Leviafan"),
            };

            List<ITestQuestion> testQuestions = new()
            {
                new DefaultQuestion(Guid.NewGuid(), testId, "ЧТО ВАЖНЕЕ?", new QuestionAnswer(Guid.NewGuid(), q1, "РЕШАТЬ ИНТЕГРАЛЫ", 10), answers1),
                new DefaultQuestion(Guid.NewGuid(), testId, "1, -37, 3, -35, 5, -33, ___. Какое число пропущено?", new QuestionAnswer(Guid.NewGuid(), q2, "7", 10), answers2),
                new DefaultQuestion(Guid.NewGuid(), testId, "Молодому ученому с фамилией Левиафанистер(потомок рода династии Левиафан) понадобилась помощь с программированием высокоскоростных вычислительных машин на языке программирования PAYSON. К кому лучше обратится молодому ученому, чтобы выполнить работу более качественно?", new QuestionAnswer(Guid.NewGuid(), q3, "Denix Payson", 10), answers3),
            };

            Test test = new Test(testId, "Проверка программиста. Часть 1", TestTheme.Программирование, Guid.NewGuid(), testQuestions, TimeSpan.Zero);

            manager.TestRepository.Add(test);
            await manager.TestRepository.SaveAsync();
        }
    }
}
