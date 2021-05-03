﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Task_8.Algorythms;

namespace Task_8.AsyncCypher
{
    public class TaskManager
    {
        public string outputFilePath = "./resources/result.txt";
        public string inputFilePath = "./resources/text.txt";
        public string keyFilePath = "./resources/key";

        public byte[] TempKey;

        private CypherAlgorithm algorithm;
        private int blockSize;
        private int keySize;
        private int blocksQuantity;
        
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        public TaskManager(CypherAlgorithm _algorithm, int _blockSize, int _keySize, int _blocksQuantity)
        {
            algorithm = _algorithm;
            blockSize = _blockSize;
            keySize = _keySize;
            blocksQuantity = _blocksQuantity;
            //new ASCIIEncoding().GetBytes(sData);
            //MessageBox.Show("Decrypted data: " + new ASCIIEncoding().GetString(Final));
        }
        
        public void RunEncryptionProcess()
        {
            //https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1?view=net-5.0
            CancellationToken ct = tokenSource.Token;
            
            
            
            // if (algorithm == CypherAlgorithm.RSA)
            //     blocksQuantity = 1;
            // else
            //     blocksQuantity = DetermineBlocksQuantity(inputFilePath, blockSize);
            
            
            Task<TaskProperties>[] allTasks = new Task<TaskProperties>[blocksQuantity];
            
            for (int i = 0; i < blocksQuantity; i++)
            {
                TaskCompletionSource<TaskProperties> taskCompletionSource = new TaskCompletionSource<TaskProperties>();
                Task<TaskProperties> task = taskCompletionSource.Task;

                var i1 = i;
                Task.Factory.StartNew(() =>
                {
                    // Were we already canceled?
                    ct.ThrowIfCancellationRequested();
                    
                    try
                    {
                        if (algorithm == CypherAlgorithm.RSA)
                        {
                            using (FileStream fs = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                                taskCompletionSource.SetResult(CypherMethods.encryptBlock(new TaskProperties(i1,
                                        blocksQuantity, algorithm,
                                        ReadDesiredPart(fs, i1 * blockSize, -1)), TempKey, keySize,
                                    keyFilePath));
                        }
                        else
                        {
                            
                            using (FileStream fs = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                                taskCompletionSource.SetResult(CypherMethods.encryptBlock(new TaskProperties(i1,
                                        blocksQuantity, algorithm,
                                        ReadDesiredPart(fs, i1 * blockSize, (i1 + 1) * blockSize)), TempKey, keySize,
                                    keyFilePath));
                        }
                       
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                    
                }, tokenSource.Token);

                allTasks[i] = task;
            }

            if (ct.IsCancellationRequested)
            {
                // Clean up here, then...
                ct.ThrowIfCancellationRequested();
            }
            else
            {
                using (var outputStream = File.Open(outputFilePath, FileMode.Create))
                {
                    foreach (var promise in allTasks)
                    {
                        try
                        {
                            //WILL BE LOCKED ON THIS AND WAIT UNTIL A PROMISE WILL BE RESOLVED
                            outputStream.Write(promise.Result.Data, 0, promise.Result.Data.Length);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                    outputStream.Close();
                }

                
            }
        }
        public void RunDecryptionProcess()
        {
            //https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1?view=net-5.0
            CancellationToken ct = tokenSource.Token;
            
            
            
            // if (algorithm == CypherAlgorithm.RSA)
            //     blocksQuantity = 1;
            // else
            //     blocksQuantity = DetermineBlocksQuantity(inputFilePath, blockSize);

            
            Task<TaskProperties>[] allTasks = new Task<TaskProperties>[blocksQuantity];


            for (int i = 0; i < blocksQuantity; i++)
            {
                TaskCompletionSource<TaskProperties> taskCompletionSource = new TaskCompletionSource<TaskProperties>();
                Task<TaskProperties> task = taskCompletionSource.Task;

                var i1 = i;
                Task.Factory.StartNew(() =>
                {
                    // Were we already canceled?
                    ct.ThrowIfCancellationRequested();

                    try
                    {
                        if (algorithm == CypherAlgorithm.RSA)
                        {
                            using (FileStream fs = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                            {
                                var result = CypherMethods.decryptBlock(new TaskProperties(i1, blocksQuantity, algorithm,
                                    ReadDesiredPart(fs, i1 * blockSize, -1)), keySize, keyFilePath);
                                taskCompletionSource.SetResult(result);
                            }
                        }
                        else
                        {
                            using (FileStream fs = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                            {
                                var result = CypherMethods.decryptBlock(new TaskProperties(i1, blocksQuantity, algorithm,
                                    ReadDesiredPart(fs, i1 * blockSize, (i1 + 1) * blockSize)), keySize, keyFilePath);
                                taskCompletionSource.SetResult(result);
                            }
                        }

                        
                    }catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                });

                allTasks[i] = task;
            }

            if (ct.IsCancellationRequested)
            {
                // Clean up here, then...
                ct.ThrowIfCancellationRequested();
            }
            else
            {
                int blockNumber = 0;
                using (var outputStream = File.Open(outputFilePath, FileMode.Create))
                {
                    foreach (var promise in allTasks)
                    {
                        blockNumber++;
                        try
                        {
                            if (blockNumber == blocksQuantity)
                            {
                                //WILL BE LOCKED ON THIS AND WAIT UNTIL A PROMISE WILL BE RESOLVED
                                var byteData = promise.Result.Data;

                                int lastByteIndex;
                                for (lastByteIndex = byteData.Length - 1; lastByteIndex >= 0; lastByteIndex--)
                                {
                                    if (byteData[lastByteIndex] != 0)
                                        break;

                                }

                                var dataToWrite = new byte[lastByteIndex + 1];
                                Array.Copy(byteData, dataToWrite, lastByteIndex + 1);
                                outputStream.Write(dataToWrite, 0, dataToWrite.Length);
                            }
                            else
                            {
                                //WILL BE LOCKED ON THIS AND WAIT UNTIL A PROMISE WILL BE RESOLVED
                                outputStream.Write(promise.Result.Data, 0, promise.Result.Data.Length);
                            }

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }


                    }
                    outputStream.Close();
                }
            }
        }


        public int DetermineBlocksQuantity(string inputFilePath, int blockSize)
        {
            FileInfo fi = new FileInfo(inputFilePath);
            var fileSize = fi.Length;
            return (int)(fileSize / blockSize + 1);
        }
        
        public byte[] ReadDesiredPart(FileStream fs, int startPosition, int endPosition) {

            if (endPosition != -1)
            {
                byte[] buffer = new byte[endPosition - startPosition];

                int arrayOffset = 0;

                //lock (fsLock) {
                //MessageBox.Show("Ned to read from " + startPosition + " to " + endPosition);
                fs.Seek(startPosition, SeekOrigin.Begin);
                var fileInfo = new FileInfo(fs.Name);
                if (endPosition > fileInfo.Length)
                    endPosition -= (int)(endPosition - fileInfo.Length);
                int numBytesRead = fs.Read(buffer, arrayOffset, endPosition - startPosition);

                //MessageBox.Show(new ASCIIEncoding().GetString(buffer));
                //  Typically used if you're in a loop, reading blocks at a time
                arrayOffset += numBytesRead;
                //}

                return buffer;
            }
            else
            {
                //reading all
                return File.ReadAllBytes(fs.Name);
            }
        }


        public void Cancel()
        {
            tokenSource.Cancel();
        }
    }
}