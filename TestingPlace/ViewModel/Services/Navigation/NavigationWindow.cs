using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace TestingPlace.ViewModel.Services.Navigation
{
    public class NavigationWindow : INavigationWindow
    {
        public Type WindowType { get; }
        public INavigationContent Content { get; }
        public IDictionary<Enum, INavigationWindow>? UserControls { get; }

        public NavigationWindow(Type windowType, INavigationContent content, IDictionary<Enum, INavigationWindow>? userControls = null)
        {
            WindowType = windowType;
            Content = content;
            UserControls = userControls;
        }

        public object Instance()
        {
            Window window = Activator.CreateInstance(WindowType) as Window
                        ?? throw new ApplicationException(this.ToString());
            window.DataContext = Content.Instance();

            return window;
        }

        public object? InstanceUserControl(Enum key, IList<object>? parameters)
        {
            if (UserControls == null) return null;

            foreach (var control in UserControls)           
                if (control.Key.Equals(key))
                {
                    INavigationWindow navigationWindow = control.Value;
                    UserControl userControl = Activator.CreateInstance(navigationWindow.WindowType) as UserControl
                        ?? throw new ApplicationException(navigationWindow.ToString());

                    if(parameters != null)
                        foreach (var param in parameters)
                            navigationWindow.Content.AddParameter(param);

                    userControl.DataContext = navigationWindow.Content.Instance();

                    return userControl;
                }
            
            return null;
        }
    }
}
