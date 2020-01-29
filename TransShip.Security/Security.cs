using System;
using System.Security.Cryptography;
using System.Text;

namespace TransShip.Security {
    public static class Security
    {
        public static string GenerateKey(SymmetricAlgorithm algorithm, int keySize)
        {
            if (!algorithm.ValidKeySize(keySize)) throw new ArgumentException("Invalid key size");
            algorithm.KeySize = keySize;
            algorithm.GenerateKey();
            return Convert.ToBase64String(algorithm.Key);
        }

        public static string Encrypt(string message, SymmetricAlgorithm algorithm, string key)
        {
            algorithm.Key = Convert.FromBase64String(key);
            algorithm.Mode = CipherMode.ECB;
            var encryptor = algorithm.CreateEncryptor();
            var data = Encoding.Unicode.GetBytes(message);
            var dataEncrypted = encryptor.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(dataEncrypted);
        }

        public static string Decrypt(string message, SymmetricAlgorithm algorithm, string key)
        {
            algorithm.Key = Convert.FromBase64String(key);
            algorithm.Mode = CipherMode.ECB;
            var decryptor = algorithm.CreateDecryptor();
            var data = Convert.FromBase64String(message);
            var dataDecrypted = decryptor.TransformFinalBlock(data, 0, data.Length);
            return Encoding.Unicode.GetString(dataDecrypted);
        }
    }
}
