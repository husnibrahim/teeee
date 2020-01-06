using System;
using System.IO;
using System.Security.Cryptography;

namespace AesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter text that need to be encrypted:");
            string data = Console.ReadLine();
            EncryptedAesManaged(data);
            Console.ReadLine();

        }
        static void EncryptedAesManaged(string raw)
        {
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    byte[] encrypted =Encrypt(raw, aes.Key, aes.IV);
                    Console.WriteLine("Encrypted Message :"+System.Text.Encoding.UTF8.GetString(encrypted));
                    string decrypted = Decrypt(encrypted,aes.Key,aes.IV);
                    Console.WriteLine("Decrypted text :"+decrypted);
                }
                //byte[]encrypted=Encrypt(raw\
            }
            catch (Exception e)
            { 
            
            }
        }
        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (AesManaged aes = new AesManaged()) 
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Key,IV);
                using (MemoryStream ms = new MemoryStream())
                {
                        using (CryptoStream cs = new CryptoStream (ms,encryptor, CryptoStreamMode.Write))
                        {
                        using (StreamWriter sw = new StreamWriter(cs)) 
                        sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }
        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            plaintext = reader.ReadToEnd();
                        }
                        }
                }
            }
            return plaintext;
        }
    }
}
