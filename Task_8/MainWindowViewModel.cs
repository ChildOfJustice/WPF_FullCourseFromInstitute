using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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