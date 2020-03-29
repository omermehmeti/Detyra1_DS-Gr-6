using System;
using System.Collections.Generic;
using System.Text;

namespace ds
{
    class Class1
    {
        public static void shfaq()
        {
            Console.WriteLine("Shtypeni upper per shkronja te medha");
            Console.WriteLine("Shtypeni lower per shkronja te vogla");
            Console.WriteLine("Shtypeni capitalization per shkronje te medha per cdo fjale");
            Console.WriteLine("Shtypeni alternating per menyren e deformuare");
            Console.WriteLine("Shtypeni inverse per shfaqjen inverse te cdo shkronje");
        }
        public static void kontrollo(string a,string b)
        {
            int ln = a.Length;
            StringBuilder str = new StringBuilder(a);
            if (b == "inverse")
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
            else
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
            
        }
    }
}
