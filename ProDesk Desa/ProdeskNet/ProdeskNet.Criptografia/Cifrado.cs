#region TRACKERS
//BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
#endregion

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ProdeskNet.Criptografia
{
    public class Cifrado
    {
        public string EncryptString(string plainText, string password)
        {
            string _result = string.Empty;
            byte[] byteKey;
            byte[] byteVector;
            string paramsMessage;
            ICryptoTransform encryptor;
            using (RijndaelManaged RijndaelMAlgorythm = new RijndaelManaged())
            {
                paramsMessage = new Parameters().transformParameters(plainText, password, RijndaelMAlgorythm.KeySize, RijndaelMAlgorythm.BlockSize, out byteKey, out byteVector);
                if (!(paramsMessage == string.Empty))
                {
                    throw new ArgumentNullException(paramsMessage);
                }

                RijndaelMAlgorythm.Key = byteKey;
                RijndaelMAlgorythm.IV = byteVector;
                encryptor = RijndaelMAlgorythm.CreateEncryptor(RijndaelMAlgorythm.Key, RijndaelMAlgorythm.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    _result = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            return _result;
        }
    }
}
