using Amazon.Auth.AccessControlPolicy.ActionIdentifiers;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DBSpeedTest
{
    partial class Program
    {
        static void AMain(string[] args)
        {
            AdoNet adoNet = new AdoNet();
            string connectionString = @"Data Source=LAPTOP-CLDC7DLB\SQLEXPRESS;Initial Catalog=enchantedears;Integrated Security=true;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Track time for each operation
                Stopwatch insertStopwatch = new Stopwatch();
                Stopwatch selectStopwatch = new Stopwatch();
                Stopwatch updateStopwatch = new Stopwatch();
                Stopwatch deleteStopwatch = new Stopwatch();

                // Testing with several rows
                int numRows = 1000000;
                Console.WriteLine($"Testing with {numRows} rows");

                string artistName;
                string description;

                for (int i = 0; i < numRows; i++)
                {
                    artistName = $"Joe Jonas {i}";
                    description = "Pop";

                    //adoNet.PerformOperations(numRows);
                    insertStopwatch.Start();
                    adoNet.PerformInsertOperation(connection, artistName, description);
                    insertStopwatch.Stop();

                    /*
                    selectStopwatch.Start();
                    adoNet.PerformSelectOperation(connection, artistName);
                    selectStopwatch.Stop();

                    updateStopwatch.Start();
                    description = "Band Pop";
                    adoNet.PerformUpdateOperation(connection, artistName, description);
                    updateStopwatch.Stop();

                    deleteStopwatch.Start();
                    adoNet.PerformDeleteOperation(connection, artistName);
                    deleteStopwatch.Stop();
                    */
                }
                Console.WriteLine($"Insertion Time: {insertStopwatch.ElapsedMilliseconds} ms");
                Console.WriteLine($"Selection Time: {selectStopwatch.ElapsedMilliseconds} ms");
                Console.WriteLine($"Update Time: {updateStopwatch.ElapsedMilliseconds} ms");
                Console.WriteLine($"Deletion Time: {deleteStopwatch.ElapsedMilliseconds} ms");
                

                connection.Close();
            }
        }
    }

    public class AdoNet
    {

        public void PerformInsertOperation(SqlConnection connection, string name, string description)
        {
            string insertQuery = "INSERT INTO dbo.Artist (Name, Description) VALUES (@name, @description)";
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@description", description);
                command.ExecuteNonQuery();
            }
        }

        public void PerformSelectOperation(SqlConnection connection, string name)
        {
            string selectQuery = "SELECT * FROM dbo.Artist WHERE Name LIKE @name";
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void PerformDeleteOperation(SqlConnection connection, string artistName)
        { 
            try
            {
                string deleteArtistQuery = "DELETE FROM dbo.Artist WHERE Name LIKE @artistName";
                using (SqlCommand deleteArtistCommand = new SqlCommand(deleteArtistQuery, connection))
                {
                    deleteArtistCommand.Parameters.AddWithValue("@artistName", artistName);
                    int artistDeleted = deleteArtistCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting artist: {ex.Message}");
            }
        }

        public void PerformUpdateOperation(SqlConnection connection, string name, string description)
        {
            string updateQuery = "UPDATE dbo.Artist SET Description = @description WHERE Name = @name";
            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@name", name);
                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
