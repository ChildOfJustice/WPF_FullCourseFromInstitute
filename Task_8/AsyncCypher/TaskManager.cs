using System;
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
        public string outPutFilePath = "./resources/result.txt";
        public string filePath = "./resources/text.txt";
        public string keyFilePath = "./resources/key";
        public string ivFilePath = "./resources/iv";

        public byte[] TempKey;
        public byte[] TempIv;


        public TaskManager()
        {

            var des = new DesFramework(generateKey: true);

            TempKey = des.Key;
            //new ASCIIEncoding().GetBytes(sData);
            //MessageBox.Show("Decrypted data: " + new ASCIIEncoding().GetString(Final));

        }
        
        public void RunEncryptionProcess(CypherAlgorithm algorithm, int blockSize, int blocksQuantity)
        {
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
                        taskCompletionSource.SetResult(encryptBlock(new TaskProperties(i1,  blocksQuantity, algorithm, ReadDesiredPart(fs, i1*blockSize, (i1+1)*blockSize))));
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
            
        }
        public void RunDecryptionProcess(CypherAlgorithm algorithm, int blockSize, int blocksQuantity)
        {
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

                        var result = decryptBlock(new TaskProperties(i1, blocksQuantity, algorithm,
                            ReadDesiredPart(fs, i1 * blockSize, (i1 + 1) * blockSize)));
                        //MessageBox.Show("THE MAIN ERROR: " + new ASCIIEncoding().GetString(result.Data));
                        taskCompletionSource.SetResult(result);
                        // MessageBox.Show("OMG: " + new ASCIIEncoding().GetString(decryptBlock(new TaskProperties(i1, algorithm,
                        //     ReadDesiredPart(fs, i1 * blockSize, (i1 + 1) * blockSize))).Data));
                    }

                });

                allTasks[i] = task;
            }


            int blockNumber = 0;
            using (var outputStream = File.Open(outPutFilePath, FileMode.Create))
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
                                if(byteData[lastByteIndex] != 0)
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
            }
        }

        protected TaskProperties encryptBlock(TaskProperties props)
        {
            byte[] encryptedData = null;
            
            switch (props.Algorithm)
            {
                case CypherAlgorithm.DES:
                    DesFramework cypherFramework;
                    if (TempKey == null)
                    {
                        cypherFramework = new DesFramework();
                        cypherFramework.ImportKey(keyFilePath);
                        TempKey = cypherFramework.Key;
                    }
                    else
                        cypherFramework = new DesFramework(key: TempKey);
                    
                    
                    encryptedData = cypherFramework.EncryptData(props.Data);
                    
                    //EXPORT THE USED KEY
                    if(props.BlockNumber == props.BlocksQuantity - 1)
                        cypherFramework.ExportKey(keyFilePath);
                    return new TaskProperties(props.BlockNumber, props.BlocksQuantity,  props.Algorithm, encryptedData);
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
                    DesFramework cypherFramework = new DesFramework();
                    
                    cypherFramework.ImportKey(keyFilePath);
                    //MessageBox.Show("IMPORTED KEY " + cypherFramework.Key);

                    decryptedData = cypherFramework.DecryptData(props.Data);
                    
                    MessageBox.Show(new ASCIIEncoding().GetString(decryptedData));
                    
                    return new TaskProperties(props.BlockNumber, props.BlocksQuantity, props.Algorithm, decryptedData);
                    break;
            }

            return null;
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
    }
}