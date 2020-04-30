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
        public static void writeMessage(string name,string message,string file)
        {
            if (!File.Exists("C:\\keys\\" +name + ".pub.xml"))
            {
                Console.WriteLine("celesi " + name + "nuk ekziston");
            }
            else if (file == null)

            {
                string s = "C:\\keys\\" + name + ".pub.xml";
                byte[] bytePlaintext =
                 Encoding.UTF8.GetBytes(message);
                XmlDocument doc = new XmlDocument();
                doc.Load(s);
                string k = doc.ToString();
                RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();
                
                
                DESCryptoServiceProvider FjalaDES =
                new DESCryptoServiceProvider();
                FjalaDES.Key = Encoding.UTF8.GetBytes("12345678");
                FjalaDES.IV = Encoding.UTF8.GetBytes("12345678");
                MemoryStream ms = new MemoryStream();

                CryptoStream cs = new CryptoStream(ms,
                                    FjalaDES.CreateEncryptor(),
                                    CryptoStreamMode.Write);
                cs.Write(bytePlaintext, 0, bytePlaintext.Length);
                cs.Close();

                byte[] byteCiphertexti = ms.ToArray();
                byte[] encryptedRSA = rsaPublic.Encrypt(byteCiphertexti, false);
               // string EncryptedResult = Encoding.Default.GetString(encryptedRSA);

                string ciphertexti=Convert.ToBase64String(encryptedRSA);
                Console.WriteLine(ciphertexti);

            }
            else
            {

            }
        }
        public static void readMessage(string message )
        {

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
                else if (path == null)
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
                else if (path == null)
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
                Console.WriteLine("Eshte krijuar celsesi privat" +
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
                Console.WriteLine("Eshte krijuar celsesi public" +
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
                Console.WriteLine("Eshte larguar celsesi public" + path1);
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
         public static void shfaq()
        {
            Console.WriteLine("=======================================================================\n");
            Console.WriteLine("Operacioni case: case (alternating,inverse,capitalize,upper,lower) teksti");
            Console.WriteLine("Operacioni vigener: vigenere (encrypt,decrypt) tekst key");
            Console.WriteLine("Operacioni foursquare: foursquare (encrypt,decrypt) tekst key1 key2");
            Console.WriteLine("Operacioni create-user: create-user user");
            Console.WriteLine("Operacioni delete-user: delete-user user");
            Console.WriteLine("Operacioni export-key: export-key (private,public) name path");
            Console.WriteLine("Operacioni import-key: import-key  name  path ");
            Console.WriteLine("Operacioni write-message: write-message: name message file");
            Console.WriteLine("Operacioni read -message: read-message ");
            
            Console.WriteLine("=========================================================================");
        }
       
       
    }
}
