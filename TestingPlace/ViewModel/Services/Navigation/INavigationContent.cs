using System;
using System.Collections.Generic;

namespace TestingPlace.ViewModel.Services.Navigation
{
    public interface INavigationContent
    {
        public Type ClassType { get; }
        public IList<object>? Parameters { get; }
        public IList<INavigationContent>? NotInstancedParameters { get; }

        public object Instance();
        public void AddParameter(object parameter);
    }
}
