using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Task_3.ViewModels;
using Task_3.WPF_Control_Elements.DialogHost;

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
            
            DataContext = new MainWindowViewModel();
            
            
            
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
    }
}