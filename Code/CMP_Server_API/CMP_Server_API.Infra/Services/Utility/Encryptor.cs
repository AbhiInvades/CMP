using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CMP_Server_API.CMP_Server_API.Infra.Services.Utility
{
    public class Encryptor : IEncryptor
    {
        private SHA256 sha;
        public string encrypt(string data)
        {
            return string.Join(",",sha.ComputeHash(Encoding.UTF8.GetBytes(data)).ToString());
        }

        public Encryptor() 
        {
            this.sha = SHA256.Create();
        }

        public string decrypt(string data) 
        {
            string[] list = data.Split(',');
            byte[] bytes = new byte[list.Length];
            for(int i = 0;i<list.Length;i++)
            {
                bytes[i] = byte.Parse(list[i]); 
            }
            return "";
        }
    }
}
