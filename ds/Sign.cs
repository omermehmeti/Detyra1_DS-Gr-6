using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ds
{
    class Sign
    {
      
        public static byte[] HashAndSignBytes(byte[] DataToSign, string user)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the
                // key from RSAParameters.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                Chilkat.PrivateKey pkey = new Chilkat.PrivateKey();

                // Load the private key from an RSA PEM file:
                bool success = pkey.LoadPemFile("C:\\Users\\Lenovo\\Desktop\\Detyra1_DS-Gr-6-master\\ds\\bin\\Debug\\netcoreapp3.0\\keys\\" + user + ".pem");
                string pkeyXml;
                // Get the private key in XML format:
                pkeyXml = pkey.GetXml();
                RSAalg.FromXmlString(pkeyXml);

                // Hash and sign the data. Pass a new instance of SHA1CryptoServiceProvider
                // to specify the use of SHA1 for hashing.
                return RSAalg.SignData(DataToSign, new SHA1CryptoServiceProvider());
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
        public static bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData,string user )
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the
                // key from RSAParameters.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                Chilkat.PublicKey pukey = new Chilkat.PublicKey();

                // Load the private key from an RSA PEM file:
                bool success = pukey.LoadOpenSslPemFile("C:\\Users\\Lenovo\\Desktop\\Detyra1_DS-Gr-6-master\\ds\\bin\\Debug\\netcoreapp3.0\\keys\\" + user + ".pub.pem");
                if (!success)
                {
                    Console.WriteLine("Celsi publik i perdoruesit" + user + "nuk gjendet");
                }
                string pkeyXml;
                // Get the private key in XML format:
                pkeyXml = pukey.GetXml();
                RSAalg.FromXmlString(pkeyXml);



                // Verify the data using the signature.  Pass a new instance of SHA1CryptoServiceProvider
                // to specify the use of SHA1 for hashing.
                return RSAalg.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
    }
}
