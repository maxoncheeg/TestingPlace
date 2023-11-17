namespace TestingPlace.Utilities.Navigation
{
    public interface INavigationAware
    {
        void OnNavigatingTo(object arg);
        void OnNavigatingFrom();
    }
}
