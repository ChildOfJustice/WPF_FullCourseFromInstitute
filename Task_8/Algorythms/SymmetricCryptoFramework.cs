using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.RightsManagement;
using System.Windows;

namespace Task_8.Algorythms
{
    public abstract class SymmetricCryptoFramework<T> where T : System.Security.Cryptography.SymmetricAlgorithm

    {
        public byte[] Key;
        public byte[] IV;

        protected T _algorythm;

        public byte[] EncryptData(byte[] Data)
        {
            try
            {
                // Create a MemoryStream.
                MemoryStream mStream = new MemoryStream();

                // Create a CryptoStream using the MemoryStream
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(mStream,
                    _algorythm.CreateEncryptor(Key, IV),
                    CryptoStreamMode.Write);

                // Convert the passed string to a byte array.
                // byte[] toEncrypt = new ASCIIEncoding().GetBytes(Data);
                byte[] toEncrypt = Data;
                
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
        public byte[] DecryptData(byte[] Data)
        {
            try
            {
                // Create a new MemoryStream using the passed
                // array of encrypted data.
                MemoryStream msDecrypt = new MemoryStream(Data);

                // Create a CryptoStream using the MemoryStream
                // and the passed key and initialization vector (IV).
                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    _algorythm.CreateDecryptor(Key, IV),
                    CryptoStreamMode.Read);

                // Create buffer to hold the decrypted data.
                byte[] fromEncrypt = new byte[Data.Length];

                // Read the decrypted data out of the crypto stream
                // and place it into the temporary buffer.
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                //Convert the buffer into a string and return it.
                // return new ASCIIEncoding().GetString(fromEncrypt);
                return fromEncrypt;
            }
            catch (CryptographicException e)
            {
                MessageBox.Show("A Cryptographic error occurred: " + e.Message );
                return null;
            }
        }
    }
}