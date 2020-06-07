using System;
using System.Collections.Generic;
using System.Text;
using Chilkat;

namespace ds
{
    class TokenGen
    {

        public static string GenjeroTokenin(string user)
        {

            Chilkat.PrivateKey privKey = new Chilkat.PrivateKey();
            bool success = privKey.LoadEncryptedPemFile("keys\\" + user + ".pem", "passwd");
            if (!success)
            {
                return "Nuk ekziston Celsi ose nuk mund te hapet";
            }
            Chilkat.Jwt jwt = new Chilkat.Jwt();
            Chilkat.JsonObject jose = new Chilkat.JsonObject();
            //  Use RS256.  Pass the string "RS384" or "RS512" to use RSA with SHA-384 or SHA-512.
            success = jose.AppendString("alg", "RS256");
            success = jose.AppendString("typ", "JWT");
            //  Now build the JWT claims (also known as the payload)
            Chilkat.JsonObject claims = new Chilkat.JsonObject();
           
            success = claims.AppendString("sub", user);
            DateTime aDay = DateTime.Now;
            TimeSpan a20min = new System.TimeSpan(0, 0, 20, 0);
            DateTime after20minutes = aDay.Add(a20min);

            string k = after20minutes.ToString();
            success = claims.AppendString("Valid-till", k);
            //success = claims.AppendString("aud", "http://example.com");
            //  Set the timestamp of when the JWT was created to now.
            int curDateTime = jwt.GenNumericDate(0);
           
            success = claims.AddIntAt(-1, "iat", curDateTime);

            //  Set the "not process before" timestamp to now.
            success = claims.AddIntAt(-1, "nbf", curDateTime);

            //  Set the timestamp defining an expiration time (end time) for the token
            //  to be now + 20 minutes (1200 seconds)
            success = claims.AddIntAt(-1, "exp", curDateTime + 1200);
            // adding the tme when its not valid
           

            //  Produce the smallest possible JWT:
            jwt.AutoCompact = true;
            

            //  Create the JWT token.  This is where the RSA signature is created.
            string token = jwt.CreateJwtPk(jose.Emit(), claims.Emit(), privKey);

            //Console.WriteLine(token);
            return token;
        }
        public static bool TokenStatus(string token)
        {
            Chilkat.Jwt jwt = new Chilkat.Jwt();
            //Chilkat.Jwt jwt = new Chilkat.Jwt();
            //string payload = jwt.GetPayload(token);
            //Console.WriteLine(payload);


            Chilkat.PublicKey pubKey = new Chilkat.PublicKey();
            bool success = pubKey.LoadFromFile("keys\\" + 
                GetTuser(token,false)+ ".pub.pem");
            Console.WriteLine(GetTuser(token, false));
            bool sigVerified = jwt.VerifyJwtPk(token, pubKey);
            if (sigVerified)
            {
               // Console.WriteLine("valid " + Convert.ToString(sigVerified));
                int leeway = 60;
                bool bTimeValid = jwt.IsTimeValid(token, leeway);
                Console.WriteLine("time constraints valid: " + Convert.ToString(bTimeValid));
                if (bTimeValid)
                {
                   // Console.WriteLine(GetTuser(token, true));
                }
                return true;
            }
            else
            {
               // Console.WriteLine("nuk eshte valid");
                return false;
            }
                
            
           

           



        }
        public static string GetTuser(string token,bool op)
        {
            Chilkat.Jwt jwt = new Chilkat.Jwt();
            string payload = jwt.GetPayload(token);
            string[] Claims = payload.Split(",");

            string[] Tedhenat = Claims[0].Split(":");
            int f = Tedhenat[0].Length;

            string user = Tedhenat[1].Substring(1, f );
            string koha = Claims[1];
            if (op)
            {
                return koha;
            }
            else
                return user;
        }



    }   

}
    

    

