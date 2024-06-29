using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace DBSpeedTest
{
    partial class Program
    {
        static void MMain(string[] args)
        {
            //EntityFramework.Execute(1);
            //EntityFramework.Execute(1000);
            //EntityFramework.Execute(10000);
            EntityFramework.Execute(1000000);
        }
    }

    internal class EntityFramework
    {
        public static void Execute(int numRows)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                using (var context = new ArtistContext())
                {
                    // Insert
                    for (int i = 0; i < numRows; i += 1000)
                    {
                        for (int j = 0; j < 1000 && i + j < numRows; j++)
                        {
                            var newArtistData = new Artist { Name = $"Taylor Swift {i + j}", Description = $"Pop {i + j}" };
                            context.Artists.Add(newArtistData);
                        }
                        context.SaveChanges();
                        context.Artists.Local.Clear(); // Clear the local cache to reduce memory usage
                    }
                    Console.WriteLine($"{numRows} row(s) inserted.");
                    Console.WriteLine($"Insert Time: {stopwatch.ElapsedMilliseconds} ms");

                    // Select
                    for (int i = 0; i < numRows; i += 1000)
                    {
                        var artistNames = Enumerable.Range(i, Math.Min(1000, numRows - i)).Select(j => $"Taylor Swift {j}");
                        var selectArtists = context.Artists.Where(a => artistNames.Contains(a.Name)).ToList();
                    }
                    Console.WriteLine($"Selected {numRows} row(s).");
                    Console.WriteLine($"Select Time: {stopwatch.ElapsedMilliseconds} ms");

                    // Update
                    for (int i = 0; i < numRows; i += 1000)
                    {
                        var artistNames = Enumerable.Range(i, Math.Min(1000, numRows - i)).Select(j => $"Taylor Swift {j}");
                        var selectArtists = context.Artists.Where(a => artistNames.Contains(a.Name)).ToList();

                        foreach (var selectArtist in selectArtists)
                        {
                            selectArtist.Description = $"Poetry Pop {selectArtist.Id}";
                        }
                        context.SaveChanges();
                        context.Artists.Local.Clear(); // Clear the local cache to reduce memory usage
                    }
                    Console.WriteLine($"Updated {numRows} row(s).");
                    Console.WriteLine($"Update Time: {stopwatch.ElapsedMilliseconds} ms");

                    // Delete
                    for (int i = 0; i < numRows; i += 1000)
                    {
                        var artistNames = Enumerable.Range(i, Math.Min(1000, numRows - i)).Select(j => $"Taylor Swift {j}");
                        var selectArtists = context.Artists.Where(a => artistNames.Contains(a.Name)).ToList();

                        context.Artists.RemoveRange(selectArtists);
                        context.SaveChanges();
                        context.Artists.Local.Clear(); // Clear the local cache to reduce memory usage
                    }
                    Console.WriteLine($"{numRows} row(s) deleted.");
                    Console.WriteLine($"Delete Time: {stopwatch.ElapsedMilliseconds} ms");
                }

                stopwatch.Stop();
                Console.WriteLine($"Total Time taken for {numRows} rows: {stopwatch.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ArtistContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>().HasKey(a => a.Id);
        }
    }
}
