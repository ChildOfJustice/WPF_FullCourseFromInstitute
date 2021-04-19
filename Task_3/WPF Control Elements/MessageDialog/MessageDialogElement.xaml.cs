using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Task_3.ViewModels;
using Task_3.WPF_Control_Elements.Spinner;
using Task_3.WPF_Control_Elements.SpinnerDialog;

namespace Task_3.WPF_Control_Elements.MessageDialog
{
    public partial class MessageDialogElement : UserControl
    {
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageDialogElement), new UIPropertyMetadata("Dialog Message"));
        public static readonly DependencyProperty MessageTextSizeProperty =
            DependencyProperty.Register("MessageTextSize", typeof(int), typeof(MessageDialogElement), new UIPropertyMetadata(20));
        public static readonly DependencyProperty DialogTypeProperty =
            DependencyProperty.Register("DialogType", typeof(MessageDialogViewModel.DialogType), typeof(MessageDialogElement), new PropertyMetadata(MessageDialogViewModel.DialogType.None, (sender, eventArgs) =>
            {
                //var oldValue = (MessageDialogViewModel.DialogType)eventArgs.OldValue;
                var newValue = (MessageDialogViewModel.DialogType)eventArgs.NewValue;
            
                var context = sender as MessageDialogElement;
                
                if(newValue == MessageDialogViewModel.DialogType.Alert)
                    context.DialogHost.ContentPresenter.Content = new Alert();
                else if(newValue == MessageDialogViewModel.DialogType.Confirm)
                    context.DialogHost.ContentPresenter.Content = new Confirm();
                else if(newValue == MessageDialogViewModel.DialogType.Prompt)
                    context.DialogHost.ContentPresenter.Content = new Prompt();
            
                
            }));
        public static readonly DependencyProperty OkCommandProperty =
            DependencyProperty.Register("OkCommand", typeof(RelayCommand), typeof(MessageDialogElement), new UIPropertyMetadata(new RelayCommand(o => MessageBox.Show("Ok"))));
        public static readonly DependencyProperty YesCommandProperty =
            DependencyProperty.Register("YesCommand", typeof(RelayCommand), typeof(MessageDialogElement), new UIPropertyMetadata(new RelayCommand(o => MessageBox.Show("Yes"))));
        public static readonly DependencyProperty NoCommandProperty =
            DependencyProperty.Register("NoCommand", typeof(RelayCommand), typeof(MessageDialogElement), new UIPropertyMetadata(new RelayCommand(o => MessageBox.Show("No"))));
        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register("CancelCommand", typeof(RelayCommand), typeof(MessageDialogElement), new UIPropertyMetadata(new RelayCommand(o => MessageBox.Show("Cancel"))));

        
        public string Message
        {
            get { return (string) GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public int MessageTextSize
        {
            get { return (int)GetValue(MessageTextSizeProperty); }
            set { SetValue(MessageTextSizeProperty, value); }
        }
        
        public MessageDialogViewModel.DialogType DialogType
        {
            get { return (MessageDialogViewModel.DialogType) GetValue(DialogTypeProperty); }
            set { SetValue(DialogTypeProperty, value); }
        }
        public RelayCommand OkCommand
        {
            get { return (RelayCommand)GetValue(OkCommandProperty); }
            set { SetValue(OkCommandProperty, value); }
        }
        public RelayCommand YesCommand
        {
            get { return (RelayCommand)GetValue(YesCommandProperty); }
            set { SetValue(YesCommandProperty, value); }
        }
        public RelayCommand NoCommand
        {
            get { return (RelayCommand)GetValue(NoCommandProperty); }
            set { SetValue(NoCommandProperty, value); }
        }
        public RelayCommand CancelCommand
        {
            get { return (RelayCommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }
        
        
        
        
        public MessageDialogElement()
        {
            InitializeComponent();
            //DataContext = new MessageDialogViewModel();
        }
        
    }
}