using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Serializers;

namespace DBSpeedTest
{
    partial class Program
    {
        static void MongoMain(string[] args)
        {
            MongoDB mongoDB = new MongoDB();
            Song retrievedSong = new Song();
            ObjectId songId = mongoDB.GetSongOrPlaylistBy(retrievedSong.Id);

            // Example usage: Insert songs
            int numRows = 1000;
            for (int i = 0; i < numRows; i++)
            {
                Song newSong = new Song
                {
                    Title = $"Example Song {i}",
                    Artist = $"Example Artist {i}",
                    Genre = $"Example Genre {i}",
                    ReleaseDate = DateTime.UtcNow.AddDays(i),
                    Duration = TimeSpan.FromMinutes(4),
                    Album = $"Example Album {i}"
                };
                mongoDB.InsertSong(newSong);
            }
            Console.WriteLine($"{numRows} song(s) inserted.");

            // Example usage: Get songs by their IDs
            for (int i = 0; i < numRows; i++)
            {
                if (retrievedSong != null)
                {
                    Console.WriteLine($"Retrieved song: Title - {retrievedSong.Title}, Artist - {retrievedSong.Artist}");
                }
                else
                {
                    Console.WriteLine($"Song with title 'Example Song {i}' not found.");
                }
            }

            // Example usage: Update songs
            for (int i = 0; i < numRows; i++)
            {
                if (retrievedSong != null)
                {
                    retrievedSong.Title = $"Updated Example Song {i}";
                    mongoDB.UpdateSong(retrievedSong.Id, retrievedSong);
                }
            }
            Console.WriteLine($"{numRows} song(s) updated.");

            // Example usage: Delete songs
            for (int i = 0; i < numRows; i++)
            {
                if (retrievedSong != null)
                {
                    mongoDB.DeleteSong(retrievedSong.Id);
                }
            }
            Console.WriteLine($"{numRows} song(s) deleted.");
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
                Console.WriteLine($"Song found: Title - {song.Title}, Artist - {song.Artist}");
            }
            else
            {
                Console.WriteLine("Song not found.");
            }

            if (playlist != null)
            {
                Console.WriteLine($"Playlist found: Name - {playlist.Name}, Description - {playlist.Description}");
            }
            else
            {
                Console.WriteLine("Playlist not found.");
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
