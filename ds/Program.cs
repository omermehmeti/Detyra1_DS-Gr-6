using System;
using System.Globalization;
using System.Text;


namespace ds
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Class1.shfaq();/*
                foursquare.encrypt("kosova", "k", "oma");
                foursquare.Decrypt("hpltvo", "k", "oma");*/
            }
            else
            {

                
                switch (args[0])
                {
                    case "case":
                        Class1.Kontrollo(args[1], args[2]);
                        break;
                    case "vigenere":
                        if (args[1] == "encrypt")
                        {
                          Console.WriteLine(program2.Encrypt(args[2], args[3]));
                        }
                        else if (args[1] == "decrypt")
                        {
                          Console.WriteLine(program2.Decrypt(args[2], args[3]));
                        }
                        else
                        {
                            Console.WriteLine("keni shtypur dicka gabim");
                            Class1.shfaq();
                        }
                        break;
                    case "foursquare":
                        if (args[1] == "encrypt")
                        {
                            foursquare.encrypt(args[2],args[3],args[4]);
                        }
                        else if (args[1] == "decrypt")
                        {
                             foursquare.Decrypt(args[2],args[3],args[4]);
                        }
                        else
                        {
                            Console.WriteLine("keni shtypur dicka gabim");
                            
                            Class1.shfaq();
                        }
                        break;
                    case "create-user":
                        string u = "C:\\keys\\" + args[1] + ".xml";
                        if (File.Exists(u)){
                            Console.WriteLine("Celsesi " + args[1] + "ekziston paraprakisht");
                        }
                        else
                            Class1.Createuser(args[1]);
                        break;
                    case "delete-user":
                        Class1.Deleteuser(args[1]);
                        break;
                        
                    default:
                        Console.WriteLine("operacioni qe keni kerkuar nuk mund te mundesohet");
                        Class1.shfaq();
                        break;
                }
                








            }
            
          

        }
    }
}
