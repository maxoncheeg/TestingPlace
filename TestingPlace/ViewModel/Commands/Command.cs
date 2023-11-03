﻿using System;
using System.Windows.Input;

namespace TestingPlace.ViewModel.Commands
{
    class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Command(EventHandler method) => CanExecuteChanged += method;

        public static Command Create(EventHandler method) => new(method);

        public bool CanExecute(object? parameter) => CanExecuteChanged is not null;

        public void Execute(object? parameter) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
