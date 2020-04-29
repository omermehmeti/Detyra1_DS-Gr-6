using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

using System.Xml;
using System.IO;

namespace ds
{
    class Class1
    {
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
            
            Console.WriteLine("=========================================================================");
        }
       
       
    }
}
