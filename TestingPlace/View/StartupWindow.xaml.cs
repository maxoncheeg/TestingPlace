﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestingPlace.ViewModel;

namespace TestingPlace.View
{
    /// <summary>
    /// Логика взаимодействия для StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        public static RoutedCommand LoginSuccessCommand { get; } = new("OpenMainWindow", typeof(StartupWindow));

        public StartupWindow()
        {
            InitializeComponent();
            DataContext = new StartupViewModel();

            CommandBindings.Add(new(LoginSuccessCommand, OpenMainWindow, (s, e) => e.CanExecute = true));

            if(DataContext is StartupViewModel viewModel)
            {
                viewModel.LoginSuccess += () => LoginSuccessCommand.Execute(this, null);
                viewModel.LoginError += ShowErrorWindow;
                viewModel.RegistrationClicked += OnRegistrationClicked;
            }
        }

        private void OpenMainWindow(object? sender, EventArgs args)
        {
            MainWindow window = new(this);
            window.ShowDialog();
        }

        private void OnRegistrationClicked()
        {
            RegistrationWindow window = new(this);
            window.ShowDialog();
        }

        private void ShowErrorWindow(string message)
        {
            MessageBox.Show(message);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(DataContext is StartupViewModel viewModel && sender is PasswordBox box)
                viewModel.Password = box.Password;
        }
    }
}
