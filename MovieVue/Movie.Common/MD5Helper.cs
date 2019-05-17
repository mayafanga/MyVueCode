using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Common
{
    public static class MD5Helper
    {
        public static string CalcMD5(this string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return CalcMD5(bytes);
        }
        public static string CalcMD5(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(bytes);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" + computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }
        public static string CalcMD5(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(stream);
                string result = "";
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    result += computeBytes[i].ToString("X").Length == 1 ? "0" + computeBytes[i].ToString("X") : computeBytes[i].ToString("X");
                }
                return result;
            }
        }
        public static string CreateVerifyCode(int length)
        {
            StringBuilder stringBuilder = new StringBuilder();
            char[] data = { 'A', 'C', 'D', 'E', 'F', 'H', 'K', 'M', 'N', 'R', 'S', 'T', 'W', 'X', 'Y' };
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(data.Length);
                char c = data[index];
                stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }

    }
}
