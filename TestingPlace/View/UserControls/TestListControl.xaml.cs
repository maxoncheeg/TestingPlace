using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestingPlace.Data;
using TestingPlace.Model.Testing;
using TestingPlace.ViewModel.UserControls;

namespace TestingPlace.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TestListControl.xaml
    /// </summary>
    public partial class TestListControl : UserControl
    {
        public TestListControl(IDataManager manager)
        {
            InitializeComponent();

            DataContext = new TestListViewModel(manager);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is TestListViewModel model)
                model.Search = searchBox.Text;
        }
    }
}
