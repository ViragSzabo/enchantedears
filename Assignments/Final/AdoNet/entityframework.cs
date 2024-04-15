using System;
using System.Data.Entity;
using System.Linq;

namespace DBSpeedTest
{
    partial class Program
    {
        static void ProgramMain(string[] args)
        {
            EntityFramework.Execute(1);
            EntityFramework.Execute(1000);
            EntityFramework.Execute(100000);
            EntityFramework.Execute(1000000);
        }
    }

    internal class EntityFramework
    {
        public static void Execute(int numRows)
        {
            try
            {
                // SQL Server connection string
                using (var context = new ArtistContext())
                {
                    for (int i = 0; i < numRows; i++)
                    {
                        // Insert
                        var newArtistData = new Artist { Name = $"Billie Eilish {i}", Description = $"Pop {i}" };
                        context.Artists.Add(newArtistData);
                        context.SaveChanges();
                    }
                    Console.WriteLine($"{numRows} row(s) inserted.");

                    for (int i = 0; i < numRows; i++)
                    {
                        // Select
                        var selectArtist = context.Artists.FirstOrDefault(a => a.Name == $"Billie Eilish {i}");
                        if (selectArtist != null)
                        {
                            Console.WriteLine($"Name: {selectArtist.Name}, Description: {selectArtist.Description}");
                        }
                    }
                    Console.WriteLine($"Selected {numRows} row(s).");

                    for (int i = 0; i < numRows; i++)
                    {
                        // Update
                        var selectArtist = context.Artists.FirstOrDefault(a => a.Name == $"Billie Eilish {i}");
                        if (selectArtist != null)
                        {
                            selectArtist.Description = $"Goth-Pop {i}";
                            context.SaveChanges();
                        }
                    }
                    Console.WriteLine($"Updated {numRows} row(s).");

                    for (int i = 0; i < numRows; i++)
                    {
                        // Delete
                        var selectArtist = context.Artists.FirstOrDefault(a => a.Name == $"Billie Eilish {i}");
                        if (selectArtist != null)
                        {
                            context.Artists.Remove(selectArtist);
                            context.SaveChanges();
                        }
                    }
                    Console.WriteLine($"{numRows} row(s) deleted.");
                }
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