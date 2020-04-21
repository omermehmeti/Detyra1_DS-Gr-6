using System;
using System.Collections.Generic;
using System.Text;

namespace ds
{
    class Class1
    {
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
