using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.HashAlgoritm
{
    public static class HashAlgoritm
    {
        public static string Sha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                string key = "{29FBC754-293C-4085-A055-D7111B3D66B4}";
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData+ key));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
