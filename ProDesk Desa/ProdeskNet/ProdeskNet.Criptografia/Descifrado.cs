#region TRACKERS
//BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
#endregion

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ProdeskNet.Criptografia
{
    public class Descifrado
    {
        public string DecryptString(string plainText, string password)
        {
            string _result;
            byte[] byteKey;
            byte[] byteVector;
            string paramsMessage;
            ICryptoTransform decryptor;

            using (RijndaelManaged RijndaelMAlgorythm = new RijndaelManaged())
            {
                paramsMessage = new Parameters().transformParameters(plainText, password, RijndaelMAlgorythm.KeySize, RijndaelMAlgorythm.BlockSize, out byteKey, out byteVector);
                if (!(paramsMessage == string.Empty))
                {
                    throw new ArgumentNullException(paramsMessage);
                }
                RijndaelMAlgorythm.Key = byteKey;
                RijndaelMAlgorythm.IV = byteVector;
                decryptor = RijndaelMAlgorythm.CreateDecryptor(RijndaelMAlgorythm.Key, RijndaelMAlgorythm.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(plainText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            _result = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return _result;
        }
    }
}
