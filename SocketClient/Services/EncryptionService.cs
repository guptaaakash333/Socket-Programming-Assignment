using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient.Services
{
    public static class EncryptionService
    {
        private static readonly byte[] Key = 
        Encoding.UTF8.GetBytes("12345678901234567890123456789012");

        private static readonly byte[] IV =
            Encoding.UTF8.GetBytes("1234567890123456");

        public static string Encrypt(string plainText)
        {
            using Aes aes = Aes.Create();

            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform encryptor = aes.CreateEncryptor();

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            return Convert.ToBase64String(
                encryptedBytes);
        }

        public static string Decrypt(string cipherText)
        {
            using Aes aes = Aes.Create();

            aes.Key = Key;
            aes.IV = IV;

            ICryptoTransform decryptor = aes.CreateDecryptor();

            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(decryptedBytes);

        }



    }
}
