using System;
using System.Globalization;
using System.Text;


namespace ds
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean t1 = false;
            Console.WriteLine("Hello World!");
            string fjalia="";
            while (t1 == false)
            {
                Console.WriteLine("Futeni Fjaline te cilen deshironi ta manipuloni me te");
                 fjalia = Console.ReadLine();
                Console.WriteLine("Kjo eshte fjalia e lexuare:" + fjalia + "\n A kjo fjalia qe deshironi te manipuloni me te ");
                string test1 = Console.ReadLine();
                if (test1=="po")
                {
                    t1 = true;
                }
                else
                {
                    t1 = false;
                }
            }
            Console.WriteLine("jepeni formen ne te cilen deshironi ta keni tekstin e future");
            Class1.shfaq();
            string case1 = Console.ReadLine();
            switch (case1.ToLowerInvariant())
            {
                case "upper":
                    // using string functions
                    Console.WriteLine(fjalia.ToUpper());
                    break;
                case "lower":
                    //using string functions
                    Console.WriteLine(fjalia.ToLower());
                    break;
                case "capitalization":
                    // Every word start with uppercase  
                    // code copied from internet
                    
                    string fullText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fjalia);
                    Console.WriteLine(fullText);
                    break;
                case "inverse":
                    Class1.kontrollo(fjalia, case1);
                    break;
                case "alternating":
                    Class1.kontrollo(fjalia, case1);
                    break;
                default:
                    Console.WriteLine("Forma e tekstit qe deshironi  nuk mund te mundesohet");
                    break;

            }
            Boolean t2 = false;
            while (t2 == false)
            {
                Console.WriteLine("Futeni Fjaline te cilen deshironi ta manipuloni me te");
                fjalia = Console.ReadLine();
                Console.WriteLine("Kjo eshte fjalia e lexuare:" + fjalia + "\n A kjo fjalia qe deshironi ta kodoni ");
                string test1 = Console.ReadLine();
                if (test1 == "po")
                {
                    t2 = true;
                }
                else
                {
                    t2 = false;
                }
            }

            string ciphertext1 =program2.Encrypt(fjalia, "kosov");
            Console.WriteLine("Fjalia e koduar eshte: "+ciphertext1);
            string decryptedtext1 = program2.Decrypt(ciphertext1, "kosov");
            Console.WriteLine("fjalia e dekoduar eshte: " + decryptedtext1);
            foursquare.program("omer", "ro", "it");

        }
    }
}
