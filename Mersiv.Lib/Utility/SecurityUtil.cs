using System;
using System.Security.Cryptography;
using System.Text;

namespace Mersiv.Lib.Utility
{
    public class SecurityUtil
    {

        private static int SALT_SIZE = 20; //length of salt string
        private static int HASH_SIZE = 24; //number of bytes generated

        public class EncryptedPassword
        {
            public string Password { get; set; }
            public string PasswordSalt { get; set; }
        }

        public static string GenerateSalt(int saltSize)
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] byteArray = new Byte[saltSize];
            rngCryptoServiceProvider.GetBytes(byteArray);
            return Convert.ToBase64String(byteArray);
        }

        public static EncryptedPassword GenerateEncryptedPassword(string password)
        {
            return SecurityUtil.GenerateEncryptedPassword(password, GenerateSalt(SecurityUtil.SALT_SIZE));
        }

        public static EncryptedPassword GenerateEncryptedPassword(string password, string passwordSalt)
        {
            //
            // Reference: http://msdn.microsoft.com/en-us/library/system.security.cryptography.rfc2898derivebytes.aspx
            //

            byte[] bytes = new byte[passwordSalt.Length * sizeof(char)];
            System.Buffer.BlockCopy(passwordSalt.ToCharArray(), 0, bytes, 0, bytes.Length);

            EncryptedPassword encryptedPassword = new EncryptedPassword();
            Rfc2898DeriveBytes saltedHash = new Rfc2898DeriveBytes(password, bytes, 1000);
            encryptedPassword.Password = Convert.ToBase64String(saltedHash.GetBytes(HASH_SIZE));
            encryptedPassword.PasswordSalt = passwordSalt;

            return encryptedPassword;
        }

        public static string GenerateRandomPassword(int length)
        {
            // http://stackoverflow.com/questions/1122483/c-sharp-random-string-generator
            // http://stackoverflow.com/questions/54991/generating-random-passwords
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < length; i++)
            {
                sb.Append(valid[random.Next(valid.Length)]);
            }
            return sb.ToString();
        }

    }
}
