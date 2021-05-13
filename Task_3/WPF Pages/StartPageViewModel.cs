using System.Windows.Controls;
using Task_3.ViewModels;
using Task_3.WPF_Control_Elements.NavigationManager;

namespace Task_3.WPF_Pages
{
    public class StartPageViewModel : BaseViewModelForNavigationManager
    {
        public StartPageViewModel(NavigationManager nm)
        {
            _navigationManager = nm;
        }
        
        public void GoToSomePage()
        {
            _navigationManager.NavigateWithNavigationManager<Page, BaseViewModelForNavigationManager>();
        }

        public void GoToSpinner()
        {
            _navigationManager.NavigateWithNavigationManager<SpinnerPage, BaseViewModelForNavigationManager>();
        }
        public void GoToDialog()
        {
            _navigationManager.NavigateWithNavigationManager<DialogPage, BaseViewModelForNavigationManager>();
        }
    }
}