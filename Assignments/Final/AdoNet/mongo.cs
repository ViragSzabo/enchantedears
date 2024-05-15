using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DBSpeedTest
{
    partial class Program
    {
        static void MonoMain(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            MongoDB mongoDB = new MongoDB();
            Song retrievedSong = null;

            stopwatch.Start();
            int numRows = 1000000;

            Stopwatch insert = new Stopwatch();
            insert.Start();
            for (int i = 0; i < numRows; i++)
            {
                retrievedSong = new Song()
                {
                    Title = "The Tortured Poets Department",
                    Artist = "Taylor Swift",
                    Genre = "Pop",
                    ReleaseDate = DateTime.UtcNow.AddDays(i),
                    Duration = TimeSpan.FromMinutes(4),
                    Album = "The Tortured Poets Department",
                };
                mongoDB.InsertSong(retrievedSong);
                insert.Stop();
                Console.WriteLine($"{numRows} song(s) inserted in {insert.ElapsedMilliseconds} ms.");
            }

            Stopwatch read = new Stopwatch();
            read.Start();
            // Example usage: Get songs by their IDs
            ObjectId songId = mongoDB.GetSongOrPlaylistBy(retrievedSong.Id);
            for (int i = 0; i < numRows; i++)
            {
                if (retrievedSong != null)
                {
                    Console.WriteLine($"Retrieved {numRows} song.");
                }
                else
                {
                    Console.WriteLine($"Song with title 'Example Song {retrievedSong.Title}' was not found.");
                }
            }
            Console.WriteLine($"{numRows} song was read in {read.ElapsedMilliseconds} ms.");
            read.Stop();

            Stopwatch update = new Stopwatch();
            update.Start();
            // Example usage: Update songs
            for (int i = 0; i < numRows; i++)
            {
                if (retrievedSong != null)
                {
                    retrievedSong.Title = $"Updated Example Song {i}";
                    mongoDB.UpdateSong(retrievedSong.Id, retrievedSong);
                }
            }
            Console.WriteLine($"{numRows} song(s) updated in {update.ElapsedMilliseconds} ms.");
            update.Stop();

            Stopwatch remove = new Stopwatch();
            remove.Start();
            // Example usage: Delete songs
            for (int i = 0; i < numRows; i++)
            {
                if (retrievedSong != null)
                {
                    mongoDB.DeleteSong(retrievedSong.Id);
                }
            }
            Console.WriteLine($"{numRows} song(s) deleted in {remove.ElapsedMilliseconds} ms.");
            remove.Stop();

            stopwatch.Stop();
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    internal class MongoDB
    {
        private MongoDBContext mongoDBContext;

        public MongoDB()
        {
            mongoDBContext = new MongoDBContext();
        }

        // Perform CRUD operations
        public void InsertSong(Song song)
        {
            mongoDBContext.Songs.InsertOne(song);
        }

        public void InsertPlaylist(Playlist playlist)
        {
            mongoDBContext.Playlists.InsertOne(playlist);
        }

        public ObjectId GetSongOrPlaylistBy(ObjectId id)
        {
            var song = mongoDBContext.Songs.Find(song => song.Id == id).FirstOrDefault();
            var playlist = mongoDBContext.Playlists.Find(playlist => playlist.Id == id).FirstOrDefault();

            if (song != null)
            {
                //Console.WriteLine($"{song.Title} was found from the {song.Album} album by {song.Artist}");
                return song.Id;
            }
            else
            {
                Console.WriteLine("Song not found.");
                return ObjectId.Empty;
                // Return a default ObjectId or handle null accordingly
            }
        }

        public void UpdateSong(ObjectId id, Song song)
        {
            var filter = Builders<Song>.Filter.Eq(s => s.Id, id);
            var update = Builders<Song>.Update
                .Set(s => s.Title, song.Title)
                .Set(s => s.Artist, song.Artist)
                .Set(s => s.Genre, song.Genre)
                .Set(s => s.ReleaseDate, song.ReleaseDate)
                .Set(s => s.Duration, song.Duration)
                .Set(s => s.Album, song.Album);

            mongoDBContext.Songs.UpdateOne(filter, update);
        }

        public void UpdatePlaylist(ObjectId id, Playlist playlist)
        {
            var filter = Builders<Playlist>.Filter.Eq(p => p.Id, id);
            var update = Builders<Playlist>.Update
                .Set(p => p.Name, playlist.Name)
                .Set(p => p.Description, playlist.Description)
                .Set(p => p.Songs, playlist.Songs);

            mongoDBContext.Playlists.UpdateOne(filter, update);
        }

        public void DeleteSong(ObjectId id)
        {
            mongoDBContext.Songs.DeleteOne(song => song.Id == id);
        }

        public void DeletePlaylist(ObjectId id)
        {
            mongoDBContext.Playlists.DeleteOne(playlist => playlist.Id == id);
        }
    }

    public class MongoDBContext
    {
        private IMongoDatabase mongoDatabase;

        public MongoDBContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            mongoDatabase = client.GetDatabase("enchantedears");
        }
        public IMongoCollection<Song> Songs => mongoDatabase.GetCollection<Song>("song");
        public IMongoCollection<Playlist> Playlists => mongoDatabase.GetCollection<Playlist>("Playlist");
    }

    public class Song
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string Album { get; set; }
    }

    public class Playlist
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ObjectId> Songs { get; set; }
    }

}
