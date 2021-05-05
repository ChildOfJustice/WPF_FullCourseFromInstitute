using System.Text;
using System.Windows;
using Task_8.Algorythms;

namespace Task_8.AsyncCypher
{
    public class CypherMethods
    {
        public static TaskProperties encryptBlock(TaskProperties props, TaskManager owner, int keySize, string keyFilePath)
        {
            byte[] encryptedData = null;
            
            switch (props.Algorithm)
            {
                case CypherAlgorithm.DES:
                    DesFramework desFramework;
                    if (owner.TempKey == null)
                    {
                        desFramework = new DesFramework();
                        desFramework.ImportKey(keyFilePath);
                        owner.TempKey = desFramework.Key;
                    }
                    else
                        desFramework = new DesFramework(key: owner.TempKey);
                    
                    
                    encryptedData = desFramework.EncryptData(props.Data);
                    
                    //EXPORT THE USED KEY
                    // if(props.BlockNumber == props.BlocksQuantity - 1)
                    //     desFramework.ExportKey(keyFilePath);
                    return new TaskProperties(props.BlockNumber, props.BlocksQuantity,  props.Algorithm, encryptedData);
                    break;
                case CypherAlgorithm.TripleDES:
                    TripleDesFramework tripleDesFramework;
                    if (owner.TempKey == null)
                    {
                        tripleDesFramework = new TripleDesFramework();
                        tripleDesFramework.ImportKey(keyFilePath);
                        owner.TempKey = tripleDesFramework.Key;
                    }
                    else
                        tripleDesFramework = new TripleDesFramework(key: owner.TempKey);
                    
                    
                    encryptedData = tripleDesFramework.EncryptData(props.Data);
                    
                    //EXPORT THE USED KEY
                    // if(props.BlockNumber == props.BlocksQuantity - 1)
                    //     tripleDesFramework.ExportKey(keyFilePath);
                    return new TaskProperties(props.BlockNumber, props.BlocksQuantity,  props.Algorithm, encryptedData);
                    break;
                case CypherAlgorithm.Rijndael:
                    RijndaelFramework rijndaelFramework;
                    if (owner.TempKey == null)
                    {
                        rijndaelFramework = new RijndaelFramework(props.Data.Length, keySize);
                        rijndaelFramework.ImportKey(keyFilePath);
                    }
                    else
                        rijndaelFramework = new RijndaelFramework(props.Data.Length, key: owner.TempKey);
                    
                    
                    encryptedData = rijndaelFramework.EncryptData(props.Data);
                    // MessageBox.Show(new ASCIIEncoding().GetString(encryptedData));
                    //EXPORT THE USED KEY
                    // if(props.BlockNumber == props.BlocksQuantity - 1)
                    //     rijndaelFramework.ExportKey(keyFilePath);
                    return new TaskProperties(props.BlockNumber, props.BlocksQuantity,  props.Algorithm, encryptedData);
                case CypherAlgorithm.RSA:
                    RsaFramework rsaFramework;

                    //TODO:
                    //rsaFramework = new RsaFramework(generateKey: true);
                    // rsaFramework.ExportPubKey(keyFilePath+"pub");
                    // rsaFramework.ExportPrivateKey(keyFilePath+"private");
                    rsaFramework = new RsaFramework();
                    
                        
                    rsaFramework.ImportPubKey(keyFilePath);
                    //rsaFramework.ImportPrivateKey(keyFilePath + "private");

                    encryptedData = rsaFramework.EncryptData(props.Data);
                    // MessageBox.Show(new ASCIIEncoding().GetString(encryptedData));
                    //EXPORT THE USED KEY
                    // if (props.BlockNumber == props.BlocksQuantity - 1)
                    // {
                    //     rsaFramework.ExportPubKey(keyFilePath+"pub");
                    //     rsaFramework.ExportPrivateKey(keyFilePath+"private");
                    // }

                    return new TaskProperties(props.BlockNumber, props.BlocksQuantity,  props.Algorithm, encryptedData);
            }

            return null;
        }
        public static TaskProperties decryptBlock(TaskProperties props, int keySize, string keyFilePath)
        {
            byte[] decryptedData = null;
            
            switch (props.Algorithm)
            {
                case CypherAlgorithm.DES:
                    DesFramework desFramework = new DesFramework();
                    
                    desFramework.ImportKey(keyFilePath);
                    //MessageBox.Show("IMPORTED KEY " + cypherFramework.Key);

                    decryptedData = desFramework.DecryptData(props.Data);
                    
                    //MessageBox.Show(new ASCIIEncoding().GetString(decryptedData));
                    
                    return new TaskProperties(props.BlockNumber, props.BlocksQuantity, props.Algorithm, decryptedData);
                    break;
                case CypherAlgorithm.TripleDES:
                    TripleDesFramework tripleDesFramework = new TripleDesFramework();
                    tripleDesFramework.ImportKey(keyFilePath);
                    
                    decryptedData = tripleDesFramework.DecryptData(props.Data);
                    
                    return new TaskProperties(props.BlockNumber, props.BlocksQuantity, props.Algorithm, decryptedData);
                    break;
                case CypherAlgorithm.Rijndael:
                    RijndaelFramework rijndaelFramework = new RijndaelFramework(props.Data.Length, keySizeInBytes: keySize);
                    rijndaelFramework.ImportKey(keyFilePath);
                    
                    decryptedData = rijndaelFramework.DecryptData(props.Data);
                    
                    return new TaskProperties(props.BlockNumber, props.BlocksQuantity, props.Algorithm, decryptedData);
                case CypherAlgorithm.RSA:
                    RsaFramework rsaFramework = new RsaFramework();
                    //rsaFramework.ImportPubKey(keyFilePath+"pub");
                    rsaFramework.ImportPrivateKey(keyFilePath);
                    
                    decryptedData = rsaFramework.DecryptData(props.Data);
                    
                    return new TaskProperties(props.BlockNumber, props.BlocksQuantity, props.Algorithm, decryptedData);
            }
            return null;
        }
    }
}