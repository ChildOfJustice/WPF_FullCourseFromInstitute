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
        private MainWindowViewModel _mainWindowViewModel;
        int counter = 0;

        public MainWindow()
        {
            InitializeComponent();
            _mainWindowViewModel = new MainWindowViewModel();
            DataContext = _mainWindowViewModel;


            _mainWindowViewModel.TasksList.AddFirst(new TaskManager());
            _mainWindowViewModel.TasksList.AddBefore(_mainWindowViewModel.TasksList.First, new TaskManager());
            _mainWindowViewModel.TasksList.AddBefore(_mainWindowViewModel.TasksList.First, new TaskManager());
        }

        private void OnEncryptButtonClick(object sender, RoutedEventArgs e)
        {
            
            
            Task.Run(() =>
            {
                try
                {
                    counter = 0;
                    for(var element = _mainWindowViewModel.TasksList.First; element != null; )
                    {
                        
                        var next = element.Next;

                        
                        element.Value.outputFilePath = "./resources/encryptedPart"+counter+".txt";
                        if (counter == 0)
                            element.Value.inputFilePath = "./resources/text.txt";
                        else
                            element.Value.inputFilePath = "./resources/encryptedPart"+(counter-1)+".txt";
                        
                        element.Value.RunEncryptionProcess(CypherAlgorithm.DES, 8, 3);
                        
                
                        // if(element.Value.removalCondition == true)
                        //     myCollection.Remove(element); // as a side effect it.Next == null
                        MessageBox.Show("FINISHED ENCRYPTION " + counter);
                        element = next;
                        counter++;
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            });

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

        private void OnDecryptButtonClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    counter = _mainWindowViewModel.TasksList.Count - 1;
                    
                    for(var element = _mainWindowViewModel.TasksList.Last; element != null; )
                    {
                        var prev = element.Previous;
                        
                        
                        if(counter == 0)
                            element.Value.outputFilePath = "./resources/FullyDecrypted.txt";
                        else
                            element.Value.outputFilePath = "./resources/decryptedPart"+counter+".txt";
                        
                        if (counter == _mainWindowViewModel.TasksList.Count - 1)
                        {
                            //the last encrypted file
                            element.Value.inputFilePath = "./resources/encryptedPart"+ counter +".txt";
                        } else 
                            element.Value.inputFilePath = "./resources/decryptedPart"+(counter+1)+".txt";
                        
                        //MessageBox.Show("started DECRYPTION " + counter + " input: " + element.Value.inputFilePath + " out: " + element.Value.outputFilePath);
                        element.Value.RunDecryptionProcess(CypherAlgorithm.DES, 8, 3);

                        MessageBox.Show("FINISHED DECRYPTION " + counter);
                        element = prev;
                        counter--;
                    }
                    
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            });
            
        }
        

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            var temp = _mainWindowViewModel.TasksList.First.Value;
            temp.Cancel();
        }

        
    }
}