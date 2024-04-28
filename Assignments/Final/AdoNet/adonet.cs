using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DBSpeedTest
{

    partial class Program
    {
        static void Main(string[] args)
        {
            AdoNet adoNet = new AdoNet();

            //int[] numRowOptions = { 1, 1000, 100000, 1000000 };
            int[] numRowOptions = { 1, 1000, 100000, 1000000 };

            foreach (int numRows in numRowOptions)
            {
                Console.WriteLine($"Testing with {numRows} rows:");
                adoNet.PerformOperations(numRows);
                Console.WriteLine();
            }
        }
    }

    internal class AdoNet
    {
        public void PerformOperations(int numRows)
        {
            // SQL Server connection string
            String connectionString = @"Data Source=LAPTOP-CLDC7DLB\SQLEXPRESS;Initial Catalog=enchantedears;Integrated Security=true;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                String name = "Taylor Swift";
                String description = "Pop";

                // Track time for insert operation
                Stopwatch insertStopwatch = new Stopwatch();
                insertStopwatch.Start();

                for (int i = 0; i < numRows; i++)
                {
                    PerformInsertOperation(connection, $"{name} {i}", $"{description} {i}");
                }
                insertStopwatch.Stop();
                Console.WriteLine($"Insertion Time: {insertStopwatch.ElapsedMilliseconds} ms");

                // Track time for select operation
                Stopwatch selectStopwatch = new Stopwatch();
                selectStopwatch.Start();

                for (int i = 0; i < numRows; i++)
                {
                    PerformSelectOperation(connection, $"{name} {i}");
                }
                selectStopwatch.Stop();
                Console.WriteLine($"Selection Time: {selectStopwatch.ElapsedMilliseconds} ms");

                // Track time for update operation
                Stopwatch updateStopwatch = new Stopwatch();
                updateStopwatch.Start();

                for (int i = 0; i < numRows; i++)
                {
                    PerformUpdateOperation(connection, $"{name} {i}", $"{description} Updated {i}");
                }
                updateStopwatch.Stop();
                Console.WriteLine($"Update Time: {updateStopwatch.ElapsedMilliseconds} ms");

                // Track time for delete operation
                Stopwatch deleteStopwatch = new Stopwatch();
                deleteStopwatch.Start();

                for (int i = 0; i < numRows; i++)
                {
                    PerformDeleteOperation(connection, $"{name} {i}");
                }
                deleteStopwatch.Stop();
                Console.WriteLine($"Deletion Time: {deleteStopwatch.ElapsedMilliseconds} ms");
            }
        }

        private static void PerformInsertOperation(SqlConnection connectionString, String name, String description)
        {
            String insertQuery = "INSERT INTO dbo.Artist (Name, Description) VALUES (@Value1, @Value2)";
            SqlCommand command = new SqlCommand(insertQuery, connectionString);
            command.Parameters.AddWithValue("@Value1", name);
            command.Parameters.AddWithValue("@Value2", description);

            int rowsAffected = command.ExecuteNonQuery();
            //Console.WriteLine($"{rowsAffected} row(s) inserted.");
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
                    //Console.WriteLine($"Name: {reader["Name"]}, Description: {reader["Description"]}");
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
                //Console.WriteLine($"{rowsAffected} row(s) deleted.");
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
                //Console.WriteLine($"{rowsAffected} row(s) deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}