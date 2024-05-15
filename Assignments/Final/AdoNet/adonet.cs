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

                string artistName = "Taylor Swift";
                string description = "Pop";

                // Track time for each operation
                Stopwatch insertStopwatch = new Stopwatch();
                Stopwatch selectStopwatch = new Stopwatch();
                Stopwatch updateStopwatch = new Stopwatch();
                Stopwatch deleteStopwatch = new Stopwatch();

                // Testing with several rows
                int[] numRowOptions = { 1000000 };
                foreach (int numRows in numRowOptions)
                {
                    Console.WriteLine($"Testing with {numRows} rows");
                    //adoNet.PerformOperations(numRows);
                    insertStopwatch.Start();
                    adoNet.PerformInsertOperation(connection, artistName, description);
                    insertStopwatch.Stop();
                    selectStopwatch.Start();
                    adoNet.PerformSelectOperation(connection, artistName);
                    selectStopwatch.Stop();
                    updateStopwatch.Start();
                    description = "Indie Folk";
                    adoNet.PerformUpdateOperation(connection, artistName, description);
                    updateStopwatch.Stop();
                    deleteStopwatch.Start();
                    adoNet.PerformDeleteOperation(connection, artistName);
                    deleteStopwatch.Stop();
                    Console.WriteLine();
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
        public void PerformOperations(int numRows)
        {
            // SQL Server connection string
            string connectionString = @"Data Source=LAPTOP-CLDC7DLB\SQLEXPRESS;Initial Catalog=enchantedears;Integrated Security=true;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string artistName = "Taylor Swift";
                string description = "Pop";

                // Track time for each operation
                Stopwatch insertStopwatch = new Stopwatch();
                Stopwatch selectStopwatch = new Stopwatch();
                Stopwatch updateStopwatch = new Stopwatch();
                Stopwatch deleteStopwatch = new Stopwatch();

                for (int i = 0; i < numRows; i++)
                {

                    // Insertion operation
                    insertStopwatch.Start();
                    PerformInsertOperation(connection, artistName, description);
                    insertStopwatch.Stop();

                    // Selection operation
                    selectStopwatch.Start();
                    PerformSelectOperation(connection, artistName);
                    selectStopwatch.Stop();

                    // Update operation
                    updateStopwatch.Start();
                    description = "Indie Folk";
                    PerformUpdateOperation(connection, artistName, description);
                    updateStopwatch.Stop();

                    // Deletion operation
                    deleteStopwatch.Start();
                    PerformDeleteOperation(connection, artistName);
                    deleteStopwatch.Stop();
                }
                Console.WriteLine($"Insertion Time: {insertStopwatch.ElapsedMilliseconds } ms");
                Console.WriteLine($"Selection Time: {selectStopwatch.ElapsedMilliseconds} ms");
                Console.WriteLine($"Update Time: {updateStopwatch.ElapsedMilliseconds} ms");
                Console.WriteLine($"Deletion Time: {deleteStopwatch.ElapsedMilliseconds} ms");

                connection.Close();
            }
        }

        public void PerformInsertOperation(SqlConnection connection, string name, string description)
        {
            string insertQuery = "INSERT INTO dbo.Artist (Name, Description) VALUES (@name, @description)";
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@description", description);
                //int rowsAffected = command.ExecuteNonQuery();
                //Console.WriteLine($"{rowsAffected} row(s) inserted.");
            }
        }

        public void PerformSelectOperation(SqlConnection connection, string name)
        {
            string selectQuery = "SELECT * FROM dbo.Artist WHERE Name = @name";
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        //Console.WriteLine("The row(s) has been read.");
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
            int artistId = GetArtistId(connection, artistName);
            if (artistId == -1)
            {
                //Console.WriteLine($"Artist '{artistName}' not found.");
                return;
            }

            // Retrieve the artist name
            string artistNameToDelete = GetArtistName(connection, artistId);

            try
            {
                // First, delete associated songs from playlists
                string deletePlaylistSongsQuery = "DELETE FROM dbo.PlaylistSong WHERE SongID IN (SELECT SongID FROM dbo.Song WHERE AlbumID IN (SELECT AlbumID FROM dbo.Album WHERE ArtistId = @artistId))";
                using (SqlCommand deletePlaylistSongsCommand = new SqlCommand(deletePlaylistSongsQuery, connection))
                {
                    deletePlaylistSongsCommand.Parameters.AddWithValue("@artistId", artistId);
                    int playlistSongsDeleted = deletePlaylistSongsCommand.ExecuteNonQuery();
                    Console.WriteLine($"Playlist deleted: {playlistSongsDeleted}");
                }

                // Then, delete associated songs
                string deleteSongsQuery = "DELETE FROM dbo.Song WHERE AlbumId IN (SELECT AlbumId FROM dbo.Album WHERE ArtistId = @artistId)";
                using (SqlCommand deleteSongsCommand = new SqlCommand(deleteSongsQuery, connection))
                {
                    deleteSongsCommand.Parameters.AddWithValue("@artistId", artistId);
                    int songsDeleted = deleteSongsCommand.ExecuteNonQuery();
                    Console.WriteLine($"Songs deleted: {songsDeleted}");
                }

                // Next, delete associated albums
                string deleteAlbumsQuery = "DELETE FROM dbo.Album WHERE ArtistId = @artistId";
                using (SqlCommand deleteAlbumsCommand = new SqlCommand(deleteAlbumsQuery, connection))
                {
                    deleteAlbumsCommand.Parameters.AddWithValue("@artistId", artistId);
                    int albumsDeleted = deleteAlbumsCommand.ExecuteNonQuery();
                    Console.WriteLine($"Album deleted: {albumsDeleted}");
                }

                // Finally, delete the artist
                string deleteArtistQuery = "DELETE FROM dbo.Artist WHERE ArtistId = @artistId";
                using (SqlCommand deleteArtistCommand = new SqlCommand(deleteArtistQuery, connection))
                {
                    deleteArtistCommand.Parameters.AddWithValue("@artistId", artistId);
                    int artistDeleted = deleteArtistCommand.ExecuteNonQuery();
                    Console.WriteLine($"Artist '{artistNameToDelete}' deleted: {artistDeleted}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting artist: {ex.Message}");
            }
        }

        // Helper method to retrieve the artist name based on the artist ID
        public string GetArtistName(SqlConnection connection, int artistId)
        {
            string query = "SELECT Name FROM dbo.Artist WHERE ArtistId = @artistId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@artistId", artistId);
                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving artist name: {ex.Message}");
                }
                return null; // Return null if the artist name is not found or an error occurs
            }
        }


        // Helper method to retrieve the artistId based on the artistName
        private static int GetArtistId(SqlConnection connection, string artistName)
        {
            string query = "SELECT ArtistId FROM dbo.Artist WHERE Name = @artistName";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@artistName", artistName);
                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving artistId: {ex.Message}");
                }
                return -1; // Return -1 if artist is not found or an error occurs
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
