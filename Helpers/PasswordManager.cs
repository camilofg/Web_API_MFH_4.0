using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Web_API_MFH_4._0.Helpers
{
    public class PasswordManager
    {

        public String CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);

        }

        public String GenerateSHA256Hash(String input, String salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sha256HashString = new SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);

            return Convert.ToBase64String(hash);//ByteArrayToHexString(hash);
        }


        public List<string> GeneratePassword(String input, int saltSize)
        {
            List<string> saltHash = new List<string>();
            string salt = CreateSalt(saltSize);

            string pass = GenerateSHA256Hash(input, salt);

            saltHash.Add(salt);
            saltHash.Add(pass);
            return saltHash;
        }

        //HashComputer m_hashComputer = new HashComputer();

        //public bool IsPasswordMatch(string password, string salt, string hash)
        //{
        //    string finalString = password + salt;
        //    return hash == m_hashComputer.GetPasswordHashAndSalt(finalString);
        //}
    }
}