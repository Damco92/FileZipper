using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FileArchiver.Common.Helpers
{
    public static class HashingHelper
    {
        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }
        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }

        public static byte[] GetDomainPasswordByteArray(string domainPassword)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(domainPassword);
            return SHA256.Create().ComputeHash(passwordBytes);
        }

        public static bool IsRegexForDocumentPasswordMatch(string documentPassword)
        {
            Regex reg = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$");
            Match match = reg.Match(documentPassword);
            return match.Success;
        }

       public static bool IsTextFile(string fileName)
        {
            Regex reg = new Regex("^.+[.]txt");
            Match match = reg.Match(fileName);
            return match.Success;
        }

        public static bool IsPdfFile(string fileName)
        {
            Regex reg = new Regex("^.+[.]pdf");
            Match match = reg.Match(fileName);
            return match.Success;
        }

        public static bool IsImageFile(string fileName)
        {
            Regex reg = new Regex("^.+[.]png");
            Match match = reg.Match(fileName);
            return match.Success;
        }

        public static bool IsJsonFile(string fileName)
        {
            Regex reg = new Regex("^.+[.]json");
            Match match = reg.Match(fileName);
            return match.Success;
        }

        public static bool IsJPGFile(string fileName)
        {
            Regex reg = new Regex("^.+[.]jpg");
            Match match = reg.Match(fileName);
            return match.Success;
        }

        public static bool IsXMLFile(string fileName)
        {
            Regex reg = new Regex("^.+[.]xml");
            Match match = reg.Match(fileName);
            return match.Success;
        }

        public static bool IsZipFile(string fileName)
        {
            Regex reg = new Regex("^.+[.]zip");
            Match match = reg.Match(fileName);
            return match.Success;
        }

        public static bool Is7ZipFile(string fileName)
        {
            Regex reg = new Regex("^.+[.]7z");
            Match match = reg.Match(fileName);
            return match.Success;
        }

        public static bool DoPasswordsMatch(string password, string confirmedPassword)
        {
            return password.Equals(confirmedPassword);
        }
    }
}
