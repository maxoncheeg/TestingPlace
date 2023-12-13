using System;
using System.Collections.ObjectModel;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;
using TestingPlace.ViewModel.Commands;
using TestingPlace.ViewModel.TestCreationSessions;

namespace TestingPlace.ViewModel.UserControls.TestCreation
{
    internal class QuestionCreationViewModel : BaseViewModel
    {
        //private int _questionIndex;
        //private int _answerIndex;

        //private QuestionAnswer _currentAnswer;

        //private string _questionTitle = string.Empty;
        //private string _answerText = string.Empty;
        //private string _imagePath = string.Empty;
        //private string _points = string.Empty;

        //private ObservableCollection<QuestionAnswer> _answers = new();
        //private ObservableCollection<Question> _questions = new();

        //public event Func<string>? GettingFilePath;

        //#region Bindings
        //public int QuestionIndex
        //{
        //    get => _questionIndex;
        //    set
        //    {
        //        _questionIndex = value;
        //        Notify();
        //    }
        //}

        //public int AnswerIndex
        //{
        //    get => _answerIndex;
        //    set
        //    {
        //        _answerIndex = value;
        //        Notify();
        //    }
        //}

        //public string QuestionTitle
        //{
        //    get => _questionTitle;
        //    set
        //    {
        //        _questionTitle = value;
        //        Notify();
        //    }
        //}

        //public string AnswerText
        //{
        //    get => _answerText;
        //    set
        //    {
        //        _answerText = value;
        //        Notify();
        //    }
        //}

        //public string ImagePath
        //{
        //    get => _imagePath;
        //    set
        //    {
        //        _imagePath = value;
        //        Notify();
        //    }
        //}

        //public string Points
        //{
        //    get => _points;
        //    set
        //    {
        //        _points = value;
        //        Notify();
        //    }
        //}

        //public ObservableCollection<QuestionAnswer> Answers
        //{
        //    get => _answers;
        //    set
        //    {
        //        _answers = value;
        //        Notify();
        //    }
        //}

        //public ObservableCollection<Question> Questions
        //{
        //    get => _questions;
        //    set
        //    {
        //        _questions = value;
        //        Notify();
        //    }
        //}
        //#endregion

        //#region Commands
        //public Command OpenImage => Command.Create(OpenImageMethod);
        //private void OpenImageMethod(object? sender, EventArgs args)
        //{
        //    ImagePath = GettingFilePath?.Invoke() ?? " ";
        //}

        //public Command SaveQuestion => Command.Create(SaveQuestionMethod);
        //private void SaveQuestionMethod(object? sender, EventArgs args)
        //{

        //}

        //public Command DeleteQuestion => Command.Create(DeleteQuestionMethod);
        //private void DeleteQuestionMethod(object? sender, EventArgs args)
        //{

        //}

        //public Command NewQuestion => Command.Create(NewQuestionMethod);
        //private void NewQuestionMethod(object? sender, EventArgs args)
        //{

        //}

        //public Command NewAnswer => Command.Create(NewAnswerMethod);
        //private void NewAnswerMethod(object? sender, EventArgs args)
        //{
        //    QuestionAnswer answer = QuestionAnswer.Create(_answerText, int.Parse(_points));

        //}
        //#endregion

        //public QuestionCreationViewModel(ITestCreationSession session)
        //{

        //}
    }
}
