using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Task_8.AsyncCypher;
using Task_8.Algorythms;

namespace Task_8
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnEncryptButtonClick(object sender, RoutedEventArgs e)
        {
            
            TaskManager mainManager = new TaskManager();


            TestButton.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));

            // Func<CypherAlgorithm, int, int, Task> aaa = new Func<CypherAlgorithm, int, int, Task>(mainManager.RunEncryptionProcess);
            // string param = "Hi";
            Task.Run(() =>
            {
                mainManager.RunEncryptionProcess(CypherAlgorithm.DES, 8, 3);
                mainManager.filePath = "./resources/result.txt";
                mainManager.outPutFilePath = "./resources/DECRYPTED.txt";
                mainManager.RunDecryptionProcess(CypherAlgorithm.DES, 8, 3);
            });
            
            //mainManager.RunEncryptionProcess(CypherAlgorithm.DES, 8, 1);
            //mainManager.filePath = "./resources/result.txt";
            //mainManager.outPutFilePath = "./resources/DECRYPTED.txt";
            //mainManager.RunDecryptionProcess(CypherAlgorithm.DES, 8, 1);
            //
            
            TestButton.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            
            // try
            // {
            //     mainManager.readFile();
            // }
            // catch (Exception exception)
            // {
            //     MessageBox.Show(exception.Message);
            //     throw;
            // }








            // var desFramework = new DesFramework();
            // desFramework.Test();
            //
            // var tripleDesFramework = new TripleDesFramework();
            // tripleDesFramework.Test();
            //
            // var rijndaelFramework = new RijndaelFramework();
            // rijndaelFramework.Test();
            //
            // var rsaFramework = new RsaFramework();
            // rsaFramework.Test();

        }


        
    }
}