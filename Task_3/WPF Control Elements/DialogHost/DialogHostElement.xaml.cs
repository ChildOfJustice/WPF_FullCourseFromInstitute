using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Task_3.ViewModels;

namespace Task_3.WPF_Control_Elements.DialogHost
{
    public partial class DialogHostElement : UserControl
    {
       
        public DialogHostElement()
        {
            InitializeComponent();
            this.DataContext = new DialogHostViewModel();
        }
    }
}