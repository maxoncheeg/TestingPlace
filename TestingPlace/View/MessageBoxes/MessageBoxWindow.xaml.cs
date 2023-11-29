using System.Windows;

namespace TestingPlace.View.MessageBoxes
{
    /// <summary>
    /// Логика взаимодействия для MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        private MessageBoxWindow(string title, string message)
        {
            InitializeComponent();
            labelTitle.Content = title;
            textBlockMessage.Text = message;
        }

        public static void ShowMessage(string title, string message)
        {
            MessageBoxWindow window = new(title, message);
            window.ShowDialog();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
