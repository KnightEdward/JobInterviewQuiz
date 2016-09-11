using System.Security.Cryptography;
using System.Text;

namespace JobInterviewQuiz.Security
{
    public class SHA512Encrypter
    {
        /// <summary>
        /// Converts an array of bytes into a hex sha512 string.
        /// </summary>
        /// <param name="temp">The temporary string to be hashed</param>
        /// <returns>The sha512 hex string</returns>
        public static string Encrypt(string what)
        {
            SHA512 shaM = new SHA512Managed();
            byte[] hash = shaM.ComputeHash(Encoding.ASCII.GetBytes(what));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
    }
}