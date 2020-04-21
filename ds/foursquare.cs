using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ds
{
    class foursquare
    {
        
        // method squarek1 is used to form a square with 
        public static char[,] squarek1(string key1, char[,] c1)
        {
            // key1.
            
        // this method eturn right character to put in matrix (avoiding putting the samech)
        public static char rchar(int k,char[,] c1)
        {
            char u='1';
            for(int i = k; i < 25; i++)
            {
                if (has1((char)('A' + i),c1))
                {
                    continue;
                }
                else
                {
                    
                    u= (char)('A' + i);
                    break;
                }
            }
            return u;
        }
        //this method return boolean if a matrix has a certain  character it returns true
        public static Boolean has1(char c,char [,] squarek11)
        {
            Boolean t1 = false;
            for(int i = 0; i < 5; i++)
            {
                for(int k = 0; k < 5; k++)
                {
                    if (squarek11[i, k] == c)
                    {
                        t1 = true;
                        break;

                    }
                }
            }
            return t1;
        }
        // this method removes repetetive characters of keywords
       public static string doubles(string key1)
        {
            StringBuilder key11 = new StringBuilder(key1);
            String withoutdoubles="";
            for(int i = 0; i < key1.Length; i++)
            {
                if (withoutdoubles.Contains(key11[i]))
                {
                    continue;
                }
                else
                {
                    withoutdoubles.Append(key11[i]);

                }
            }

            return withoutdoubles;
        }
        // 
        
        // this method tells if the plaintext has even or odd character number
       public static Boolean verification(string s)
        {
            s.Trim();
            int s1=0;
            for(int i = 0; i < s.Length + 1; i++)
            {
                s1++;
            }
            if (s1 % 2 == 0)
            {
                return true;
            }
            else
            {
                s.Append('X');
                return false;
            }
        }
        //this method creates  arrays with twio characters from plaintext
        public static int key1 = 0, key2 = 2;

        public static void createarrays(string s)
        {
            verification(s);
           int k1 = key1;
            int k2 = key2;
            for(int i = 0; i < s.Length;i++)
            {
               
                
                    
                    
                try
                {
                    
                    s.Substring(key1, key2);
                    bigram[i] = s.Substring(key1, key2);
                }

                catch (Exception e)
                {

                   
                }
                k1 += 2;
                k2 += 2;


            }
        }
        // this method encodes the bigrams
        public static void encode()
        {

           for(int i = 0; i < bigram.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int r1, c1, r2, c2;
                   r1=row( bigram[i].ElementAt(0),square);
                    c1 = column(bigram[i].ElementAt(0),square);

                    r2=row(bigram[i].ElementAt(1),square);
                    c2 = column(bigram[i].ElementAt(1),square);
                    encrypted.Append(squarek11[r1,c2]);
                    encrypted.Append(squarek12[r2, c1]);
                }
            }
            Console.WriteLine(encrypted);

            
        }
        //this methode teturns the colomn where a certain character is
        public static int column(char c,char[,] c1)
        {
            int u=5;
            for(int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; i++)
                {
                    if (c1[i, j] == c)
                    {
                        u = j;
                        break;
                    }
                    else continue;

                }
            }

            return u;
        }
        public static int row(char c,char[,] c1)
        {
            int u = 5;
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if (c1[i, j] == c)
                    {
                        u = i;
                        break;
                    }
                    else continue;

                }
            }
            return u;
        }
        public static void program(string s,string k1, string k2)
        {
            createarrays(s);
            square1();

            squarek1(k1, squarek11);
            squarek1(k2, squarek12);
            encode();

        }


    }
}
