using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ds
{
    class foursquare
    {
        public static void squarek(string key1, ref char[,] c1)
        {
            // key1.
            // char[,] c2 = { };
            //StringBuilder key11 = new StringBuilder(doubles(key1));
            // StringBuilder sqr = new StringBuilder()
            string alpha = "ABCDEFGHIKLMNOPRSTUVXYZ";
            string key11 = doubles(key1);
            int count = 0;
            int count2 = 0;
           // Console.WriteLine(key11);
            string celsi = "";

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (count < key11.Length)
                    {
                        c1[i, j] = key11[count];
                        //c2[i, j] = c1[i, j];
                        celsi += c1[i, j];
                        count += 1;
                    }
                    else
                    {
                        // int v = j;
                        if (celsi.Contains(alpha[count2]))
                        {
                            c1[i, j] = rchar(count2, c1);


                        }
                        else
                        {
                            c1[i, j] = alpha[count2];
                            count2 += 1;
                        }


                    }

                }
                count += 1;
            }

        }
        // this method eturn right character to put in matrix (avoiding putting the samech)
        public static char rchar(int k, char[,] c1)
        {
            char u = '1';
            for (int i = k; i < 26; i++)
            {
                if (has1((char)('A' + i), c1))
                {
                    continue;
                }
                else
                {
                    if ((char)('A' + i) == 'J') continue;
                    else
                        u = (char)('A' + i);
                    break;
                }
            }
            return u;
        }
        //this method return boolean if a matrix has a certain  character it returns true
        public static Boolean has1(char c, char[,] squarek11)
        {
            Boolean t1 = false;
            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (squarek11[i, k] == c)
                    {
                        return true;


                    }
                }
            }
            return t1;
        }
        // this method removes repetetive characters of keywords
        public static string doubles(string key1)
        {
            string inputString = key1;
            string resultstring = "";
            for (int i = 0; i < inputString.Length; i++)
            {
                if (!resultstring.Contains(inputString[i]))
                {
                    resultstring += inputString[i];
                }
            }
            return resultstring.ToUpper();
        }
        // 

        // this method tells if the plaintext has even or odd character number
        public static string verify(ref string s)
        {
            s.Trim();
            s.ToUpper();
            if (s.Length % 2 == 0)
            {
                return s;
            }
            else
            {
                return s + "X";
            }
        }
        //this method creates  arrays with twio characters from plaintext
       //ublic static int key1 = 0, key2 = 2;

        public static void createarrays(ref string s, ref string[] bigram,ref int a)
        {
            verify(ref s);
            int k1 = 0;
            int k2 = 2;
            for (int i = 0; i < s.Length / 2; i++)
            {
                // s.Substring(key1, key2);

                bigram[i] = s.Substring(k1, k2);
                k1 += 2;
                // += 2;
                a += 1;
                





            }
        }
        // this method encodes the bigrams
        public static string encrypt(string plaintext,string k1,string k2)
        {
            char[,] squarek1 = new char[5, 5];
            char[,] squarek2 = new char[5, 5];
            char[,] katroriA = new char[5, 5];
            metod(ref katroriA);
            squarek(k1, ref squarek1);
            squarek(k2, ref squarek2);
             // shfaqjaematrixes(squarek1);
             // shfaqjaematrixes(squarek2);
            String[] bigram = new string[10] ;
            int a = 0;
            string s = plaintext.ToUpper().Trim();
            createarrays(ref s, ref bigram,ref a);
            String encrypted = "";
            
            for(int i = 0; i < a; i++)
            {
                int R1, R2, K1, K2 = 0;
                //Console.WriteLine(bigram[i].ElementAt(0));
                R1 = rendi(bigram[i].ElementAt(0), ref katroriA);
                R2 = rendi(bigram[i].ElementAt(1), ref katroriA);
                K1 = kolona(bigram[i].ElementAt(0), ref katroriA);
                K2 = kolona(bigram[i].ElementAt(1), ref katroriA);
                encrypted += squarek1[R1, K2];
                encrypted += squarek2[R2, K1];
            }


            Console.WriteLine(encrypted);
            return encrypted;
        }
        public static string Decrypt(string ciphertext,string k1,string k2)
        {
            char[,] squarek1 = new char[5, 5];
            char[,] squarek2 = new char[5, 5];
            char[,] katroriA = new char[5, 5];
            metod(ref katroriA);
            squarek(k1, ref squarek1);
            squarek(k2, ref squarek2);
             // shfaqjaematrixes(squarek1);
             //  shfaqjaematrixes(squarek2);
            String[] bigram = new string[10];
            int a = 0;
            string s = ciphertext.ToUpper().Trim();
            createarrays(ref s, ref bigram, ref a);
            String decrypted = "";

            for (int i = 0; i < a; i++)
            {
                int R1, R2, K1, K2 ;
                //Console.WriteLine(bigram[i].ElementAt(0));
                R1 = rendi(bigram[i].ElementAt(0), ref squarek1);
                R2 = rendi(bigram[i].ElementAt(1), ref squarek2);
                K1 = kolona(bigram[i].ElementAt(0), ref squarek1);
                K2 = kolona(bigram[i].ElementAt(1), ref squarek2);
                decrypted += katroriA[R1, K2];
                decrypted += katroriA[R2, K1];
            }


            Console.WriteLine(decrypted);
            return decrypted;

        }
        public static void metod(ref char[,] s)
        {
            int k = 0;
            for (int i = 0; i < 5; i++)
            {
                //int k = 0;
                for (int j = 0; j < 5; j++)
                {
                    if (k == 9) k += 1;
                    s[i, j] = (char)('A' + k);

                    k = k + 1;
                }

            }
        }
        public static int rendi(char c, ref char[,] c1)
        {

            int rendi = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (c1[i, j] == c) return i;
                }
            }

            return rendi;
        }
        public static int kolona(char c,ref char[,] c1)
        {
            int rendi = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (c1[i, j] == c) return j;
                }
            }

            return rendi;
        }









        public static void shfaqjaematrixes(char[,] c)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(c[i, j]);
                }
                Console.WriteLine('\n');
            }
        }
        
       
       
       
        
        
        
       
        
        


    }
}
