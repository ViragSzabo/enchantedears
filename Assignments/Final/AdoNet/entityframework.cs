using System;
using System.Data.Entity;
using System.Linq;

namespace DBSpeedTest
{
    partial class Program
    {
        static void ProgramMain(string[] args)
        {
            EntityFramework.Execute();
        }
    }

    internal class EntityFramework
    {
        public static void Execute()
        {
            try
            {
                // Insert
                ArtistContext context = new ArtistContext();
                var newArtistData = new Artist { Name = "Billie Eilish", Description = "Pop" };
                context.Artists.Add(newArtistData);
                context.SaveChanges();
                Console.WriteLine($"{newArtistData.Name} added.");

                // Select
                var selectArtist = context.Artists.FirstOrDefault(a => a.Name == "Billie Eilish");
                if (selectArtist != null)
                {
                    Console.WriteLine($"Name: {selectArtist.Name}, Description: {selectArtist.Description}");
                }

                // Update
                selectArtist.Description = "Goth-Pop";
                context.SaveChanges();
                Console.WriteLine($"Description updated for {selectArtist.Name}.");

                // Delete
                context.Artists.Remove(selectArtist);
                context.SaveChanges();
                Console.WriteLine($"{selectArtist.Name} removed.");
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