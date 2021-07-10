using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Task_8.Algorythms
{
    public class RijndaelFramework : SymmetricCryptoFramework<Rijndael>
    {
        private int keySize;
        public RijndaelFramework(int blockSize, int keySizeInBytes = 16, byte[] key = null, byte[] iv = null, bool generateKey = false)
        {
            _algorythm =  Rijndael.Create();
            _algorythm.BlockSize = blockSize*8;
            Key = key;
            if (Key != null)
                keySize = Key.Length;
            else
                keySize = keySizeInBytes;
            IV = iv;
            _algorythm.Padding = PaddingMode.Zeros;

            if (generateKey)
                Key = _algorythm.Key;
            if (IV == null)
            {
                IV = new byte[blockSize];
                for (int i = 0; i < blockSize; i++)
                {
                    IV[i] = 0;
                }
            }
        }



        public void ExportKey(string outPutFile)
        {
            using (var outputStream = File.Open(outPutFile, FileMode.Create))
                outputStream.Write(Key, 0, Key.Length);
        }
        public void ImportKey(string inPutFile)
        {
            try
            {
                Key = new byte[keySize];
                using (var inputStream = File.OpenRead(inPutFile))
                    inputStream.Read(Key, 0, keySize); // 8 bytes = 64 bit key
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        public void Test()
        {
            try
            {
                string sData = "Here is some data to encrypt.";
                MessageBox.Show("Text that will be encrypted with Rijndael: " + sData);


                var Data = EncryptData(new ASCIIEncoding().GetBytes(sData));
                string encryptedString = "";
                foreach (var @byte in Data)
                    encryptedString += @byte;
                MessageBox.Show("Encrypted data: " + encryptedString);
                
                var Final = DecryptData(Data);
                
                MessageBox.Show("Decrypted data: " + new ASCIIEncoding().GetString(Final));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}