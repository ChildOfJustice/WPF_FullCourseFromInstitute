using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Task_8
{
    class Task_8
    {
        private static bool _ProtectKey;
        private static string _AlgorithmName = "DES";
        public static string AlgorithmName
        {
            get { return _AlgorithmName; }
            set { _AlgorithmName = value; }
        }
        public static bool ProtectKey
        {
            get { return _ProtectKey; }
            set { _ProtectKey = value;}
        }

        static int ReadBlock(Stream s, byte[] block)
        {
            int position = 0;
            while (position < block.Length)
            {
                var actuallyRead = s.Read(block, position, block.Length - position);
                if (actuallyRead == 0)
                    break;
                position += actuallyRead;
            }
            return position;
        }
        public static void GenerateKey(int a)
        {
            // ...
            // Создать алгоритм
            SymmetricAlgorithm Algorithm = SymmetricAlgorithm.Create(AlgorithmName);
            Algorithm.GenerateKey();
            
            // Получить ключ
            byte[] Key = Algorithm.Key;
            ProtectKey = true;
            if (ProtectKey)
            {
                // Использовать DPAPI для шифрования ключа
                Key = ProtectedData.Protect(
                    Key, null, DataProtectionScope.LocalMachine);
            }

            // Сохранить ключ в файле key.config
            using (FileStream fs = new FileStream("C:\\Users\\Frost\\Desktop\\" + a + ".txt", FileMode.Create))
            {
                fs.Write(Key, 0, Key.Length);
            }
        }
        public static string GenerateKeyA(int a)
        {
            // ...
            RSACryptoServiceProvider Algorithm = new RSACryptoServiceProvider();

            // Сохранить секретный ключ
            string CompleteKey = Algorithm.ToXmlString(true);
            byte[] KeyBytes = Encoding.UTF8.GetBytes(CompleteKey);

            KeyBytes = ProtectedData.Protect(KeyBytes,
                    null, DataProtectionScope.LocalMachine);

            using (FileStream fs = new FileStream("C:\\Users\\taskf\\Desktop\\" + a + ".txt", FileMode.Create))
            {
                fs.Write(KeyBytes, 0, KeyBytes.Length);
                
            }

            // Вернуть открытый ключ
            return Algorithm.ToXmlString(false);
        }
        public static void ReadKey(SymmetricAlgorithm algorithm, string keyFile)
        {
            // ...
            byte[] Key;

            using (FileStream fs = new FileStream(keyFile, FileMode.Open, FileAccess.Read))
            {
                Key = new byte[fs.Length];
                fs.Read(Key, 0, (int)fs.Length);
            }

            if (ProtectKey)
                algorithm.Key = ProtectedData.Unprotect(Key, null, DataProtectionScope.LocalMachine);
            else
                algorithm.Key = Key;

        }
        
        public static async void EncryptData(String inputFile, String outputFile, String keyFile)
        {
            // ...
            bool stop = false;
          
            using (FileStream SourceStream1 = File.Open(inputFile, FileMode.Open)) {
                byte[] buf1 = new byte[16];
                byte[] buf2 = new byte[16];
                byte[] buf3 = new byte[16];
                byte[] buf4 = new byte[16];
                byte[] BigArr = new byte[64];
                while (!stop)
            {
                    int count = await SourceStream1.ReadAsync(BigArr, 0, 64);
                    System.Buffer.BlockCopy(BigArr, 0, buf1, 0, 16);
                    System.Buffer.BlockCopy(BigArr, 16, buf2, 0, 16);
                    System.Buffer.BlockCopy(BigArr, 32, buf3, 0, 16);
                    System.Buffer.BlockCopy(BigArr, 48, buf4, 0, 16);
                   
                   if (count != 64) stop = true;
                    // Создать алгоритм шифрования
                    SymmetricAlgorithm Algorithm = SymmetricAlgorithm.Create(AlgorithmName);
                    Console.WriteLine("Encrypting with " + AlgorithmName);

                ReadKey(Algorithm, keyFile);
                    // Сгенерировать случайный вектор инициализации (IV)
                    // для использования с алгоритмом
                    Algorithm.GenerateIV();
                    Task[] tasks1 = new Task[4]
                    {
                        Task.Run(() => {
                            MemoryStream Target1 = new MemoryStream();
                            Target1.Write(Algorithm.IV, 0, Algorithm.IV.Length);
                            CryptoStream cs1 = new CryptoStream(Target1, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);
                            cs1.Write(buf1, 0, buf1.Length);
                    cs1.FlushFinalBlock();
                    // Вернуть зашифрованный поток данных в виде байтового массива
                    buf1 = Target1.ToArray();
                           }),


                        Task.Run(() => {
                            MemoryStream Target2 = new MemoryStream();
                            Target2.Write(Algorithm.IV, 0, Algorithm.IV.Length);
                            CryptoStream cs2 = new CryptoStream(Target2, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);
                            cs2.Write(buf2, 0, buf2.Length);
                    cs2.FlushFinalBlock();
                    // Вернуть зашифрованный поток данных в виде байтового массива
                    buf2 = Target2.ToArray();
                            }),

                        Task.Run(() => {
                            MemoryStream Target3 = new MemoryStream();
                            Target3.Write(Algorithm.IV, 0, Algorithm.IV.Length);
                           CryptoStream cs3 = new CryptoStream(Target3, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);
                            cs3.Write(buf3, 0, buf3.Length);
                            cs3.FlushFinalBlock();
                            // Вернуть зашифрованный поток данных в виде байтового массива
                            buf3 = Target3.ToArray();}),

                        Task.Run(() => {
                            MemoryStream Target4 = new MemoryStream();
                            Target4.Write(Algorithm.IV, 0, Algorithm.IV.Length);
                          CryptoStream cs4 = new CryptoStream(Target4, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);
                            cs4.Write(buf4, 0, buf4.Length);
                            cs4.FlushFinalBlock();
                            // Вернуть зашифрованный поток данных в виде байтового массива
                            buf4 = Target4.ToArray();})
                    };

                    await Task.WhenAll(tasks1);
                    try
                    {
;                        using (FileStream SourceStream = File.Open(outputFile, FileMode.OpenOrCreate))
                        {
                            SourceStream.Seek(0, SeekOrigin.End);
                                await SourceStream.WriteAsync(buf1, 0, buf1.Length);
                                await SourceStream.WriteAsync(buf2, 0, buf2.Length);
                                await SourceStream.WriteAsync(buf3, 0, buf3.Length);
                                await SourceStream.WriteAsync(buf4, 0, buf4.Length);
                        }
                    }
                    catch { }
                }
            }
        }

        public static void DecryptData(String inputFile, String outputFile, String KeyFile)
        {
            // ...


            // Создать алгоритм
            // вот эта хуйня изобретена укропами, взята буквально с боем нахуй (=
            SymmetricAlgorithm Algorithm = SymmetricAlgorithm.Create(AlgorithmName);
            ReadKey(Algorithm, KeyFile);
            byte[] ClearData = Encoding.UTF8.GetBytes(inputFile);
            

            // Расшифровать информацию
            MemoryStream Target = new MemoryStream();

            // Прочитать вектор инициализации (IV)
            // и инициализировать им алгоритм
            int ReadPos = 0;
            byte[] IV = new byte[Algorithm.IV.Length];
           Array.Copy(ClearData, IV, IV.Length);
            Algorithm.IV = IV;
            ReadPos += Algorithm.IV.Length;

            CryptoStream cs = new CryptoStream(Target, Algorithm.CreateDecryptor(),
                CryptoStreamMode.Write);
           cs.Write(ClearData, ReadPos, ClearData.Length - ReadPos);
            cs.FlushFinalBlock();

            // Получить байты из потока в памяти и преобразовать их в текст
           ClearData = Target.ToArray();
            using (FileStream fs = new FileStream(outputFile, FileMode.Create))
            {
                fs.Write(ClearData, 0, ClearData.Length);
            }



        }





        private static void ReadKeyA(RSACryptoServiceProvider algorithm, string keyFile)
        {
            // ...
            byte[] KeyBytes;

            using (FileStream fs = new FileStream(keyFile, FileMode.Open))
            {
                KeyBytes = new byte[fs.Length];
                fs.Read(KeyBytes, 0, (int)fs.Length);
            }

            KeyBytes = ProtectedData.Unprotect(KeyBytes,
                    null, DataProtectionScope.LocalMachine);

            algorithm.FromXmlString(Encoding.UTF8.GetString(KeyBytes));
        }

        public static void EncryptDataA(Object obj)
        {
          
            
            // Создать алгоритм на основе открытого ключа
            RSACryptoServiceProvider Algorithm = new RSACryptoServiceProvider();
    //        Algorithm.FromXmlString(GenerateKeyA("C:\\Users\\Frost\\Desktop\\key.txt"));
            
            // Зашифровать данные
            //return Algorithm.Encrypt((enc1.buff), true);
        }

       // public static string DecryptDataA(Object obj)
       // {
            // ...
            
            //RSACryptoServiceProvider Algorithm = new RSACryptoServiceProvider();
            //ReadKeyA(Algorithm, enc1.Key);

            //byte[] ClearData = Algorithm.Decrypt(enc1.buff, true);
           // return Convert.ToString(
                   //Encoding.UTF8.GetString(ClearData));
       // }
        static void Main(string[] args)
        {
            int[] stack = { 1};
            String test = "C:\\Users\\Frost\\Desktop\\test.txt";
            String text = "C:\\Users\\Frost\\Desktop\\text.txt";
            String key = "C:\\Users\\Frost\\Desktop\\0.txt";
            String control = "C:\\Users\\Frost\\Desktop\\control.txt";
            //1-DES
            //2-TDES
            //3-Rijndael
            //4-RSA
       
            
            
            

            for (int i = 0; i < stack.Length; i++)
            {
                if (stack[i] == 1)
                {
                    AlgorithmName = "TripleDES";
                    Console.WriteLine("Generate key for " + i + "step. Algorithm name is " + AlgorithmName);
                    GenerateKey(i);
                    Console.WriteLine("Начинается шифрование методом " + AlgorithmName);
                    Console.WriteLine("Желаете продолжить?");
                    Console.WriteLine("y/n?");
                    string name = Console.ReadLine();
             
                    if (name == "y") {

                        EncryptData(text, test, key);
                        
                        
                    }

                   
                }
                if (stack[i] == 2)
                {
                    AlgorithmName = "TripleDES";
                    Console.WriteLine("Generate key for " + i + "step. Algorithm name is " + AlgorithmName);
                    GenerateKey(stack[i]);
                }
                if (stack[i] == 3)
                {
                    AlgorithmName = "Rijndael";
                    Console.WriteLine("Generate key for " + i + "step. Algorithm name is " + AlgorithmName);
                    GenerateKey(stack[i]);
                }
                if (stack[i] == 4)
                {
                    AlgorithmName = "RSA";
                    Console.WriteLine("Generate key for " + i + "step. Algorithm name is " + AlgorithmName);
                    GenerateKeyA(stack[i]);
                }
            }
           
            
           

        }

    }
    
}
