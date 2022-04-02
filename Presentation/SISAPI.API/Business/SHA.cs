using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SISAPI.Business
{
    public class SHA
    {
        public string Encrypt(string password)
        {
            SHA256 sha = SHA256.Create();
            byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte byt in data)
            {
                stringBuilder.Append(byt.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
