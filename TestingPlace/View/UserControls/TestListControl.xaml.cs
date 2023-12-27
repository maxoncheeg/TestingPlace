using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestingPlace.ViewModel.UserControls;

namespace TestingPlace.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TestListControl.xaml
    /// </summary>
    public partial class TestListControl : UserControl
    {
        public TestListControl()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is TestListViewModel model)
                model.Search = searchBox.Text;
        }
    }
}
