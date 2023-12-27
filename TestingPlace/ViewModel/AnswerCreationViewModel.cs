using System;
using System.Collections.ObjectModel;
using System.Linq;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.ViewModel.Commands;
using TestingPlace.ViewModel.TestSessions;

namespace TestingPlace.ViewModel
{
    internal class AnswerCreationViewModel : BaseViewModel
    {
        private ITestCreationSession _session;

        private int _answerIndex;

        private string _answerText = string.Empty;
        private string _imagePath = string.Empty;
        private string _points = "0";

        private ObservableCollection<IQuestionAnswer> _answers = new();

        public event Func<string>? GettingFilePath;
        public event Action<string, string>? MessageReceiving;

        #region Bindings
        public int AnswerIndex
        {
            get => _answerIndex;
            set
            {
                _answerIndex = value;
                
                Notify();
                UpdateInfo();
            }
        }

        public string AnswerText
        {
            get => _answerText;
            set
            {
                _answerText = value;
                Notify();
            }
        }

        public string Points
        {
            get => _points;
            set
            {
                _points = value;
                Notify();
            }
        }

        public string AnswerPath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                Notify();
            }
        }

        public ObservableCollection<IQuestionAnswer> Answers
        {
            get => _answers;
            set
            {
                _answers = value;
                Notify();
            }
        }
        #endregion

        #region Commands
        public Command OpenImage => Command.Create(OpenImageMethod);
        private void OpenImageMethod(object? sender, EventArgs args)
        {
            AnswerPath = GettingFilePath?.Invoke() ?? string.Empty;
        }

        public Command SaveAnswer => Command.Create(SaveQuestionMethod);
        private void SaveQuestionMethod(object? sender, EventArgs args)
        {
            if (double.TryParse(Points, out var points))
            {
                if (_session.CurrentQuestionAnswers.Count(x => x.Points > 0) > 0)
                {
                    MessageReceiving?.Invoke("Внимание", "В вопросе уже есть правильный ответ, у которого баллы отличны от 0!");
                    return;
                }

                AbstractAnswerEntity answer;
                if (_answerIndex == -1)
                {
                    answer = new QuestionAnswer(Guid.NewGuid(), _session.CurrentQuestionId, AnswerText, points) { ImagePath = AnswerPath };
                    _session.CurrentQuestionAnswers.Add(answer);
                }
                else
                {
                    answer = new QuestionAnswer(Guid.NewGuid(), _session.CurrentQuestionId, AnswerText, points) { ImagePath = AnswerPath };
                    _session.CurrentQuestionAnswers[_answerIndex] = answer;
                }
                Answers = new(_session.CurrentQuestionAnswers);
                AnswerIndex = _session.CurrentQuestionAnswers.IndexOf(answer);
                UpdateInfo();
                NewAnswerMethod(this, EventArgs.Empty);
            }
            else MessageReceiving?.Invoke("ОШИБКА", "Произошла неизвестная ошибка (на самом деле известная)");
        }

        public Command RemoveAnswer => Command.Create(RemoveAnswerMethod);
        private void RemoveAnswerMethod(object? sender, EventArgs args)
        {
            if(AnswerIndex < _session.CurrentQuestionAnswers.Count)
            {
                _session.CurrentQuestionAnswers.RemoveAt(AnswerIndex);
                Answers = new(_session.CurrentQuestionAnswers);

                AnswerIndex = 0;
                UpdateInfo();
            }
        }

        public Command NewAnswer => Command.Create(NewAnswerMethod);
        private void NewAnswerMethod(object? sender, EventArgs args)
        {
            AnswerText = string.Empty;
            Points = "0";
            AnswerIndex = -1;
        }

        #endregion
        public AnswerCreationViewModel(ITestCreationSession session) 
        { 
            _session = session;
            Answers = new(_session.CurrentQuestionAnswers);
            UpdateInfo();
            NewAnswerMethod(this, EventArgs.Empty);
        }

        private void UpdateInfo()
        {
            if (AnswerIndex >= 0 && Answers.Count > 0)
            {
                AnswerText = Answers[AnswerIndex].Text;
                Points = Answers[AnswerIndex].Points.ToString();
            }
        }
    }
}
