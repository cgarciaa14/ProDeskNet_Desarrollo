#region TRACKERS
//BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProdeskNet.Criptografia
{
    public class Parameters
    {
        private string _Key = "0e64982d-5cb0-478a-a44a-d387e8979115";
        private const int byteLength = 8;
        internal string transformParameters(string plainText, string password, int keySize, int vectorSize, out byte[] byteKey, out byte[] byteVector)
        {
            var key = new Rfc2898DeriveBytes(_Key, Encoding.ASCII.GetBytes(password));
            string _result = string.Empty;
            if (plainText == null || plainText == string.Empty)
            {
                _result += "\nParameter plainText cannot be null or empty";
            }
            if (password == null || password == string.Empty)
            {
                _result += "\nThe Password expected";
                byteVector = null;
            }
            else
            {
                byteVector = key.GetBytes(vectorSize / byteLength);
            }
            byteKey = key.GetBytes(keySize / byteLength);
            return _result;
        }

        internal string transformParameters(string plainText, string password, int keySize, int vectorSize, out byte[] bytePlainText, out byte[] byteKey, out byte[] byteVector)
        {
            string _result = string.Empty;
            _result = transformParameters(plainText, password, keySize, vectorSize, out byteKey, out byteVector);
            bytePlainText = null;
            if (_result == string.Empty)
            {
                bytePlainText = Encoding.UTF8.GetBytes(plainText);
            }
            return _result;
        }
    }
}
