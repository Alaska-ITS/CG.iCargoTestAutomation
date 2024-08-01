using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace iCargoUIAutomation
{
    public class KeyVault
    {
        private readonly string _keyVaultUri;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _tenantId;
        private readonly string _cccUsernameSecretName;
        private readonly string _cccPasswordSecretName;
        private readonly string _cgodgUsernameSecretName;
        private readonly string _cgodgPasswordSecretName;

        public KeyVault()
        {
            var configuration = LoadConfiguration();
            _keyVaultUri = configuration["KeyVaultUri"];
            _clientId = configuration["ClientId"];
            _clientSecret = configuration["ClientSecret"];
            _tenantId = configuration["TenantId"];
            _cccUsernameSecretName = configuration["CCCSecretUsername"];
            _cccPasswordSecretName = configuration["CCCSecretPassword"];
            _cgodgUsernameSecretName = configuration["CGODGSecretUsername"];
            _cgodgPasswordSecretName = configuration["CGODGSecretPassword"];
        }

        private IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        public Dictionary<string, string> GetSecrets()
        {
            var client = new SecretClient(new Uri(_keyVaultUri), new ClientSecretCredential(_tenantId, _clientId, _clientSecret));
            //var client = new SecretClient(new Uri(_keyVaultUri), new DefaultAzureCredential());
            //var client = new SecretClient(new Uri(_keyVaultUri), new ManagedIdentityCredential());

            // Get Secrets using CG.Common.DependencyInjection without using DefaultAzureCredential , ClientSecretCredential, ManagedIdentityCredential and EnvironmentCredential
            //var client = new SecretClient(new Uri(_keyVaultUri), new CG.Common.DependencyInjection.KeyVaultCredential());

            var secrets = new Dictionary<string, string>();

            KeyVaultSecret cccUsername = client.GetSecret(_cccUsernameSecretName);
            secrets.Add("CCC_Username", cccUsername.Value);

            KeyVaultSecret cccPassword = client.GetSecret(_cccPasswordSecretName);
            secrets.Add("CCC_Password", cccPassword.Value);

            KeyVaultSecret cgodgUsername = client.GetSecret(_cgodgUsernameSecretName);
            secrets.Add("CGODG_Username", cgodgUsername.Value);

            KeyVaultSecret cgodgPassword = client.GetSecret(_cgodgPasswordSecretName);
            secrets.Add("CGODG_Password", cgodgPassword.Value);

            return secrets;
        }
    }
}

//using System;
//using System.Text;
//using System.Security.Cryptography;
//using System.IO;
//using System.Linq;
//using Microsoft.Extensions.Configuration;

//namespace iCargoUIAutomation
//{
//    public class ConfigurationService
//    {
//        private readonly IConfiguration _configuration;

//        public ConfigurationService()
//        {
//            // Load appsettings.json and other configuration files as needed
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

//            _configuration = builder.Build();
//        }

//        public string GetDecryptedValue(string key, string passPhrase)
//        {
//            string encryptedValue = _configuration[key];
//            if (string.IsNullOrEmpty(encryptedValue))
//            {
//                throw new ArgumentException($"Key '{key}' not found in appsettings.json");
//            }

//            try
//            {
//                return StringCipher.Decrypt(encryptedValue, passPhrase);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Failed to decrypt value for key '{key}'", ex);
//            }
//        }
//    }

//    public static class StringCipher
//    {
//        private const int Keysize = 256;
//        private const int DerivationIterations = 1000;

//        public static string Encrypt(string plainText, string passPhrase)
//        {
//            var saltStringBytes = Generate256BitsOfRandomEntropy();
//            var ivStringBytes = Generate128BitsOfRandomEntropy(); // Generate 16 bytes IV for AES
//            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
//            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
//            {
//                var keyBytes = password.GetBytes(Keysize / 8);
//                using (var symmetricKey = new RijndaelManaged())
//                {
//                    symmetricKey.BlockSize = 128; // AES block size is 128 bits
//                    symmetricKey.Mode = CipherMode.CBC;
//                    symmetricKey.Padding = PaddingMode.PKCS7;
//                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
//                    {
//                        using (var memoryStream = new MemoryStream())
//                        {
//                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
//                            {
//                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
//                                cryptoStream.FlushFinalBlock();
//                                var cipherTextBytes = saltStringBytes;
//                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
//                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
//                                memoryStream.Close();
//                                cryptoStream.Close();
//                                return Convert.ToBase64String(cipherTextBytes);
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        public static string Decrypt(string cipherText, string passPhrase)
//        {
//            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
//            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
//            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(16).ToArray(); // Extract IV from cipher text
//            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) + 16).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) + 16)).ToArray();

//            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
//            {
//                var keyBytes = password.GetBytes(Keysize / 8);
//                using (var symmetricKey = new RijndaelManaged())
//                {
//                    symmetricKey.BlockSize = 128;
//                    symmetricKey.Mode = CipherMode.CBC;
//                    symmetricKey.Padding = PaddingMode.PKCS7;
//                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
//                    {
//                        using (var memoryStream = new MemoryStream(cipherTextBytes))
//                        {
//                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
//                            using (var streamReader = new StreamReader(cryptoStream, Encoding.UTF8))
//                            {
//                                return streamReader.ReadToEnd();
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        private static byte[] Generate256BitsOfRandomEntropy()
//        {
//            var randomBytes = new byte[32];
//            using (var rngCsp = new RNGCryptoServiceProvider())
//            {
//                rngCsp.GetBytes(randomBytes);
//            }
//            return randomBytes;
//        }

//        private static byte[] Generate128BitsOfRandomEntropy()
//        {
//            var randomBytes = new byte[16]; // 16 bytes for 128 bits
//            using (var rngCsp = new RNGCryptoServiceProvider())
//            {
//                rngCsp.GetBytes(randomBytes);
//            }
//            return randomBytes;
//        }
//    }
//}
