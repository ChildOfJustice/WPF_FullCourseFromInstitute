using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Task_8.Algorythms
{
    public class DesFramework : SymmetricCryptoFramework<DES>
    {
        public DesFramework(byte[] key = null, byte[] iv = null, bool generateKey = false)
        {
            _algorythm =  DES.Create();
            Key = key;
            IV = iv;
            _algorythm.Padding = PaddingMode.Zeros;

            if (generateKey)
                Key = _algorythm.Key;
            if (IV == null)
                IV = new byte[]{ 0, 0, 0, 0, 0, 0, 0, 0};
        }



        public void ExportKey(string outPutFile)
        {
            using (var outputStream = File.Open(outPutFile, FileMode.Create))
            {
                
                outputStream.Write(Key, 0, Key.Length);
                // var key = "";
                // foreach (var VARIABLE in Key)
                // {
                //     key += (VARIABLE - '0');
                // }
                //
                // MessageBox.Show("!!!export " + new ASCIIEncoding().GetString(Key));
                // MessageBox.Show("!!!export in bytes IS " + key);
            }
        }
        public void ImportKey(string inPutFile)
        {
            try
            {
                Key = new byte[8];
                using (var inputStream = File.OpenRead(inPutFile))
                {
                    inputStream.Read(Key, 0, 8); // 8 bytes = 64 bit key
                    // var key = "";
                    // foreach (var VARIABLE in Key)
                    // {
                    //     key += (VARIABLE - '0');
                    // }
                    //
                    // MessageBox.Show("THE KEY IS " + new ASCIIEncoding().GetString(Key));
                    // MessageBox.Show("THE KEY in bytes IS " + key);
                }
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
                MessageBox.Show("Text that will be encrypted with DES: " + sData);
                
                
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
        
        
        
        
        
        
        
        public static byte[] EncryptTextToMemory(string Data, byte[] Key, byte[] IV)
        {
            try
            {
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();

                // Create a new DES object.
                DES DESalg = DES.Create();

                // Create a CryptoStream using the MemoryStream
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    DESalg.CreateEncryptor(Key, IV),
                    CryptoStreamMode.Write);

                // Convert the passed string to a byte array.
                byte[] toEncrypt = new ASCIIEncoding().GetBytes(Data);

                // Write the byte array to the crypto stream and flush it.
                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                cStream.FlushFinalBlock();

                // Get an array of bytes from the
                // MemoryStream that holds the
                // encrypted data.
                byte[] ret = mStream.ToArray();

                // Close the streams.
                cStream.Close();
                mStream.Close();

                // Return the encrypted buffer.
                return ret;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }

        public static string DecryptTextFromMemory(byte[] Data, byte[] Key, byte[] IV)
        {
            try
            {
                // Create a new MemoryStream using the passed
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(Data);

                // Create a new DES object.
                DES DESalg = DES.Create();

                // Create a CryptoStream using the MemoryStream
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    DESalg.CreateDecryptor(Key, IV),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[Data.Length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                //Convert the buffer into a string and return it.
                return new ASCIIEncoding().GetString(fromEncrypt);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
        }
    }
}