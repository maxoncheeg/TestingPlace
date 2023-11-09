using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestingPlace.Data;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;
using TestingPlace.Model.Testing.TestSessions;
using TestingPlace.View.UserControls.TestQuestions;
using TestingPlace.ViewModel.Commands;

namespace TestingPlace.ViewModel
{
    internal class TestSolveViewModel : BaseViewModel
    {
        private ITestSession? _testSession = null;
        private UserControl _defaultQuestionControl;

        #region Bindings
        public UserControl ActualControl
        {
            get => _defaultQuestionControl;
            set
            {
                _defaultQuestionControl = value;
                Notify();
            }
        }
        #endregion

        #region Commands
        public Command GetNextQuestion => Command.Create(GetNextQuestionMethod);
        public void GetNextQuestionMethod(object? sender, EventArgs args)
        {
            if(_testSession != null)
            {
                if (!_testSession.NextQuestion())
                {
                    //дальше некуда
                }

                if(_testSession.Test.QuestionCount == _testSession.Answers.Count)
                {
                    foreach (KeyValuePair<int, IQuestionAnswer> pair in _testSession.Answers)
                    {
                        _testSession.Test[pair.Key].Answer(new QuestionAnswer);
                    }
                }

                switch (_testSession.Test[_testSession.CurrentQuestionIndex])
                {
                    case DefaultQuestion:
                        ActualControl = _defaultQuestionControl;
                        break;
                }
            }
        }
        #endregion

        public TestSolveViewModel(UserControl defaultQuestionControl)
        {
            _defaultQuestionControl = defaultQuestionControl;

            if (DataManager.Instance() is DataManager manager && manager.TestSession != null)
            {
                _testSession = manager.TestSession;

                switch (_testSession.Test[_testSession.CurrentQuestionIndex])
                {
                    case DefaultQuestion:
                        ActualControl = _defaultQuestionControl;
                        break;
                }
            }
        }
    }
}
