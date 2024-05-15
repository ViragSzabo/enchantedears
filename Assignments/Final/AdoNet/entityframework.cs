using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace DBSpeedTest
{
    partial class Program
    {
        static void EMain(string[] args)
        {
            //EntityFramework.Execute(1);
            EntityFramework.Execute(1000);
            //EntityFramework.Execute(100000);
            //EntityFramework.Execute(1000000);
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

                // SQL Server connection string
                using (var context = new ArtistContext()) 
                {
                    for (int i = 0; i < numRows; i++)
                    {
                        // Insert
                        var newArtistData = new Artist { Name = $"Taylor Swift {i}", Description = $"Pop {i}" };
                        context.Artists.Add(newArtistData);
                        context.SaveChanges();
                    }
                    Console.WriteLine($"{numRows} row(s) inserted.");
                    Console.WriteLine(stopwatch.Elapsed);

                    for (int i = 0; i < numRows; i++)
                    {
                        // Select
                        var selectArtist = context.Artists.ToList().FirstOrDefault(a => a.Name == $"Taylor Swift {i}");
                        if (selectArtist != null)
                        {
                            //Console.WriteLine($"Name: {selectArtist.Name}, Description: {selectArtist.Description}");
                        }
                    }
                    Console.WriteLine($"Selected {numRows} row(s).");
                    Console.WriteLine(stopwatch.Elapsed);

                    for (int i = 0; i < numRows; i++)
                    {
                        // Update
                        var selectArtist = context.Artists.ToList().FirstOrDefault(a => a.Name == $"Taylor Swift {i}");
                        if (selectArtist != null)
                        {
                            selectArtist.Description = $"Poetry Pop {i}";
                            context.SaveChanges();
                        }
                    }
                    Console.WriteLine($"Updated {numRows} row(s).");
                    Console.WriteLine(stopwatch.Elapsed);

                    for (int i = 0; i < numRows; i++)
                    {
                        // Delete
                        var selectArtist = context.Artists.ToList().FirstOrDefault(a => a.Name == $"Taylor Swift {i}");
                        if (selectArtist != null)
                        {
                            context.Artists.Remove(selectArtist);
                            context.SaveChanges();
                        }
                    }
                    Console.WriteLine($"{numRows} row(s) deleted.");
                    Console.WriteLine(stopwatch.Elapsed);
                }

                stopwatch.Stop();
                Console.WriteLine($"Time taken for {numRows} rows: {stopwatch.ElapsedMilliseconds} ms");
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