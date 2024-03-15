using DBSpeedTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace DBSpeedTest
{
    public class Program
    {
        private static string GenerateArtistName (Random r, int len)
        {
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                if (b < len)
                {
                    Name += vowels[r.Next(vowels.Length)];
                    b++;
                }
            }
            return Name;
        }
        private static String GenerateArtistDescription (Random r)
        {
            String[] genre = { "pop", "rock", "disco", "hiphop", "rap", "classic", "postmodern" };
            return genre[r.Next(genre.Length)];
        }

        private static void SetDBSize(int size)
        {
            SqlConnection con;
            static string connectionString = @"Data Source=LAPTOP-CLDC7DLB\SQLEXPRESS;Initial Catalog=enchantedears;Integrated Security=true;";
            con = new SqlConnection(connectionString);
            con.Open();

            SqlCommand commandDeleteAllData;
            SqlDataAdapter adapterDeleteAllData = new SqlDataAdapter();
            String sqlDelete = "TRUNCATE TABLE [dbo].[artistSpeedTest]";
            commandDeleteAllData = new SqlCommand(sqlDelete, con);
            adapterDeleteAllData.DeleteCommand = new SqlCommand(sqlDelete, con);
            adapterDeleteAllData.DeleteCommand.ExecuteNonQuery();
        
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                SqlCommand commandInsert;
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sqlInster = "INSERT INTO [dbo].[artistSpeedTest](Name, Description) VALUES('" + GenerateArtistName(r) + "', '" + GenerateArtistDescription(r) + "')";
                commandInsert = new SqlCommand(sqlInsert, con);
                adapter.InsertCommand = new SqlCommand(sqlInsert, con);
                adapter.InsertCommand.ExecuteNonQuery();
                commandInsert.Dispose();
            }
        }

        private static void TestAll()
        {
            TestAdoNet();
            TestEntityFramework();
            TestNoSQL();
        }

        private static void TestAdoNet()
        {
            String connectionString;
            SqlConnection con;
            static string connectionString = @"Data Source=LAPTOP-CLDC7DLB\SQLEXPRESS;Initial Catalog=enchantedears;Integrated Security=true;";
            con = new SqlConnection(connectionString);
            con.Open();
            
            SqlCommand command;
            String sql;

            int lines = 1000;
            int[] testLines = new int[]{1, 10, 1000, 100000, 1000000};

            // continue...
        }

        public static void Main(string[] args)
        {
            TestAll();
        }

    }
}