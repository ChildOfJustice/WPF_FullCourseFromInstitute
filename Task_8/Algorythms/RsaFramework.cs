using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Task_8.Algorythms
{
    public class RsaFramework
    {
        private RSACryptoServiceProvider _algorythm;
        public RsaFramework(String privKeyString = null, String pubKeyString = null, bool generateKey = false)
        {
            _algorythm = new RSACryptoServiceProvider(2048);

            if (generateKey)
            {
                //how to get the private key
                var privKey = _algorythm.ExportParameters(true);

                //and the public key ...
                var pubKey = _algorythm.ExportParameters(false);
            } else
            {
                if (privKeyString != null)
                {
                    //get a stream from the string
                    var sr = new System.IO.StringReader(privKeyString);
                    //we need a deserializer
                    var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                    //get the object back from the stream
                    _algorythm.ImportParameters((RSAParameters)xs.Deserialize(sr));
                }

                if (pubKeyString != null)
                {
                    //get a stream from the string
                    var sr = new System.IO.StringReader(pubKeyString);
                    //we need a deserializer
                    var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                    //get the object back from the stream
                    _algorythm.ImportParameters((RSAParameters)xs.Deserialize(sr));
                }
            }
            

            

            //conversion for the private key is no black magic either ... omitted
            
        }


        public byte[] EncryptData(byte[] data)
        {
            // //for encryption, always handle bytes...
            // var bytesPlainTextData = System.Text.Encoding.Unicode.GetBytes(plainTextData);

            //apply pkcs#1.5 padding and encrypt our data 
            var bytesCypherText = _algorythm.Encrypt(data, false);

            //we might want a string representation of our cypher text... base64 will do
            // var cypherText = Convert.ToBase64String(bytesCypherText);
            // MessageBox.Show("Cypher text: " + cypherText);

            return bytesCypherText;
        }

        public byte[] DecryptData(byte[] data)
        {
            // //first, get our bytes back from the base64 string ...
            // bytesCypherText = Convert.FromBase64String(cypherText);

            //we want to decrypt, therefore we need a csp and load our private key
            // csp = new RSACryptoServiceProvider();
            // csp.ImportParameters(privKey);

            //decrypt and strip pkcs#1.5 padding
            var bytesPlainTextData = _algorythm.Decrypt(data, false);

            //get our original plainText back...
            //plainTextData = System.Text.Encoding.Unicode.GetString(bytesPlainTextData);

            //MessageBox.Show("Plain text: " + plainTextData);

            return bytesPlainTextData;
        }


        public void ExportPubKey(string outPutFile)
        {
           
            //and the public key ...
            var pubKey = _algorythm.ExportParameters(false);
            //we need some buffer
            var sw = new System.IO.StringWriter();
            //we need a serializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //serialize the key into the stream
            xs.Serialize(sw, pubKey);
            //get the string from the stream
            var pubKeyString = sw.ToString();

            
            File.WriteAllText(outPutFile, pubKeyString);
            
            // var keyBytes = new ASCIIEncoding().GetBytes(pubKeyString);
            // using (var outputStream = File.Open(outPutFile, FileMode.Create))
            //     outputStream.Write(keyBytes, 0, keyBytes.Length);
        }
        public void ExportPrivateKey(string outPutFile)
        {
           
            //how to get the private key
            var privKey = _algorythm.ExportParameters(true);
            //we need some buffer
            var sw = new System.IO.StringWriter();
            //we need a serializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //serialize the key into the stream
            xs.Serialize(sw, privKey);
            //get the string from the stream
            var privKeyString = sw.ToString();

            File.WriteAllText(outPutFile, privKeyString);
            
            // var keyBytes = new ASCIIEncoding().GetBytes(privKeyString);
            // using (var outputStream = File.Open(outPutFile, FileMode.Create))
            //     outputStream.Write(keyBytes, 0, keyBytes.Length);
        }
        public void ImportPubKey(string inPutFile)
        {
            try
            {
                //get a stream from the string
                var sr = new System.IO.StringReader(File.ReadAllText(inPutFile));
                //we need a deserializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //get the object back from the stream
                _algorythm.ImportParameters((RSAParameters)xs.Deserialize(sr));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void ImportPrivateKey(string inPutFile)
        {
            try
            {
                //get a stream from the string
                var sr = new System.IO.StringReader(File.ReadAllText(inPutFile));
                //we need a deserializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //get the object back from the stream
                _algorythm.ImportParameters((RSAParameters)xs.Deserialize(sr));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void Test()
        {
            //lets take a new CSP with a new 2048 bit rsa key pair
            var csp = new RSACryptoServiceProvider(2048);

            //how to get the private key
            var privKey = csp.ExportParameters(true);

            //and the public key ...
            var pubKey = csp.ExportParameters(false);

            //converting the public key into a string representation
            string pubKeyString;
            {
                //we need some buffer
                var sw = new System.IO.StringWriter();
                //we need a serializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //serialize the key into the stream
                xs.Serialize(sw, pubKey);
                //get the string from the stream
                pubKeyString = sw.ToString();
            }

            //converting it back
            {
                //get a stream from the string
                var sr = new System.IO.StringReader(pubKeyString);
                //we need a deserializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //get the object back from the stream
                pubKey = (RSAParameters)xs.Deserialize(sr);
            }

            //conversion for the private key is no black magic either ... omitted

            //we have a public key ... let's get a new csp and load that key
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(pubKey);

            //we need some data to encrypt
            var plainTextData = "foobar";
            MessageBox.Show("Text that will be encrypted with RSA: " + plainTextData);

            //for encryption, always handle bytes...
            var bytesPlainTextData = System.Text.Encoding.Unicode.GetBytes(plainTextData);

            //apply pkcs#1.5 padding and encrypt our data 
            var bytesCypherText = csp.Encrypt(bytesPlainTextData, false);

            //we might want a string representation of our cypher text... base64 will do
            var cypherText = Convert.ToBase64String(bytesCypherText);
            MessageBox.Show("Cypher text: " + cypherText);

            /*
            * some transmission / storage / retrieval
            * 
            * and we want to decrypt our cypherText
            */

            //first, get our bytes back from the base64 string ...
            bytesCypherText = Convert.FromBase64String(cypherText);

            //we want to decrypt, therefore we need a csp and load our private key
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(privKey);

            //decrypt and strip pkcs#1.5 padding
            bytesPlainTextData = csp.Decrypt(bytesCypherText, false);

            //get our original plainText back...
            plainTextData = System.Text.Encoding.Unicode.GetString(bytesPlainTextData);

            MessageBox.Show("Plain text: " + plainTextData);
        }
    }
}