using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DBSpeedTest
{
    class Program
    {
        static string connectionString = @"Data Source=LAPTOP-CLDC7DLB\SQLEXPRESS;Initial Catalog=enchantedears;Integrated Security=true;";

        static void Main()
        {
            // test the operations the speed using a stopwatch
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            PerformInsertOperation(connectionString);
            stopwatch.Stop();
            Console.WriteLine($"Insert Operation Time taken: {stopwatch.Elapsed.TotalMilliseconds} milliseconds");

            stopwatch.Restart();
            PerformUpdateOperation(connectionString);
            stopwatch.Stop();
            Console.WriteLine($"Update Operation Time taken: {stopwatch.Elapsed.TotalMilliseconds} milliseconds");

            stopwatch.Restart();
            PerformSelectOperation(connectionString);
            stopwatch.Stop();
            Console.WriteLine($"Select Operation Time taken: {stopwatch.Elapsed.TotalMilliseconds} milliseconds");

            stopwatch.Restart();
            PerformDeleteOperation(connectionString);
            stopwatch.Stop();
            Console.WriteLine($"Delete Operation Time taken: {stopwatch.Elapsed.TotalMilliseconds} milliseconds");
        }

        static void PerformInsertOperation(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO dbo.Artist (Name, Description) VALUES (@Value1, @Value2)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Value1", "Billie Eilish");
                    command.Parameters.AddWithValue("@Value2", "Pop");

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
            }
        }

        static void PerformUpdateOperation(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE dbo.Artist SET Description = @NewDescription WHERE Name = @ArtistName";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@NewDescription", "Goth-Pop");
                    command.Parameters.AddWithValue("@ArtistName", "Billie Eilish");

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) updated.");
                }
            }
        }

        static void PerformSelectOperation(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM dbo.Artist";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Name: {reader["Name"]}, Description: {reader["Description"]}");
                        }
                    }
                }
            }
        }

        static void PerformDeleteOperation(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM dbo.Artist WHERE Name = @ArtistName";
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@ArtistName", "Billie Eilish");

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) deleted.");
                }
            }
        }
    }
}