using CebuContactTracing.ViewModels.Base;
using System.Threading.Tasks;

namespace CebuContactTracing.Services.Navigation
{
    public interface INavigationService
    {
		ViewModelBase PreviousPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task NavigateToPopUpAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToPopUpAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task RemoveLastFromBackStackAsync(bool popup=false);

        Task RemoveBackStackAsync(bool popup=false);
    }
}