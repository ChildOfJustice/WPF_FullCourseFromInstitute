using System.Windows.Controls;
using System.Windows.Navigation;
using Task_3.ViewModels;

namespace Task_3.WPF_Control_Elements.NavigationManager
{
    public class NavigationManager
    {
        private NavigationService _navigationService;

        public NavigationManager(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool Navigate<TView>(BaseViewModel viewModel) where TView : Page, new()
        {
            Page page = new TView();
            page.DataContext = viewModel;
            _navigationService.Navigate(page);
            return true;
        }

        public bool NavigateWithNavigationManager<TView>(BaseViewModelForNavigationManager viewModel) where TView : Page, new()
        {
            Page page = new TView();
            page.DataContext = viewModel;
            _navigationService.Navigate(page);
            return true;
        }

        public bool Navigate<TView, TViewModel>() where TView : Page, new() where TViewModel : BaseViewModel, new()
        {
            Page page = new TView();
            BaseViewModel viewModel = new TViewModel();
            page.DataContext = viewModel;
            _navigationService.Navigate(page);
            return true;
        }

        public bool NavigateWithNavigationManager<TView, TViewModel>() where TView : Page, new() where TViewModel : BaseViewModelForNavigationManager, new()
        {
            Page page = new TView();
            BaseViewModelForNavigationManager viewModel = new TViewModel();
            viewModel.NavigationManager = this;
            page.DataContext = viewModel;
            _navigationService.Navigate(page);
            return true;
        }

        public bool GoBack()
        {
            if (!CanGoBack) return false;
            _navigationService.GoBack();
            return true;
        }

        public bool CanGoBack
        {
            get =>
                _navigationService.CanGoBack;

            private set
            {     }
        }
    }
}