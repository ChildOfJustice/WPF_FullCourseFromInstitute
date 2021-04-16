using System.Windows;
using System.Windows.Controls;
using Task_3.WPF_Control_Elements.Spinner;

namespace Task_3.WPF_Control_Elements.SpinnerDialog
{
    public partial class SpinnerDialogElement : UserControl
    {
        public SpinnerDialogElement()
        {
            InitializeComponent();
            
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("!!!!@");
            //DialogHost.WorkSpaceBorder.CornerRadius = new CornerRadius(100,0,0,0);
            //DialogHost.WorkSpaceBorder.DataContext = null;
            
            
            
            // MessageBox.Show("COMMAND");
            // ContentWindow win = new ContentWindow();
            // if (owner != null)
            //     win.Owner = owner;
            //var vm = new CircularProgressBar();
            //win.DataContext = vm;
            // win.ShowDialog();

            //DO NOT WORK
            //DialogHost.WorkSpaceBorder.DataContext = new Content();
        }
    }
}