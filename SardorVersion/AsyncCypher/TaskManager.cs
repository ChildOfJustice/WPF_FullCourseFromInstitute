using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Task_8.Algorythms;

namespace SardorVersion.AsyncCypher
{
    public class TaskManager
    {
        public string outPutFilePath = "./resources/result.txt";
        public string filePath = "./resources/text.txt";
        public string keyFilePath = "./resources/key";
        public string ivFilePath = "./resources/iv";

        public byte[] TempKey;
        public byte[] TempIv;


        public TaskManager()
        {
            var temp = new DESCryptoServiceProvider();

            TempKey = temp.Key;
            TempIv = temp.IV;
        }
        
        public void RunEncryptionProcess(CypherAlgorithm algorithm, int blockSize, int blocksQuantity)
        {
            //Thread.Sleep(2000);
            // string sData = "Here is some data to encrypt2222.";
            // MessageBox.Show("Text that will be encrypted with DES: " + sData);
            //
            // var framework = new DesFramework();
            // var Data = framework.EncryptData(new ASCIIEncoding().GetBytes(sData));
            // TempIv = framework.IV;
            // TempKey = framework.Key;
            //
            // using (var outputStream = File.Open(outPutFilePath, FileMode.Create))
            // {
            //     try
            //     {
            //         outputStream.Write(Data, 0, Data.Length);
            //         MessageBox.Show("ENCRYPTED " + new ASCIIEncoding().GetString(Data));
            //     }
            //     catch (Exception e)
            //     {
            //         MessageBox.Show(e.Message);
            //     }
            // }
            
            
            

            //return true;
            //Console.WriteLine("Generate key for " + i + "step. Algorithm name is " + AlgorithmName);
            //GenerateKey(i);
            //Console.WriteLine("Начинается шифрование методом " + AlgorithmName);
            //Console.WriteLine("Желаете продолжить?");
            //Console.WriteLine("y/n?");
            //string name = Console.ReadLine();
            // using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            // {
            //     var smth = EncryptAsync(new TaskProperties(0, CypherAlgorithm.DES, ReadDesiredPart(fs, 0, 5)));
            //     smth.
            // }

            //https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1?view=net-5.0
            Task<TaskProperties>[] allTasks = new Task<TaskProperties>[blocksQuantity];
            
            for (int i = 0; i < blocksQuantity; i++)
            {
                TaskCompletionSource<TaskProperties> taskCompletionSource = new TaskCompletionSource<TaskProperties>();
                Task<TaskProperties> task = taskCompletionSource.Task;

                var i1 = i;
                Task.Factory.StartNew(() =>
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        taskCompletionSource.SetResult(encryptBlock(new TaskProperties(i1, algorithm, ReadDesiredPart(fs, i1*blockSize, (i1+1)*blockSize))));
                });

                allTasks[i] = task;
            }


            using (var outputStream = File.Open(outPutFilePath, FileMode.Create))
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
            }
            //MessageBox.Show(new ASCIIEncoding().GetString(allTasks[0].Result.Data));
            //await EncryptData(text, F, key, 4);
           
        }
        public void RunDecryptionProcess(CypherAlgorithm algorithm, int blockSize, int blocksQuantity)
        {
            // var framework = new DesFramework();
            // framework.Key = TempKey;
            // framework.IV = TempIv;
            //
            // var Final = new byte[1];
            // using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            // {
            //     //Final = framework.DecryptData(ReadDesiredPart(fs,0,blockSize));
            //     Final = decryptBlock(new TaskProperties(0, CypherAlgorithm.DES, ReadDesiredPart(fs, 0, blockSize))).Data;
            // }
            //
            //
            // MessageBox.Show("Decrypted data: " + new ASCIIEncoding().GetString(Final));
            // using (var outputStream = File.Open("./resources/DECTEST.txt", FileMode.Create))
            // {
            //     try
            //     {
            //         outputStream.Write(Final, 0, Final.Length);
            //         MessageBox.Show("Decrypted " + new ASCIIEncoding().GetString(Final));
            //     }
            //     catch (Exception e)
            //     {
            //         MessageBox.Show(e.Message);
            //     }
            // }
            //
            //
            // return;
            
            
            
            
            
            
            
            //https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1?view=net-5.0
            Task<TaskProperties>[] allTasks = new Task<TaskProperties>[blocksQuantity];
            
            for (int i = 0; i < blocksQuantity; i++)
            {
                TaskCompletionSource<TaskProperties> taskCompletionSource = new TaskCompletionSource<TaskProperties>();
                Task<TaskProperties> task = taskCompletionSource.Task;

                var i1 = i;
                Task.Factory.StartNew(() =>
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {

                        var result = decryptBlock(new TaskProperties(i1, algorithm,
                            ReadDesiredPart(fs, i1 * blockSize, (i1 + 1) * blockSize)));
                        //MessageBox.Show("THE MAIN ERROR: " + new ASCIIEncoding().GetString(result.Data));
                        taskCompletionSource.SetResult(result);
                        // MessageBox.Show("OMG: " + new ASCIIEncoding().GetString(decryptBlock(new TaskProperties(i1, algorithm,
                        //     ReadDesiredPart(fs, i1 * blockSize, (i1 + 1) * blockSize))).Data));
                    }

                });

                allTasks[i] = task;
            }

            //MessageBox.Show("RESULT: " + new ASCIIEncoding().GetString(allTasks[0].Result.Data));
            using (var outputStream = File.Open(outPutFilePath, FileMode.Create))
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
            }
        }

        protected TaskProperties encryptBlock(TaskProperties props)
        {
            byte[] encryptedData = null;
            
            switch (props.Algorithm)
            {
                case CypherAlgorithm.DES:
                    
                    DesFramework cypherFramework = new DesFramework(TempKey, TempIv);
                    // using (FileStream keyFileStream = new FileStream(keyFilePath, FileMode.Open, FileAccess.Read), ivFileStream = new FileStream(keyFilePath, FileMode.Open, FileAccess.Read))
                    //     cypherFramework = new DesFramework(ReadDesiredPart(keyFileStream,0,64), ReadDesiredPart(ivFileStream,0,64));
                        
                    
                    encryptedData = cypherFramework.EncryptData(props.Data);
                    //TempKey = cypherFramework.Key;
                    //TempIv = cypherFramework.IV;
                    // using (var outputStream = File.Open(keyFilePath, FileMode.Create))
                    // {
                    //     
                    //         
                    //     //WILL BE LOCKED ON THIS AND WAIT UNTIL A PROMISE WILL BE RESOLVED
                    //     outputStream.Write(TempKey, 0, TempKey.Length);
                    //         
                    //     
                    // }

                    //MessageBox.Show("First:!!! " + new ASCIIEncoding().GetString(TempKey) + " | " + new ASCIIEncoding().GetString(TempIv));
                    return new TaskProperties(props.BlockNumber, props.Algorithm, encryptedData);
                    break;
            }

            return null;
        }
        protected TaskProperties decryptBlock(TaskProperties props)
        {
            byte[] decryptedData = null;
            
            switch (props.Algorithm)
            {
                case CypherAlgorithm.DES:
                    DesFramework cypherFramework = new DesFramework(TempKey, TempIv);
                    
                    // using (FileStream keyFileStream = new FileStream(keyFilePath, FileMode.Open, FileAccess.Read), ivFileStream = new FileStream(ivFilePath, FileMode.Open, FileAccess.Read))
                    //     cypherFramework = new DesFramework(ReadDesiredPart(keyFileStream,0,64), ReadDesiredPart(ivFileStream,0,64));

                    // cypherFramework.Key = TempKey;
                    // cypherFramework.IV = TempIv;
                    decryptedData = cypherFramework.DecryptData(props.Data);
                    MessageBox.Show(new ASCIIEncoding().GetString(decryptedData));
                    return new TaskProperties(props.BlockNumber, props.Algorithm, decryptedData);
                    break;
            }

            return null;
        }
        
        

        public void readFile()
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var resBytes = ReadDesiredPart(fs, 5, 10);
                MessageBox.Show(new ASCIIEncoding().GetString(resBytes));

                
                // string res = ""; // !read in reversed order!
                // for (int offset = 1; offset <= fs.Length; offset++)
                // {
                //     fs.Seek(-offset, SeekOrigin.End);
                //     res += Convert.ToChar(fs.ReadByte());
                // }
            }
        }
        
        public byte[] ReadDesiredPart(FileStream fs, int startPosition, int endPosition) {

            byte[] buffer = new byte[endPosition - startPosition];

            int arrayOffset = 0;

            //lock (fsLock) {
                fs.Seek(startPosition, SeekOrigin.Begin);

                int numBytesRead = fs.Read(buffer, arrayOffset , endPosition - startPosition);

                //  Typically used if you're in a loop, reading blocks at a time
                arrayOffset += numBytesRead;
            //}

            return buffer;
        }
        
        
        
        
        
        
        
        
        // public static async Task EncryptData(String inputFile, String outputFile, String keyFile, int threadsCount = 4)
        // {
        //     
        //     using (var inputStream = File.Open(inputFile, FileMode.Open))
        //     {
        //         using (var outputStream = File.Open(outputFile, FileMode.Create))
        //         {
        //             var buffers = Enumerable
        //                 .Repeat(0, threadsCount)
        //                 .Select(_ => new byte[blockSizeInBytes])
        //                 .ToArray();
        //
        //             var readBytes = new byte[blockSizeInBytes * threadsCount];
        //             while (true)
        //             {
        //                 var readBytesCount = await inputStream.ReadAsync(readBytes, 0, blockSizeInBytes * threadsCount);
        //                 // TODO: if readBytesCount != BlockSize * threadsCount
        //
        //                 for (var i = 0; i < buffers.Length; i++)
        //                 {
        //                     Array.Copy(readBytes, i * blockSizeInBytes, buffers[i], 0, blockSizeInBytes);
        //                 }
        //
        //                 try
        //                 {
        //                     await Task
        //                         .WhenAll(buffers
        //                             .Select(buf => Task.Run(() =>
        //                             {
        //                                 return Encryption(Algorithm, buf);
        //                             }))
        //                             .ToArray())
        //                         .ContinueWith(async t =>
        //                         {
        //                             var encryptedBuffers = await t;
        //                             foreach (var buf in encryptedBuffers)
        //                             {
        //                                 try
        //                                 {
        //                                     outputStream.Write(buf, 0, buf.Length);
        //                                 }
        //                                 catch
        //                                 {
        //                                     Console.WriteLine("ёбаныйрот этого казино блядь!");
        //                                 }
        //                             }
        //                         });
        //                 }
        //                 catch
        //                 {
        //                     // TODO
        //                     Console.WriteLine("нихуя!");
        //                 }
        //                 // TODO
        //                 if (readBytesCount != blockSizeInBytes * threadsCount)
        //                 {
        //                     break;
        //                 }
        //             }
        //         }
        //     }
        // }

    }
}