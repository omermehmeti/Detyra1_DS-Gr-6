using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;


namespace ds
{
    class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                
                Class1.shfaq();
                return 1;
               
                









            }
            else
            {

                
                switch (args[0])
                {
                    case "case":
                        if (args.Length == 3)
                        {
                            Class1.Kontrollo(args[1], args[2]);
                            return 0;

                        }
                        else
                        {
                            Console.WriteLine("keni shtypur dicka gabim");
                            Class1.shfaq();
                            return 1;
                        }
                           
                        
                      
                    case "vigenere":
                        if (args.Length < 4)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull\n");
                            Class1.shfaq();
                            return 1;
                        }
                        else if (args[1] == "encrypt")
                        {
                          Console.WriteLine(program2.Encrypt(args[2], args[3]));
                            return 0;
                        }
                        else if (args[1] == "decrypt")
                        {
                          Console.WriteLine(program2.Decrypt(args[2], args[3]));
                            return 0;
                        }
                        else
                        {
                            Console.WriteLine("keni shtypur dicka gabim");
                            Class1.shfaq();
                            return 1;
                        }
                        
                    case "foursquare":
                        if (args.Length<5)
                        {
                            Console.WriteLine("Keni shtypur cicka gabim\n");
                            Class1.shfaq();
                            return 1;
                        }
                        else if (args[1] == "encrypt")
                        {
                            foursquare.encrypt(args[2],args[3],args[4]);
                            return 0;
                        }
                        else if (args[1] == "decrypt")
                        {
                             foursquare.Decrypt(args[2],args[3],args[4]);
                            return 0;
                        }
                        else
                        {
                            Console.WriteLine("keni shtypur dicka gabim");
                            
                            Class1.shfaq();
                            return 1;
                        }
                        
                    case "create-user":
                       
                        if (args.Length < 2)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull\n");
                            Class1.shfaq();
                            return 1;
                        }
                        else
                        {
                            Regex obj = new Regex("^[a-zA-Z0-9/_]*$");
                            // string s = Console.ReadLine();
                            string u = "C:\\keys\\" + args[1] + ".xml";
                            if (!obj.IsMatch(args[1]))
                            {
                                Console.WriteLine("Useri  nuk mund te permbaje disa nga karakteret" +
                                    "qe keni perdorur");

                            }
                            else if (File.Exists(u))
                                Console.WriteLine("Celesi " + args[1] + " ekziston paraprakisht");
                            else
                                Class1.Createuser(args[1]);
                            return 0;
                        }
                        

                        
                        
                    case "delete-user":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull\n");
                            Class1.shfaq();
                            return 1;
                        }
                        else
                        {
                            Class1.Deleteuser(args[1]);
                            return 0;

                        }
                            
                        
                    case "export-key":
                        if (args.Length < 4)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull\n");
                            Class1.shfaq();
                            return 1;

                        }
                        else if (args[1] == "public")
                        {
                            Class1.exportKey("public", args[2], args[3]);
                            return 0;
                        }
                        else if (args[1] == "private")
                        {
                            Class1.exportKey("private", args[2], args[3]);
                            return 0;
                        }
                        else
                        {
                            Console.WriteLine("operacioni qe kerkuat nuk mund te mundesohet");
                            return 1;
                        }
                           
                        
                    case "import-key":
                        if (args.Length < 3)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull\n");
                            Class1.shfaq();
                            return 1;
                        }
                        else
                        {
                            Class1.importKey(args[1], args[2]);
                            return 0;
                        }
                           
                        
                    case "write-message":
                        if (args.Length <3)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull\n");
                            Class1.shfaq();
                            return 1;
                            //Class1.writeMessage(args[1], args[2], "");
                        }
                        else if(args.Length==3)
                        {
                            Class1.writeMessage(args[1], args[2], "");
                            return 0;
                        }
                        else
                        {
                            Class1.writeMessage(args[1], args[2], args[3]);
                            return 0;
                        }
                        
                    case "read-message":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull\n");
                            Class1.shfaq();
                            return 1;
                        }
                        else
                        {
                            Class1.readMessage(args[1]);
                            return 0;
                        }


                        //Class1.readMessage(args[1]);
                        


                    default:
                        Console.WriteLine("operacioni qe keni kerkuar nuk mund te mundesohet");
                        Class1.shfaq();
                        return 1;
                        
                }
                








            }
        }
    }
}
