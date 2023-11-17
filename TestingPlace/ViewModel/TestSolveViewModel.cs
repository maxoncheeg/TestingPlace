using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private ITestSession _testSession;
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
                //if (!_testSession.NextQuestion())
                //{
                //    //дальше некуда
                //}

                if(!_testSession.NextQuestion() || _testSession.Test.QuestionCount == _testSession.Answers.Count)
                {
                    double points = 0;
                    foreach (KeyValuePair<int, IQuestionAnswer> pair in _testSession.Answers)
                    {
                        points += _testSession.Test[pair.Key].Answer(pair.Value);
                    }

                    MessageBox.Show($"ВЫ ЗАРАБОТАЛИ {points} ИЗ {_testSession.Test.GetTotalPoints()}");
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

        public TestSolveViewModel(IDataManager manager, ITestSession session, UserControl defaultQuestionControl)
        {
            _testSession = session;
            _defaultQuestionControl = defaultQuestionControl;

            if (manager.TestSession != null)
            {
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
