using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Task_3.ViewModels;
using Task_3.WPF_Control_Elements.DialogHost;
using Task_3.WPF_Control_Elements.NavigationManager;
using Task_3.WPF_Control_Elements.Spinner;
using Task_3.WPF_Pages;

namespace Task_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {


        private RelayCommand _command = new RelayCommand(o =>
        {
            MessageBox.Show("!!!!");
        });
        public RelayCommand CommandForDialogBox
        {
            get { return this._command; }
            set { this._command = value; }
        }
        
        
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                frame.Content = new StartPage(new NavigationManager(frame.NavigationService));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
            
            //DataContext = new MainWindowViewModel();





            //Loaded += OnLoad;
            // 
            // Button1.DataContext = smth;
            //this.DataContext = new MainWindowViewModel();

            //Button1.FontSize = smth.FontSize;
        }
        
        // private void OnLoad(object sender, RoutedEventArgs e)
        // {
        //     mainDataContext.FontSize = 100;
        // }
        private void OnMainButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ((NavigationManager) DataContext).Navigate<Page>(new BaseViewModel());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
            
        }
    }
}