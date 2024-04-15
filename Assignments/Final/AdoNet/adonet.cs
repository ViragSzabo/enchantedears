using System;
using System.Data.SqlClient;

namespace DBSpeedTest
{
    partial class Program
    {
        static void Main(string[] args)
        {
            AdoNet adoNet = new AdoNet();
            adoNet.PerformOperations();
        
        }
    }

    internal class AdoNet
    {
        public void PerformOperations()
        {
            // SQL Server connection string
            String connectionString = @"Data Source=LAPTOP-CLDC7DLB\SQLEXPRESS;Initial Catalog=enchantedears;Integrated Security=true;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                String name = "Billie Eilish";
                String description = "Pop";

                PerformInsertOperation(connection, name, description);
                Console.WriteLine(name + " got added.");

                PerformSelectOperation(connection, name);
                Console.WriteLine("Selected " + name);

                PerformUpdateOperation(connection, name, description);
                Console.WriteLine("Updated the description of " + name + ". The refreshed description: " + description);

                PerformDeleteOperation(connection, name);
                Console.WriteLine(name + " got removed.");
            }
        }

        private static void PerformInsertOperation(SqlConnection connectionString, String name, String description)
        {
            String insertQuery = "INSERT INTO dbo.Artist (Name, Description) VALUES (@Value1, @Value2)";
            SqlCommand command = new SqlCommand(insertQuery, connectionString);
            command.Parameters.AddWithValue("@Value1", name);
            command.Parameters.AddWithValue("@Value2", description);

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} row(s) inserted.");
        }

        private static void PerformSelectOperation(SqlConnection connectionString, String name)
        {
            string selectQuery = "SELECT * FROM dbo.Artist WHERE name = @address";
            SqlCommand command = new SqlCommand(selectQuery, connectionString);
            command.Parameters.AddWithValue("@address", name); 

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Name: {reader["Name"]}, Description: {reader["Description"]}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void PerformDeleteOperation(SqlConnection connectionString, String name)
        {
            string deleteQuery = "DELETE FROM dbo.Artist WHERE Name = @name";
            SqlCommand command = new SqlCommand(deleteQuery, connectionString);
            command.Parameters.AddWithValue("@name", "Billie Eilish");

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void PerformUpdateOperation(SqlConnection connectionString, String name, String description)
        {
            string updateQuery = "UPDATE dbo.Artist SET Description = @description WHERE Name = @name";
            SqlCommand command = new SqlCommand(updateQuery, connectionString);
            command.Parameters.AddWithValue("@description", "Goth-Pop");
            command.Parameters.AddWithValue("@name", "Billie Eilish");
            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}