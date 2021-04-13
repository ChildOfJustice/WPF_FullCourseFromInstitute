﻿using System.Windows;
using System.Windows.Input;

namespace Task_3.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        public MainWindowViewModel()
        {
            _cornerRadiusConfiguration = new CornerRadius(20, 100, 30, 2);
        }
        
        
        
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