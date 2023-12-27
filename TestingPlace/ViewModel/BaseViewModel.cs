using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestingPlace.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void Notify([CallerMemberName] string propertyName = "") 
            => PropertyChanged?.Invoke(this, new(propertyName));
    }
}
