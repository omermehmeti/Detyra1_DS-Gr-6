using System;
using System.Collections.Generic;
using System.Text;

namespace ds
{
    class GjeneroSalt
    {

        public static String KrijoSalt(int madhesia)
        {

            var nrRastesishem = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var baferi = new byte[madhesia];
            nrRastesishem.GetBytes(baferi);
            return Convert.ToBase64String(baferi);
        }
        public static String GjeneroSHA256Hashin(String hyrja, String salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(hyrja + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring =
                new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
       
        
    }
}
