using CommunityToolkit.Mvvm.Input;

namespace SistemaTaxiMobil.ViewModels
{
    public partial class SplashViewModel : BaseViewModel
    {
        public SplashViewModel()
        {
            Title = "Bienvenido";
        }

        [RelayCommand]
        async Task Ingresar()
        {
            await Shell.Current.GoToAsync("///login");
        }
    }
}