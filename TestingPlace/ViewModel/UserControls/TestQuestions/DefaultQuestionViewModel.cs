using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestingPlace.Data;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;
using TestingPlace.Model.Testing.TestSessions;

namespace TestingPlace.ViewModel.UserControls.TestQuestions
{
    internal class DefaultQuestionViewModel : BaseViewModel
    {
        private int _selectedIndex = -1;
        private ITestSession? _testSession;
        private ObservableCollection<QuestionAnswer> _answers = new();
        private string _title = string.Empty;

        public ObservableCollection<QuestionAnswer> Answers
        {
            get => _answers;
            set
            {
                _answers = value;
                Notify();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Notify();
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;

                if (SelectedIndex != -1 && _testSession != null)            
                    _testSession.Answer(Answers[SelectedIndex]);
                
                Notify();
            }
        }

        public DefaultQuestionViewModel()
        {
            if (DataManager.Instance() is DataManager manager && manager.TestSession != null)
            {
                _testSession = manager.TestSession;
                _testSession.QuestionChanged += OnQuestionChanged;
                OnQuestionChanged(this, new(_testSession.Test[_testSession.CurrentQuestionIndex]));
            }
        }

        private void OnQuestionChanged(object? sender, QuestionEventArgs e)
        {
            if (e.Question is DefaultQuestion question)
            {
                SelectedIndex = -1;
                Answers = new(question.Answers);
                Title = question.Text;
            }
        }
    }
}
