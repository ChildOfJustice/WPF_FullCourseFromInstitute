using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Task_3.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        public MainWindowViewModel()
        {
            _cornerRadiusConfiguration = new CornerRadius(20, 100, 30, 2);
            
            _fontSize = 100;
            _foreGround = new SolidColorBrush(Color.FromRgb(100, 100, 200));
            _backGround = new SolidColorBrush(Color.FromRgb(200, 20, 000));


            _img = new BitmapImage(new Uri("C:\\Users\\Administrator\\Desktop\\READY\\ReadyRPKS\\Task_3\\Resources\\img.jpg"));

        }
        
        
        #region Styles

        private int _fontSize;

        public int FontSize
        {
            get =>
                _fontSize;

            set
            {
                _fontSize = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }
        private Brush _foreGround;

        public Brush ForeGroundColor
        {
            get =>
                _foreGround;

            set
            {
                _foreGround = value;
                OnPropertyChanged(nameof(ForeGroundColor));
            }
        }
        private Brush _backGround;

        public Brush BackGroundColor
        {
            get =>
                _backGround;

            set
            {
                _backGround = value;
                OnPropertyChanged(nameof(BackGroundColor));
            }
        }
        
        
        private BitmapImage _img;

        public BitmapImage ImageContent
        {
            get =>
                _img;

            set
            {
                _img = value;
                OnPropertyChanged(nameof(ImageContent));
            }
        }
        #endregion
        
        #region DialogHost

        private CornerRadius _cornerRadiusConfiguration;
        public CornerRadius CornerRadiusConfiguration
        {
            get { return this._cornerRadiusConfiguration; }
            set { this._cornerRadiusConfiguration = value; }
        }
        
        
        
        
        //
        //
        //
        // private IDialogFacade dialogFacade = null;
        // private ICommand openDialogCommand = null;
        // public ICommand OpenDialogCommand
        // {
        //     get { return this.openDialogCommand; }
        //     set { this.openDialogCommand = value; }
        // }
        // public MainWindowViewModel(IDialogFacade dialogFacade)
        // {
        //     this.dialogFacade = dialogFacade;
        //     this.openDialogCommand = new RelayCommand(OnOpenDialog);
        // }
        //
        // private void OnOpenDialog(object parameter)
        // {
        //     //Dialogs.DialogService.DialogViewModelBase vm =
        //     //    new Dialogs.DialogYesNo.DialogYesNoViewModel("Question");
        //     //Dialogs.DialogService.DialogResult result =
        //     //    Dialogs.DialogService.DialogService.OpenDialog(vm, parameter as Window);
        //     Dialogs.DialogService.DialogResult result =
        //         this.dialogFacade.ShowDialogYesNo("Question", parameter as Window);
        // }

        #endregion

        // private string _synchronizedText;
        // public string SynchronizedText
        // {
        //     get => _synchronizedText;
        //     set
        //     {
        //         _synchronizedText = value;
        //         OnPropertyChanged(nameof(SynchronizedText));
        //     }
        // }
    }
}