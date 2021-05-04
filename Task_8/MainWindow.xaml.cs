using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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


            // _mainWindowViewModel.TasksList.AddFirst(new TaskManager());
            // _mainWindowViewModel.TasksList.AddBefore(_mainWindowViewModel.TasksList.First, new TaskManager());
            // _mainWindowViewModel.TasksList.AddBefore(_mainWindowViewModel.TasksList.First, new TaskManager());
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
                        
                        element.Value.RunEncryptionProcess();
                        
                
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
                        element.Value.RunDecryptionProcess();

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
            for (var element = _mainWindowViewModel.TasksList.First; element != null;)
            {
                var next = element.Next;

                element.Value.Cancel();
                
                element = next;
            }

            _mainWindowViewModel.TasksList.Clear();
        }
        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            if (_mainWindowViewModel.ChosenAlgorithm == CypherAlgorithm.None)
                MessageBox.Show("Choose the algorithm first!");
            else
            {
                TaskManager tm;
                switch (_mainWindowViewModel.ChosenAlgorithm)
                {
                    case CypherAlgorithm.DES:
                        tm = new TaskManager(_mainWindowViewModel.ChosenAlgorithm, 8, 8);

                        if (_mainWindowViewModel.SymmetricKeyFile != null)
                            tm.keyFilePath = _mainWindowViewModel.SymmetricKeyFile;
                        _mainWindowViewModel.TasksList.AddLast(tm);
                        break;
                    case CypherAlgorithm.TripleDES:
                        tm = new TaskManager(_mainWindowViewModel.ChosenAlgorithm, 8, 8);
                        
                        if (_mainWindowViewModel.SymmetricKeyFile != null)
                            tm.keyFilePath = _mainWindowViewModel.SymmetricKeyFile;
                        _mainWindowViewModel.TasksList.AddLast(tm);
                        break;
                    case CypherAlgorithm.Rijndael:
                        tm = new TaskManager(_mainWindowViewModel.ChosenAlgorithm,
                            _mainWindowViewModel.RijndaelBlockSize, _mainWindowViewModel.RijndaelKeySize);
                        if (_mainWindowViewModel.SymmetricKeyFile != null)
                            tm.keyFilePath = _mainWindowViewModel.SymmetricKeyFile;
                        _mainWindowViewModel.TasksList.AddLast(tm);
                        break;
                    case CypherAlgorithm.RSA:
                        _mainWindowViewModel.TasksList.AddLast(new TaskManager(_mainWindowViewModel.ChosenAlgorithm, 16, -1));
                        //TODO add pub and priv key path to the TaskManager
                        break;
                }
            }
                
            
            MessageBox.Show("Added, chain size: " + _mainWindowViewModel.TasksList.Count);
        }

        private void OnChooseSymmetricKeyFileButtonClick(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                _mainWindowViewModel.SymmetricKeyFile = filename;
            }
        }
        private void OnChoosePublicKeyFileButtonClick(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                _mainWindowViewModel.PublicKeyFile = filename;
            }
        }
        private void OnChoosePrivateKeyFileButtonClick(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                _mainWindowViewModel.PrivateKeyFile = filename;
            }
        }
        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            TextBlock selectedItem = (TextBlock)comboBox.SelectedItem;
            _mainWindowViewModel.SetChosenAlgorithm(selectedItem.Text);
            // MessageBox.Show(selectedItem.Text);
        }
        private void ComboBoxRijndaelBlockSizeSelected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            TextBlock selectedItem = (TextBlock)comboBox.SelectedItem;
            _mainWindowViewModel.RijndaelBlockSize = Int32.Parse(selectedItem.Text)/8;
            // MessageBox.Show(selectedItem.Text);
        }
        private void ComboBoxRijndaelKeySizeSelected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            TextBlock selectedItem = (TextBlock)comboBox.SelectedItem;
            _mainWindowViewModel.RijndaelKeySize = Int32.Parse(selectedItem.Text)/8;
            // MessageBox.Show(selectedItem.Text);
        }
    }
}