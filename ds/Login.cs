using System;
using System.Collections.Generic;
using System.Text;

namespace ds
{
    class Login
    {
        public static bool VerifyPassword(string user,string password)
        {
            string PASS = Connectimimedb.GetSalt(user,false);
           // Console.WriteLine(PASS);
            
            string testhash = GjeneroSalt.GjeneroSHA256Hashin(password, PASS);
           // Console.WriteLine(testhash);
            string dbHash = Connectimimedb.GetSalt(user, true);
            if (dbHash == testhash)
            {
                return true;
            }
            else
                return false;
           
        }
        public static string ShkruajPass()
        {
            StringBuilder passwordBuilder = new StringBuilder();
            bool continueReading = true;
            char newLineChar = '\r';
            while (continueReading)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                char passwordChar = consoleKeyInfo.KeyChar;

                if (passwordChar == newLineChar)
                {
                    continueReading = false;
                }
                else
                {
                    passwordBuilder.Append(passwordChar.ToString());
                }
            }
            return passwordBuilder.ToString();
        }
        public static void Kredincialet(string one)
        { }
           
    }
}
