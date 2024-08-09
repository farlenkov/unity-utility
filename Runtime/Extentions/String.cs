using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UnityUtility
{
    public static class StringUtilityExt
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string EncryptAES(this string data, string key)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(data);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptAES(this string data, string key)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(data);

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream(buffer))
                {
                    using (var cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    
        public static string ToBase64(this string originalString, bool skipEmpty = true)
        {
            if (originalString == null)
                return null;

            if (skipEmpty && string.IsNullOrEmpty(originalString)) // ??
                return originalString;

            var bytesToEncode = Encoding.UTF8.GetBytes(originalString);
            return Convert.ToBase64String(bytesToEncode);
        }

        public static string FromBase64(this string base64String, bool skipEmpty = true)
        {
            if (base64String == null)
                return null;

            if (skipEmpty && string.IsNullOrEmpty(base64String)) // ??
                return base64String;

            var bytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(bytes);
        }
    
        public static string ToMD5(this string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            var md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.Default.GetBytes(originalString));
            var strBuilder = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
                strBuilder.Append(result[i].ToString("x2"));

            return strBuilder.ToString();
        }
    }
}
