using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Task_8.Algorythms
{
    public class TripleDesFramework : SymmetricCryptoFramework<TripleDES>
    {

        public TripleDesFramework(byte[] key = null, byte[] iv = null)
        {
            _algorythm =  TripleDES.Create();
            Key = key ?? _algorythm.Key;
            IV = iv ?? _algorythm.IV;
        }
        
        
        public void Test()
        {
            try
            {
                string sData = "Here is some data to encrypt.";
                MessageBox.Show("Text that will be encrypted with Triple DES: " + sData);

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