using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Task_8.Algorythms;

namespace Task_8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnEncryptButtonClick(object sender, RoutedEventArgs e)
        {
            var desFramework = new DesFramework();
            desFramework.Test();
            
            var tripleDesFramework = new TripleDesFramework();
            tripleDesFramework.Test();
            
            var rijndaelFramework = new RijndaelFramework();
            rijndaelFramework.Test();

            var rsaFramework = new RsaFramework();
            rsaFramework.Test();

        }


        
    }
}