using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;

using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Net;

using Chilkat;

namespace ds
{
    class Fshtese
    {

        public static byte[] Desdekriptim(byte[] decryptedRSA, byte[] bytes1, byte[] dekodimiDes)
        {
            DESCryptoServiceProvider FjalaDES = new DESCryptoServiceProvider();
            FjalaDES.Mode = CipherMode.CBC;
            FjalaDES.Padding = PaddingMode.PKCS7;


            FjalaDES.Key = decryptedRSA;
            FjalaDES.IV = bytes1;

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms,
                                FjalaDES.CreateDecryptor(),
                                CryptoStreamMode.Write);
            cs.Write(dekodimiDes, 0, dekodimiDes.Length);
            cs.FlushFinalBlock();
            // cs.Close();

            byte[] plainCiphertexti = ms.ToArray();
            return plainCiphertexti;

        }
        public static void Dekriptimi(string message,bool opa)
        {
            string[] words = message.Split('.');
            var bytes = Convert.FromBase64String(words[0]);
            string user = Encoding.UTF8.GetString(bytes);
           // Console.WriteLine(user);
            byte[] bytes1 = Convert.FromBase64String(words[1]);
            //string IV = Encoding.UTF8.GetString(bytes1);
            // Console.WriteLine(IV);
            // bytes1 = Formovargun(IV);
            string path = "keys\\" + user + ".pem";
            //Console.WriteLine(path);

            if (!File.Exists(path))
            {
                string s = "Celesi " + user + " nuk ekziston";
                // Console.WriteLine("Celesi " + user + " nuk ekziston");
                
            }
            else
            {
                RSACryptoServiceProvider rsaPriv = new RSACryptoServiceProvider();
                Chilkat.PrivateKey privKey2 = new Chilkat.PrivateKey();
                privKey2.LoadPemFile(path);
                string privKeyXml = privKey2.GetXml();


                
                rsaPriv.FromXmlString(privKeyXml);
                byte[] dekodimi = Convert.FromBase64String(words[2]);
                byte[] decryptedRSA = rsaPriv.Decrypt(dekodimi, false);
                byte[] dekodimiDes = Convert.FromBase64String(words[3]);
               
               
                Console.WriteLine("Marresi: " + user);
               


                Console.WriteLine ("Mesazhi: "+ Encoding.UTF8.GetString(Desdekriptim(decryptedRSA, bytes1, dekodimiDes)));
                if (opa)
                {
                    byte[] dataToverify = Convert.FromBase64String(words[5]);
                    var Bytes12 = Convert.FromBase64String(words[4]);
                    string perdoruesi = Encoding.UTF8.GetString(Bytes12);
                    Console.WriteLine("Derguesi" + perdoruesi);
                    if (Sign.VerifySignedHash(dataToverify, Desdekriptim(decryptedRSA, bytes1, dekodimiDes), perdoruesi)) 
                    {
                        Console.WriteLine("Nenshkrami eshte valid");
                    }
                    



                }
                
                
                





            }

        }
        public static byte[] GenerateRandomByteArray(int size)
        {
            var random = new Random();
            byte[] byteArray = new byte[size];
            random.NextBytes(byteArray);
            return byteArray;
        }

        public static byte[] GenerateIv()
        {
            byte[] IV = GenerateRandomByteArray(8);
            return IV;
        }

        public static byte[] GenerateKey()
        {
            byte[] key = GenerateRandomByteArray(8);
            return key;
        }
        public static byte[] Formovargun(string fjala)
        {
            byte[] vargu = Encoding.UTF8.GetBytes(fjala);
            return vargu;
        }
        public static string Enkriptimi(string name, string message,string user)
        {
            string s = "keys\\" + name + ".pub.pem";
            byte[] bytePlaintext =
             Encoding.UTF8.GetBytes(message);

            RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();
            Chilkat.PublicKey pubKey2 = new Chilkat.PublicKey();
            pubKey2.LoadOpenSslPemFile(s);
            string pubKeyXml = pubKey2.GetXml();

            
            rsaPublic.FromXmlString(pubKeyXml);
            
            DESCryptoServiceProvider FjalaDES =
           new DESCryptoServiceProvider();
            byte[] keyRandom = GenerateKey();
            byte[] keyIV = GenerateIv();
            FjalaDES.Key = keyRandom;
            FjalaDES.IV = keyIV;
            FjalaDES.Mode = CipherMode.CBC;
            FjalaDES.Padding = PaddingMode.PKCS7;


            // FjalaDES.Key = Encoding.UTF8.GetBytes("12345678");
            // FjalaDES.IV = Encoding.UTF8.GetBytes("12345678");
            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms,
                                FjalaDES.CreateEncryptor(),
                                CryptoStreamMode.Write);
            cs.Write(bytePlaintext, 0, bytePlaintext.Length);
            cs.FlushFinalBlock();

            cs.Close();

            byte[] byteCiphertexti = ms.ToArray();



            // mesazhi i koduar nga celsi des
            string ciphertexti = Convert.ToBase64String(byteCiphertexti);
            // celsi des i koduar permes celsit rsa
            byte[] encryptedRSA = rsaPublic.Encrypt(keyRandom, false);
            string ciphertexti1 = Convert.ToBase64String(encryptedRSA);
            //Kodimi i emrit te celsit
            byte[] emriVarg = Formovargun(name);
            string ciphertexti2 = Convert.ToBase64String(emriVarg);
            //kodimi i iv
            string ciphertexti3 = Convert.ToBase64String(FjalaDES.IV);
            //pjesa e 3
            byte[] signedData = Sign.HashAndSignBytes(byteCiphertexti, user);
            string ciphertexti5 = Convert.ToBase64String(signedData);
            string ciphertexti4 = Convert.ToBase64String(Formovargun(user));


            string total = ciphertexti2 + "." + ciphertexti3 + "." + ciphertexti1 +
                "." + ciphertexti+ciphertexti4+ciphertexti5;

            // Console.WriteLine(ciphertexti);
            return total;

        }
        public static void shfaq()
        {
            Console.WriteLine("========================================================================\n");
            Console.WriteLine("Operacioni case: case (alternating,inverse,capitalize,upper,lower) teksti");
            Console.WriteLine("Operacioni vigener: vigenere (encrypt,decrypt) tekst key");
            Console.WriteLine("Operacioni foursquare: foursquare (encrypt,decrypt) tekst key1 key2");
            Console.WriteLine("Operacioni create-user: create-user user");
            Console.WriteLine("Operacioni delete-user: delete-user user");
            Console.WriteLine("Operacioni export-key: export-key (private,public) name path");
            Console.WriteLine("Operacioni import-key: import-key  name  path ");
            Console.WriteLine("Operacioni write-message: write-message: name message token file");
            Console.WriteLine("Operacioni read -message: read-message token ");
            Console.WriteLine("Operacioni log-in: user ");
            Console.WriteLine("Operacioni status token");
            Console.WriteLine("");

            Console.WriteLine("=========================================================================");
        }
        public static void writeMessage(string name, string message,string token, string file)
        {
            bool tokenV = TokenGen.TokenStatus(token);
            if (!tokenV)
            {
                Console.WriteLine("Tokeni nuk eshte i duhur ose ka skaduar");
                
            }
            else if (!File.Exists("keys\\" + name + ".pub.pem")&&tokenV)
            {
                Console.WriteLine("celesi publik " + name + " nuk ekziston");
            }
            else if (file == ""&&tokenV)

            {

                string ciphertexti = Enkriptimi(name, message,token);
                Console.WriteLine(ciphertexti);


            }
            else if (!File.Exists(file)&&tokenV)
            {
                File.Create(file);
                string ciphertexti = Enkriptimi(name, message, token);

                System.IO.File.WriteAllText(file, ciphertexti);
                Console.WriteLine("mesazhi eshte ruajtur ne fajjllin " + file);
            }
            else
            {
                string ciphertexti = Enkriptimi(name, message,token);

                System.IO.File.WriteAllText(file, ciphertexti);
                Console.WriteLine("mesazhi eshte ruajtur ne fajjllin " + file);



            }
        }
        public static Boolean Validatemessage(string message)
        {
            string[] words = message.Split('.');
            if (words.Length == 6)
                return true;
            return false;
        }
        public static void readMessage(string message)
        {
            if (Validatemessage(message) && !File.Exists(message))
            {
                Dekriptimi(message, true);




            }
            else if (!Validatemessage(message))
            {
                
                Dekriptimi(message, false);

            }
            else if (File.Exists(message) )
            {
                
                string readText = File.ReadAllText(message);
                bool fV = Validatemessage(readText);
                if (fV)
                {
                    Dekriptimi(readText, true);
                }
                else
                    Dekriptimi(readText, false);

                //Console.WriteLine("operacionet per leximin e fajllave");
                
                
            }

        }
        public static void exportKey(string type, string name, string path)
        {
            string path1 = "keys\\" + name + ".pem";
            string path2 = "keys\\" + name + ".pub.pem";
            if ((type == "private"))
            {
                if (!File.Exists(path1))
                {
                    Console.WriteLine("celesi nuk ekziston");

                }
                else if (path == "")
                {
                    //XElement file = XElement.Load(@path1);
                    Chilkat.PrivateKey pkey = new Chilkat.PrivateKey();
                    pkey.LoadPem(path1);
                    string pkeyXml;
                    // Get the private key in XML format:
                    pkeyXml = pkey.GetXml();
                    Console.WriteLine(pkeyXml);
                }
                else
                {
                    XElement file = XElement.Load(@path1);
                    //Console.WriteLine(file);
                    file.Save("keys1\\" + name + ".xml");
                    file.Save(path);
                    Console.WriteLine("celsi u ruajt ne folderin key1");


                }






            }
            else if (type == "public")
            {

                if (!File.Exists(path2))
                {
                    Console.WriteLine("celesi nuk ekziston");

                }
                else if (path == "")
                {
                    XElement file = XElement.Load(@path1);
                    Console.WriteLine(file);
                    //XElement file = XElement.Load(@path1);
                    Chilkat.PublicKey pukey = new Chilkat.PublicKey();
                    pukey.LoadOpenSslPemFile (path1);
                    string pkeyXml;
                    // Get the private key in XML format:
                    pkeyXml = pukey.GetXml();
                    Console.WriteLine(pkeyXml);
                }
                else
                {
                    XElement file = XElement.Load(@path1);
                    //Console.WriteLine(file);
                    file.Save("key1\\" + name + ".pub.xml");
                    file.Save(path);
                    Console.WriteLine("celsi u ruajt ne folderin key1");




                    //XmlDocument FILE = new XmlDocument();
                    //FILE.LoadXml(file);
                    //FILE.Save("C:\\keys1\\" + name + ".pub.xml");
                    //

                }


            }
            else
            {
                Console.WriteLine("Operacioni qe keni kerkuar nuk mund te mundesohet");
                shfaq();
            }


        }
        public static void importKey(string name, string path)
        {
            Regex obj = new Regex("^(http://|https://)");
            Regex obj1 = new Regex("(.xml|.pub.xml)$");
            if (File.Exists("C:\\keys\\" + name + ".xml") | File.Exists("C:\\keys\\" + name + ".pub.xml"))
            {
                Console.WriteLine("celesi " + name + "ekziston paraprakisht");
            }

            else if (obj.IsMatch(path))
            {

                string rt;

                WebRequest request = WebRequest.Create(path);

                WebResponse response = request.GetResponse();

                System.IO.Stream dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                rt = reader.ReadToEnd();


                // Console.WriteLine(rt);

                reader.Close();
                response.Close();

                File.WriteAllText("keys\\" + name + ".xml", rt);

                Console.WriteLine("Celesi u ruajt ne fajllin " + "keys\\" + name + ".xml");
            }
            else if (obj1.IsMatch(path))
            {

                XmlDocument FILE = new XmlDocument();
                FILE.LoadXml(path);
                Console.WriteLine();

                FILE.Save("keys\\" + name + ".xml");


            }
            else
                Console.WriteLine("Fajlli i dhene nuk eshte fajll valid");
        }
        public static void Createuser(string user)
        {

            Connectimimedb.InsertStudent(user);
            Chilkat.Global glob = new Chilkat.Global();
            Chilkat.Rsa rsa = new Chilkat.Rsa();
            glob.UnlockBundle("hELLOW");


           

            // Generate a 1024-bit key.  Chilkat RSA supports
            // key sizes ranging from 512 bits to 4096 bits.
            bool success = rsa.GenerateKey(1024);
            if (success != true)
            {
                Console.WriteLine(rsa.LastErrorText);
                return;
            }

            // Keys are exported in XML format:
            string publicKeyXml = rsa.ExportPublicKey();
           

            string privateKeyXml = rsa.ExportPrivateKey();
            

            // Save the private key in PEM format:
            Chilkat.PrivateKey privKey = new Chilkat.PrivateKey();
            success = privKey.LoadXml(privateKeyXml);
            success = privKey.SaveRsaPemFile("keys\\" + user + ".pem");
            Console.WriteLine("Eshte krijuar qelesi privat " + "keys\\" + user + ".pem");
            // Save the public key in PEM format:
            Chilkat.PublicKey pubKey = new Chilkat.PublicKey();
            success = pubKey.LoadXml(publicKeyXml);
            success = pubKey.SaveOpenSslPemFile("keys\\" + user + ".pub.pem");
            Console.WriteLine("Eshte krijuar qelesi public " + "keys\\" + user + ".pub.pem");



        }
        public static void Deleteuser(string user)
        {
            bool verify = Connectimimedb.verify(user);
            string path ="keys\\" + user + ".pem";
            string path1 = "keys\\" + user + ".pub.pem";
            

            if (File.Exists(path) && File.Exists(path1))
            {
                File.Delete(path);
                Console.WriteLine("Eshte larguar celsesi privat" + path);
                File.Delete(path1);
                Console.WriteLine("Eshte larguar celsesi public" + path1);
                Connectimimedb.DeleteStudent(user);
            }
            else if (!File.Exists(path) && File.Exists(path1))
            {
                File.Delete(path1);
                Console.WriteLine("Eshte larguar celesi public" + path1);
                Connectimimedb.DeleteStudent(user);
            }
            else
            {
                Console.WriteLine("Useri nuk ekziston");
            }
               
        }
        public static void Kontrollo(string a,string b)
        {
            int ln = b.Length;
           System.Text.StringBuilder str = new System.Text.StringBuilder(b);
            if (a == "inverse")
            {
                // Conversion according to ASCII values 
                for (int i = 0; i < ln; i++)
                {
                    if (str[i] >= 'a' && str[i] <= 'z')

                        //Convert lowercase to uppercase 
                        str[i] = (char)(str[i] - 32);

                    else if (b[i] >= 'A' && b[i] <= 'Z')

                        //Convert uppercase to lowercase 
                        str[i] = (char)(str[i] + 32);

                }
                Console.WriteLine(str);

            }
            else if (a=="alternating")
            {
                for (int i = 0; i < ln; i++)
                {
                    if (i % 2 == 0)
                    {

                        if (str[i] >= 'a' && str[i] <= 'z')

                            //Convert lowercase to uppercase 
                            str[i] = (char)(str[i] - 32);

                        else if (b[i] >= 'A' && b[i] <= 'Z')


                            str[i] = (char)(b[i]);

                    }
                    else
                    {
                        if (str[i] >= 'a' && str[i] <= 'z')

                            //Convert lowercase to uppercase 
                            str[i] = (char)(str[i]);

                        else if (b[i] >= 'A' && b[i] <= 'Z')


                            str[i] = (char)(b[i] + 32);
                    }
                }
                Console.WriteLine(str);
            }
            else if (a == "lower")
            {
                for(int i = 0; i < ln; i++)
                {

                    if (str[i] >= 'a' && str[i] <= 'z')

                        //Convert lowercase to uppercase 
                        str[i] = (char)(str[i]);

                    else if (b[i] >= 'A' && b[i] <= 'Z')


                        str[i] = (char)(b[i] + 32);
                }
                Console.WriteLine(str);
            }
            else if (a == "upper")
            {
                for(int i = 0; i < ln; i++)
                {

                    if (str[i] >= 'a' && str[i] <= 'z')

                        //Convert lowercase to uppercase 
                        str[i] = (char)(b[i] - 32);

                    else if (b[i] >= 'A' && b[i] <= 'Z')

                                str[i] = (char)(str[i]);
                    
                }
                Console.WriteLine(str);


            }
            else if (a == "capitalize")
            {
                //perdorimi i nje librarie te gatshme
                string fullText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(b);
                Console.WriteLine(fullText);

            }
            else
            {
                Console.WriteLine("keni shtypur dicka gabim");
                shfaq();
            }
            
        }
       
    }
}
