using System.Security.Cryptography;
using System.Text;

namespace CRUD.Helpers
{
    public static class CriptoHelper
    {
        public static string HashMD5(string valor)
        {
            var bytes = Encoding.ASCII.GetBytes(valor);
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(bytes);

            var ret = string.Empty;
            for (int i = 0; i < hash.Length; i++)
            {
                ret += hash[i].ToString("x2");
            }

            return ret;
        }
    }
}