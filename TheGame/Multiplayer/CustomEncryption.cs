﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TheGame.Multiplayer
{
    public static class CustomEncryption
    {
        private static string Encrypt(string text, string key)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }

                }
            }
        }

        private static string Decrypt(string cipher, string key)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }

        public static string GenerateToken(string username, string authToken, string refreshToken)
        {
            string usernameEncrypted = Encrypt(username, authToken);
            return usernameEncrypted + "," + authToken + "," + refreshToken;
        }

        public static string DecryptPassword(string password)
        {
            string key = DateTime.UtcNow.ToString("MM-dd-yyyy:HH_mm");
            return Decrypt(password, key);
        }

        public static string EncryptPassword(string password)
        {
            string key = DateTime.UtcNow.ToString("MM-dd-yyyy:HH_mm");
            return Encrypt(password, key);
        }

        public static string EncryptPasswordForDatabase(string username, string password)
        {
            return Encrypt(password, username);
        }

        public static string DecryptPasswordFromHeader(string username, string password)
        {
            return Decrypt(password, username);
        }

        public static string EncryptPasswordForHeader(string username, string password)
        {
            return Encrypt(password, username);
        }


    }
}
