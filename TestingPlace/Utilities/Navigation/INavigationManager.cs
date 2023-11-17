namespace TestingPlace.Utilities.Navigation
{
    internal interface INavigationManager
    {
        void Navigate(string navigationKey, object? arg = null);
    }
}
