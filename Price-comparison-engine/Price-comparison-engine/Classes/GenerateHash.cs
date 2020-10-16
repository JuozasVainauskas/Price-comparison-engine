using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_comparison_engine.Classes
{
    class GenerateHash
    {
        public static string CreateSalt(int dydis)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buferis = new byte[dydis];
            rng.GetBytes(buferis);
            return Convert.ToBase64String(buferis);
        }

        public static string GenerateSHA256Hash(string ivedimas, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(ivedimas + salt);
            var sha256hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}
