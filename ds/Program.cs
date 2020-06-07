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
            Console.Title = "ds";

            if (args.Length == 0)
            {
               
               

                Fshtese.shfaq();

                //Fshtese.Createuser("ismet");
                Login.VerifyPassword("meti", "Meti123");
                
                return 1;
               
               
                









            }
            else
            {

                
                switch (args[0])
                {
                    case "case":
                        if (args.Length > 2)
                        {
                            Fshtese.Kontrollo(args[1], args[2]);
                            return 0;

                        }
                        else
                        {
                            Console.WriteLine("keni shtypur dicka gabim");
                            Fshtese.shfaq();
                            return 1;
                        }
                           
                        
                      
                    case "vigenere":
                        if (args.Length < 4)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull\n");
                            Fshtese.shfaq();
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
                            Fshtese.shfaq();
                            return 1;
                        }
                        
                    case "foursquare":
                        if (args.Length<5)
                        {
                            Console.WriteLine("Keni shtypur cicka gabim\n");
                            Fshtese.shfaq();
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
                            
                            Fshtese.shfaq();
                            return 1;
                        }
                        
                    case "create-user":
                       
                        if (args.Length < 2)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull\n");
                            Fshtese.shfaq();
                            return 1;
                        }
                        else
                        {
                            Regex obj = new Regex("^[a-zA-Z0-9/_]*$");
                            // string s = Console.ReadLine();
                            string u = "\\" + args[1] + ".pem";
                            if (!obj.IsMatch(args[1]))
                            {
                                Console.WriteLine("Useri  nuk mund te permbaje disa nga karakteret" +
                                    "qe keni perdorur");

                            }
                            else if (File.Exists(u))
                                Console.WriteLine("Celesi " + args[1] + " ekziston paraprakisht");
                            else
                                Fshtese.Createuser(args[1]);
                            return 0;
                        }
                        

                        
                        
                    case "delete-user":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull\n");
                            Fshtese.shfaq();
                            return 1;
                        }
                        else
                        {
                            Fshtese.Deleteuser(args[1]);
                            return 0;

                        }
                            
                        
                    case "export-key":
                        if (args.Length < 4)
                        {
                            if (args.Length == 3)
                            {
                                Fshtese.exportKey(args[1], args[2], "");
                                return 0;
                            }
                            else
                            {
                                Console.WriteLine("Argumentet nuk jane ne rregull\n");
                                Fshtese.shfaq();
                                return 1;
                            }
                            

                        }
                        
                        else if (args[1] == "public")
                        {
                            Fshtese.exportKey("public", args[2], args[3]);
                            return 0;
                        }
                        else if (args[1] == "private")
                        {
                            Fshtese.exportKey("private", args[2], args[3]);
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
                            Fshtese.shfaq();
                            return 1;
                        }
                        else
                        {
                            Fshtese.importKey(args[1], args[2]);
                            return 0;
                        }
                    case "log-in":
                        if (args.Length < 2)
                        {
                            Fshtese.shfaq();
                            return 1;
                        }
                        else if (args.Length == 2)
                        {

                            Console.WriteLine("Shtypeni passwordin:");

                            string s = Login.ShkruajPass();
                            
                            if (Login.VerifyPassword(args[1], s))
                            {
                                Console.WriteLine("jeni futur me sukses  ");
                                Console.WriteLine(TokenGen.GenjeroTokenin(args[1]));
                            }
                            else
                                Console.WriteLine("Passwordi ose emri Gabim");
                            return 0;
                        }
                       
                       else 
                        {
                            Fshtese.shfaq();
                            return 1;
                        }


                    case "status":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull");
                            Fshtese.shfaq();
                            return 1;
                        }
                        else
                        {
                            if (TokenGen.TokenStatus(args[1]))
                            {
                               Console.WriteLine( TokenGen.GetTuser(args[1], true));
                               Console.WriteLine( TokenGen.GetTuser(args[1], false));
                            }
                            else
                                Console.WriteLine("Tokeni nuk eshte valid");
                            return 0;
                        }



                    case "write-message":
                        if (args.Length <3)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull\n");
                            Fshtese.shfaq();
                            return 1;
                            //Class1.writeMessage(args[1], args[2], "");
                        }
                        else if(args.Length==3)
                        {
                            Fshtese.writeMessage(args[1], args[2],"", "");
                            return 0;
                        }
                        else
                        {
                            Fshtese.writeMessage(args[1], args[2], args[3],args[4]);
                            return 0;
                        }
                        
                    case "read-message":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("Argumentet nuk jane ne rregull\n");
                            Fshtese.shfaq();
                            return 1;
                        }
                        else
                        {
                            Fshtese.readMessage(args[1]);
                            return 0;
                        }


                        //Class1.readMessage(args[1]);
                        


                    default:
                        Console.WriteLine("operacioni qe keni kerkuar nuk mund te mundesohet");
                        Fshtese.shfaq();
                        return 1;
                        
                }
                








            }
        }
    }
}
