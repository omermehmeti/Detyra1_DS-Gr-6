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

namespace ds
{
    class Class1
    {
         
        public static byte[] Desdekriptim(byte[] decryptedRSA,byte [] bytes1,byte [] dekodimiDes)
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
        public static string Dekriptimi(string message)
        {
            string[] words = message.Split('.');
            var bytes = Convert.FromBase64String(words[0]);
            string user = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(user);
            byte[] bytes1 = Convert.FromBase64String(words[1]);
            //string IV = Encoding.UTF8.GetString(bytes1);
            // Console.WriteLine(IV);
            // bytes1 = Formovargun(IV);
            string path = "C:\\keys\\" + user + ".xml";
            Console.WriteLine(path);

            if (!File.Exists(path))
            {
                string s = "Celesi " + user + " nuk ekziston";
               // Console.WriteLine("Celesi " + user + " nuk ekziston");
                return s;
            }
            else
            {
                RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();

                StreamReader sr = File.OpenText(path);
                string rsaxml = sr.ReadToEnd();
                sr.Close();
                rsaPublic.FromXmlString(rsaxml);
                byte[] dekodimi = Convert.FromBase64String(words[2]);
                byte[] decryptedRSA = rsaPublic.Decrypt(dekodimi, false);
                byte[] dekodimiDes = Convert.FromBase64String(words[3]);
                
                string mesazhi = Encoding.UTF8.GetString(Desdekriptim(decryptedRSA,bytes1,dekodimiDes));
                Console.WriteLine("Marresi: "+user);
                return mesazhi;





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
        public static string Enkriptimi(string name,string message)
        {
            string s = "C:\\keys\\" + name + ".pub.xml";
            byte[] bytePlaintext =
             Encoding.UTF8.GetBytes(message);
            
            RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();

            StreamReader sr = File.OpenText(s);
            string rsaxml = sr.ReadToEnd();
            sr.Close();
            rsaPublic.FromXmlString(rsaxml);
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
           

            string total = ciphertexti2 + "." + ciphertexti3 + "." + ciphertexti1 +
                "." + ciphertexti;

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
            Console.WriteLine("Operacioni write-message: write-message: name message file");
            Console.WriteLine("Operacioni read -message: read-message ");
            Console.WriteLine("Operacioni log-in: user pss");
            Console.WriteLine("Operacioni ");
            Console.WriteLine("");
            
            Console.WriteLine("=========================================================================");
        }
        public static void writeMessage(string name,string message,string file)
        {
            if (!File.Exists("C:\\keys\\" +name + ".pub.xml"))
            {
                Console.WriteLine("celesi publik " + name + " nuk ekziston");
            }
            else if (file == "")

            {
                
                string ciphertexti = Enkriptimi(name, message);
                Console.WriteLine(ciphertexti);
                

            }
            else if (!File.Exists(file))
            {
                Console.WriteLine("Fajlli qe keni dhene nuk ekziston");
            }
            else
            {
                string ciphertexti = Enkriptimi(name, message);

                System.IO.File.WriteAllText(file, ciphertexti);
                Console.WriteLine("mesazhi eshte ruajtur ne fajjllin " + file);



            }
        }
        public static Boolean Validatemessage(string message)
        {
            string[] words = message.Split('.');
            if (words.Length == 4)
                return true;
            return false;
        }
        public static void readMessage(string message )
        {
            if (Validatemessage(message)&&!File.Exists(message))
            {
                Console.WriteLine("Mesazhi "+Dekriptimi(message));
                



            }
            else if (File.Exists(message))
            {
                Console.WriteLine("operacionet per leximin e fajllave");
                string readText = File.ReadAllText(message);
                Console.WriteLine("Mesazhi "+Dekriptimi(readText));

            }
            else
            {
                Console.WriteLine("Mesazhi nuk eshte valid");
            }

        }
        public static void exportKey(string type,string name,string path)
        {
            string path1 = "C:\\keys\\" + name + ".xml";
            string path2 = "C:\\keys\\" + name+ ".pub.xml";
           if (  (type == "private"))
            {
                if(!File.Exists(path1))
                {
                    Console.WriteLine("celesi nuk ekziston");

                }
                else if (path == "")
                {
                    XElement file = XElement.Load(@path1);
                    Console.WriteLine(file);
                }
                else
                {
                    XmlDocument FILE = new XmlDocument();
                    FILE.LoadXml(path1);
                    FILE.Save("C:\\keys1\\" + name + ".xml");

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
                }
                else
                {
                    XmlDocument FILE = new XmlDocument();
                    FILE.LoadXml(path1);
                    FILE.Save("C:\\keys1\\" + name + ".pub.xml");

                }


            }
           

        }
        public static void importKey(string name,string path)
        {
            Regex obj = new Regex("^(http://|https://)");
            Regex obj1 = new Regex("(.xml|.pub.xml)$");
            if(File.Exists("C:\\keys\\" + name + ".xml")|File.Exists("C:\\keys\\" + name + ".pub.xml"))
            {
                Console.WriteLine("celesi " + name + "ekziston paraprakisht");
            }

            else if (obj.IsMatch(path))
            {
               
                string rt;

                WebRequest request = WebRequest.Create(path);

                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                rt = reader.ReadToEnd();
                

               // Console.WriteLine(rt);

                reader.Close();
                response.Close();

                File.WriteAllText("C:\\keys\\"+name+".xml", rt );

                Console.WriteLine("Celesi u ruajt ne fajllin " + "C:\\keys\\" + name + ".xml");
            }
            else if (obj1.IsMatch(path))
            {
                 
                XmlDocument FILE = new XmlDocument();
                FILE.LoadXml(path);
                Console.WriteLine();
                
                FILE.Save("C:\\keys\\" + name + ".xml");
                

            }
            else
                Console.WriteLine("Fajlli i dhene nuk eshte fajll valid");
        }
        public static void Createuser(string user)
        {
            /*
            string path = "C:\\keys\\"+user+".xml";
            XmlTextWriter writer = new XmlTextWriter(path, null);*/
            //stream to save the keys
            FileStream fs = null;
            StreamWriter sw = null;

            //create RSA provider
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
            try
            {
                String privateKeyPath= "C:\\keys\\"+user+".xml";
                //save private key
                fs = new FileStream(privateKeyPath, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(rsa.ToXmlString(true));
                sw.Flush();
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
                Console.WriteLine("Eshte krijuar celsesi privat " +
                    "C:\\keys\\" + user + ".xml");
            }
            try
            {
                string publicKeyPath = "C:\\keys\\" + user + ".pub.xml";
                //save public key
                fs = new FileStream(publicKeyPath, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.Write(rsa.ToXmlString(false));
                sw.Flush();
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
                Console.WriteLine("Eshte krijuar celsesi public " +
                    "C:\\keys\\" + user + ".pub.xml");
            }
            rsa.Clear();
        }
        public static void Deleteuser(string user)
        {
            string path ="C:\\keys\\" + user + ".xml";
            string path1 = "C:\\keys\\" + user + ".pub.xml";

            if (File.Exists(path) && File.Exists(path1))
            {
                File.Delete(path);
                Console.WriteLine("Eshte larguar celsesi privat" + path);
                File.Delete(path1);
                Console.WriteLine("Eshte larguar celsesi public" + path1);
            }
            else if (!File.Exists(path) && File.Exists(path1))
            {
                File.Delete(path1);
                Console.WriteLine("Eshte larguar celesi public" + path1);
            }
            else
                Console.WriteLine("celesi nuk ekziston");
        }
        public static void Kontrollo(string a,string b)
        {
            int ln = b.Length;
            StringBuilder str = new StringBuilder(b);
            if (a == "inverse")
            {
                // Conversion according to ASCII values 
                for (int i = 0; i < ln; i++)
                {
                    if (str[i] >= 'a' && str[i] <= 'z')

                        //Convert lowercase to uppercase 
                        str[i] = (char)(str[i] - 32);

                    else if (a[i] >= 'A' && a[i] <= 'Z')

                        //Convert uppercase to lowercase 
                        str[i] = (char)(a[i] + 32);

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

                        else if (a[i] >= 'A' && a[i] <= 'Z')


                            str[i] = (char)(a[i]);

                    }
                    else
                    {
                        if (str[i] >= 'a' && str[i] <= 'z')

                            //Convert lowercase to uppercase 
                            str[i] = (char)(str[i]);

                        else if (a[i] >= 'A' && a[i] <= 'Z')


                            str[i] = (char)(a[i] + 32);
                    }
                }
                Console.WriteLine(str);
            }
            else if (b == "lower")
            {
                for(int i = 0; i < ln; i++)
                {

                    if (str[i] >= 'a' && str[i] <= 'z')

                        //Convert lowercase to uppercase 
                        str[i] = (char)(str[i]);

                    else if (a[i] >= 'A' && a[i] <= 'Z')


                        str[i] = (char)(a[i] + 32);
                }
                Console.WriteLine(str);
            }
            else if (b == "upper")
            {
                for(int i = 0; i < ln; i++)
                {

                    if (str[i] >= 'a' && str[i] <= 'z')

                        //Convert lowercase to uppercase 
                        str[i] = (char)(str[i]);

                    else if (a[i] >= 'A' && a[i] <= 'Z')


                        str[i] = (char)(a[i] + 32);
                }
                Console.WriteLine(str);


            }
            else if (b == "capitalize")
            {
                //perdorimi i nje librarie te gatshme
                string fullText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(a);
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
