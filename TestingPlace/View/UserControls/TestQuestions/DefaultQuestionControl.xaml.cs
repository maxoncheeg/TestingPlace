﻿using System.Windows.Controls;
using TestingPlace.Model.Testing.TestSessions;
using TestingPlace.ViewModel.UserControls.TestQuestions;

namespace TestingPlace.View.UserControls.TestQuestions
{
    /// <summary>
    /// Логика взаимодействия для DefaultQuestionControl.xaml
    /// </summary>
    public partial class DefaultQuestionControl : UserControl
    {
        public DefaultQuestionControl(ITestSession session)
        {
            InitializeComponent();

            DataContext = new DefaultQuestionViewModel(session);
        }
    }
}
