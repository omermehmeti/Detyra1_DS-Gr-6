using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace ds
{
    class Connectimimedb
    {
        static OleDbConnection con;
        static OleDbCommand cmd;
        static OleDbDataReader reader;
        public static string ShkruajPassword()
        {
            return "";
        }
        
        
        public static string GetSalt(string user,bool H)
        {
           // int counter = 0;
            con = new OleDbConnection();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Lenovo\\Desktop\\Detyra1_DS-Gr-6-master\\ds\\bin\\Debug\\netcoreapp3.0\\appDB.accdb";
            cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM USERS WHERE Emri='" + user +"'";
            con.Open();
            reader = cmd.ExecuteReader();
            string[] salt= new string[2];
            string[] hash = new string[2];
            
            while (reader.Read())
            {//www.csharp-console-example.com
                salt[0] = reader[2].ToString();
                hash[0] = reader[3].ToString();
                //counter++;
                //Console.WriteLine(reader[0] + "-" + reader[1] + " " + reader[2]);
            }
            if (H == true)
            {
                return hash[0];
            }
            else
                return salt[0];


          //  con.Close();
            
        }
       
        public static void InsertStudent(string name)
        {
            Regex obj = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{6,}$");
            //Console.Write("First Name : ");
           // string fname = Console.ReadLine();
            string Password="";
            bool passinput = true;
            while (passinput)
            {
                Console.Write("Password : ");
               
                string password1 = Login.ShkruajPass();
                //string Password = "";
                if (obj.IsMatch(password1))
                {
                    Console.Write("Re type Password :");
                    Password = Login.ShkruajPass();
                    passinput = false;
                }
                else
                {
                    Console.WriteLine("Passwordi duhet te permbaje karakteret e speecifikaura");
                    passinput = true;
                }

                bool pass = false;
                if ((Password != password1)&!(Password==""))
                {

                    pass = true;
                    passinput = false;

                }

                while (pass&!passinput)
                {
                    Console.WriteLine("Paswordat nuk perputhen");
                    Console.Write("Password : ");
                    
                    string password12 = Login.ShkruajPass();
                    if (obj.IsMatch(password12))
                    {
                        Console.Write("Re type Password :");
                        string Password13 = Login.ShkruajPass();
                        if (password12 == Password13)
                        {
                            pass = false;
                            passinput = false;
                            Password = password12;
                        }
                            

                    }
                    else
                    {
                        Console.WriteLine("passwordi duhet te permbaje karakteret e specifikuara");
                        passinput = true;
                        pass = true;
                    }
                   

                }

            }
            String salt = GjeneroSalt.KrijoSalt(10);
            string hashFjalkalimi;
            hashFjalkalimi = GjeneroSalt.GjeneroSHA256Hashin(Password, salt);



            con = new OleDbConnection();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Lenovo\\Desktop\\Detyra1_DS-Gr-6-master\\ds\\bin\\Debug\\netcoreapp3.0\\appDB.accdb";
            cmd = new OleDbCommand();
            cmd.Connection = con;
            
            cmd.CommandText = "INSERT INTO USERS (Emri,Passwordi,Salt,Hash) VALUES ('" + name + "','" + Password + "','" + salt + "','" + hashFjalkalimi + "')";

            // cmd.CommandText = "INSERT INTO Users (Name,Password) VALUES ('" + fname + "','" + Password+ "');
            //www.csharp-console-example.com
            con.Open();
            int sonuc = cmd.ExecuteNonQuery();
            con.Close();
            if (sonuc > 0)
            {
                Console.WriteLine("Inserted");
            }
            else
            {
                Console.WriteLine("Three are errors. The record was not inserted.");
            }
        }
        public static void DeleteStudent(string name)
        {


            con = new OleDbConnection();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Lenovo\\Desktop\\Detyra1_DS-Gr-6-master\\ds\\bin\\Debug\\netcoreapp3.0\\appDB.accdb";
            cmd = new OleDbCommand();
            cmd.Connection = con;
            //string omi = "omer";
            //cmd.CommandText = "DELETE FROM USERS WHERE Emri=" +omi+ "";
            cmd.CommandText = "DELETE FROM Users WHERE Emri='" + name + "'";

            //www.csharp-console-example.com
            con.Open();
            int sonuc = cmd.ExecuteNonQuery();
            con.Close();
            if (sonuc > 0)
            {
                Console.WriteLine("Deleted.");
            }
            else
            {
                Console.WriteLine("Three are errors. The record was not deleted.");
            }
        }
        public static Boolean verify(string name)
        {
            
            con = new OleDbConnection();
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Lenovo\\Desktop\\Detyra1_DS-Gr-6-master\\ds\\bin\\Debug\\netcoreapp3.0\\appDB.accdb";
            cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM Users WHERE Emri='" + name + "'";
            
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
           

        }
    }
}
