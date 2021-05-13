using System.Windows;
using System.Windows.Controls;
using Task_3.WPF_Control_Elements.NavigationManager;

namespace Task_3.WPF_Pages
{
    public partial class StartPage : Page
    {
        private StartPageViewModel vm;
        public StartPage(NavigationManager nm)
        {
            InitializeComponent();
            vm = new StartPageViewModel(nm);
            this.DataContext = vm;
        }

        private void ButtonBase_OnClick_Binding_GoToSomePage_(object sender, RoutedEventArgs e)
        {
            vm.GoToSomePage();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            vm.GoToSpinner();
        }
        
        private void OnGoToDialogPageClick(object sender, RoutedEventArgs e)
        {
            vm.GoToDialog();
        }
    }
}