using System;
using System.Collections.Generic;

namespace TestingPlace.ViewModel.Services.Navigation
{
    public interface INavigationWindow
    {
        public Type WindowType { get; }
        public INavigationContent Content { get; }
        public IDictionary<Enum, INavigationWindow>? UserControls { get; }

        public object Instance();
        public object? InstanceUserControl(Enum key, IList<object>? parameters);
    }
}
