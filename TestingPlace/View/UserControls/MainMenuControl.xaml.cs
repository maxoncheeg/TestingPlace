using System.Windows.Controls;
using TestingPlace.ViewModel.Managers;
using TestingPlace.ViewModel.UserControls;

namespace TestingPlace.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для MainMenuControl.xaml
    /// </summary>
    public partial class MainMenuControl : UserControl
    {
        public MainMenuControl(IDataManager manager)
        {
            InitializeComponent();

            DataContext = new MainMenuViewModel(manager);
        }
    }
}
