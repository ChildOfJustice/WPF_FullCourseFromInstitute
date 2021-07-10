using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using Task_3.ViewModels;
using Task_8.Algorythms;
using Task_8.AsyncCypher;

namespace Task_8
{
    public class MainWindowViewModel : BaseViewModel
    {
        public LinkedList<TaskManager> TasksList = new LinkedList<TaskManager>();

        public CypherAlgorithm ChosenAlgorithm = CypherAlgorithm.None;
        public int RijndaelBlockSize = 16;
        public int RijndaelKeySize = 16;

        public String SymmetricKeyFile;
        public String PublicKeyFile;
        public String PrivateKeyFile;

        public MainWindowViewModel()
        {
            _img = new BitmapImage(new Uri("C:\\Users\\Administrator\\Desktop\\READY\\ReadyRPKS\\Task_8\\resources\\mainMenu.jpg"));
        }

        
        private BitmapImage _img;
        private String chainInString;
        private LinkedList<String> chain = new LinkedList<string>();

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
        public String ChainInString
        {
            get =>
                chainInString;

            set
            {
                chainInString = value;
                OnPropertyChanged(nameof(ChainInString));
            }
        }
        public LinkedList<String> Chain
        {
            get =>
                chain;

            set
            {
                chain = value;
                ChainInString = ChainToString();
            }
        }



        private String ChainToString()
        {
            String res = "";
            for (var element = chain.First; element != null;)
            {
                var next = element.Next;

                res += element.Value;
                
                element = next;
            }

            return res;
        }
        
        
        public void SetChosenAlgorithm(string algorithmName)
        {
            switch (algorithmName)
            {
                case "DES":
                    ChosenAlgorithm = CypherAlgorithm.DES;
                    break;
                case "TripleDES":
                    ChosenAlgorithm = CypherAlgorithm.TripleDES;
                    break;
                case "Rijndael":
                    ChosenAlgorithm = CypherAlgorithm.Rijndael;
                    break;
                case "RSA":
                    ChosenAlgorithm = CypherAlgorithm.RSA;
                    break;
            }
        }
    }
}