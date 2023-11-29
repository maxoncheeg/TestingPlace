using System.Collections.ObjectModel;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;
using TestingPlace.Model.Testing.TestSessions;

namespace TestingPlace.ViewModel.UserControls.TestQuestions
{
    internal class DefaultQuestionViewModel : BaseViewModel
    {
        private int _selectedIndex = -1;
        private ITestSession _testSession;
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

        public DefaultQuestionViewModel(ITestSession session)
        {
            _testSession = session;

            _testSession.QuestionChanged += OnQuestionChanged;
            OnQuestionChanged(this, new(_testSession.Test[_testSession.CurrentQuestionIndex]));
        }

        private void OnQuestionChanged(object? sender, QuestionEventArgs e)
        {
            if (e.Question is DefaultQuestion question)
            {
                Answers = new(question.Answers);
                if (_testSession.Answers.ContainsKey(_testSession.CurrentQuestionIndex) && _testSession.Answers[_testSession.CurrentQuestionIndex] is QuestionAnswer answer)
                    SelectedIndex = Answers.IndexOf(answer);
                else SelectedIndex = -1;

                Title = question.Text;
            }
        }
    }
}
