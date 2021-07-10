using Task_3.WPF_Control_Elements.NavigationManager;

namespace Task_3.ViewModels
{
    public class BaseViewModelForNavigationManager : BaseViewModel
    {
        protected NavigationManager _navigationManager;

        public NavigationManager NavigationManager
        {
            get => _navigationManager;

            set
            {
                _navigationManager = value;
            }
        }
    }
}