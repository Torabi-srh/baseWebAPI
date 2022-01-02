using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace baseWebAPI.Resolvers
{

    public static class Cryptography
    {
        #region Settings 
        private static int _iterations = 2;
        private static int _keySize = 256;
        private static string _hash = "SHA1";
        private static string _salt = "Alephbas4awl34kjq";
        private static string _vector = "8947az34awl34kjq";
        #endregion

        public static string Encrypt(string passtext, string passPhrase)
        {
            try
            {

                string functionReturnValue = null;
                byte[] initVectorBytes = null;
                initVectorBytes = Encoding.ASCII.GetBytes(_vector);
                byte[] saltValueBytes = null;
                saltValueBytes = Encoding.ASCII.GetBytes(_salt);
                byte[] plainTextBytes = null;
                plainTextBytes = Encoding.UTF8.GetBytes(passtext);
                PasswordDeriveBytes password = default(PasswordDeriveBytes);
                password = new PasswordDeriveBytes(passPhrase, saltValueBytes, _hash, _iterations);
                byte[] keyBytes = null;
                keyBytes = password.GetBytes(_keySize / 8);
                RijndaelManaged symmetricKey = default(RijndaelManaged);
                symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = default(ICryptoTransform);
                encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = default(MemoryStream);
                memoryStream = new MemoryStream();
                CryptoStream cryptoStream = default(CryptoStream);
                cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = null;
                cipherTextBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                string cipherText = null;
                cipherText = Convert.ToBase64String(cipherTextBytes);
                functionReturnValue = cipherText;
                return functionReturnValue;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string Decrypt(string cipherText, string passPhrase)
        {
            try
            {
                string functionReturnValue = null;
                byte[] initVectorBytes = null;
                initVectorBytes = Encoding.ASCII.GetBytes(_vector);
                byte[] saltValueBytes = null;
                saltValueBytes = Encoding.ASCII.GetBytes(_salt);
                byte[] cipherTextBytes = null;
                cipherTextBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes password = default(PasswordDeriveBytes);
                password = new PasswordDeriveBytes(passPhrase, saltValueBytes, _hash, _iterations);
                byte[] keyBytes = null;
                keyBytes = password.GetBytes(_keySize / 8);
                RijndaelManaged symmetricKey = default(RijndaelManaged);
                symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = default(ICryptoTransform);
                decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = default(MemoryStream);
                memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = default(CryptoStream);
                cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = null;
                plainTextBytes = new byte[cipherTextBytes.Length + 1];
                int decryptedByteCount = 0;
                decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                string plainText = null;
                plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                functionReturnValue = plainText;
                return functionReturnValue;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
