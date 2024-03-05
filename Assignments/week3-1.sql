USE enchantedears;
GO

-- Add test data for the AppUser table
INSERT INTO dbo.Subscription (Name, Price)
VALUES
('Single', 3.0),
('Family', 2.0);

INSERT INTO dbo.Artist (Name, Description)
VALUES
('Taylor Swift', 'An artist of Pop.'),
('MIKA', 'An artist of Dance Pop.');

INSERT INTO dbo.AppUser (Username, Email, Password, SubscriptionID)
VALUES 
('DrSheldonCooper', 'sheldon.cooper@gmail.com', '32trainNerD49', 16),
('LeonardHofstadter', 'leonard.hofstadter@gmail.com', 'Kal-el', 16),
('WizardWolowitz', 'wizard.wolowitz@gmail.com', 'Wolowitz-0231', 16),
('RajKoothrappali', 'raj.koothrappali@gmail.com', 'StarS304Beyonce', 15);

-- Insert an album for Taylor Swift
INSERT INTO dbo.Album (Title, ReleaseDate, ArtistID)
VALUES ('1989', '2014-10-27', (SELECT ArtistID FROM dbo.Artist WHERE Name = 'Taylor Swift'));

-- Insert songs for the Taylor Swift album
INSERT INTO dbo.Song (Title, ArtistID, Genre, ReleaseDate, AlbumID, Duration)
VALUES
('Shake It Off', (SELECT ArtistID FROM dbo.Artist WHERE Name = 'Taylor Swift'), 'Pop', '2014-08-18', (SELECT AlbumID FROM dbo.Album WHERE Title = '1989'), 220),
('Blank Space', (SELECT ArtistID FROM dbo.Artist WHERE Name = 'Taylor Swift'), 'Pop', '2014-11-10', (SELECT AlbumID FROM dbo.Album WHERE Title = '1989'), 231),
('Bad Blood', (SELECT ArtistID FROM dbo.Artist WHERE Name = 'Taylor Swift'), 'Pop', '2015-05-17', (SELECT AlbumID FROM dbo.Album WHERE Title = '1989'), 213);

-- Insert an album for MIKA
INSERT INTO dbo.Album (Title, ReleaseDate, ArtistID)
VALUES ('Life In Cartoon Motion', '2007-02-05', (SELECT ArtistID FROM dbo.Artist WHERE Name = 'MIKA'));

-- Insert songs for the MIKA album
INSERT INTO dbo.Song (Title, ArtistID, Genre, ReleaseDate, AlbumID, Duration)
VALUES
('Grace Kelly', (SELECT ArtistID FROM dbo.Artist WHERE Name = 'MIKA'), 'Dance Pop', '2006-12-04', (SELECT AlbumID FROM dbo.Album WHERE Title = 'Life In Cartoon Motion'), 203),
('Relax, Take It Easy', (SELECT ArtistID FROM dbo.Artist WHERE Name = 'MIKA'), 'Dance Pop', '2007-06-04', (SELECT AlbumID FROM dbo.Album WHERE Title = 'Life In Cartoon Motion'), 226),
('Love Today', (SELECT ArtistID FROM dbo.Artist WHERE Name = 'MIKA'), 'Dance Pop', '2007-02-16', (SELECT AlbumID FROM dbo.Album WHERE Title = 'Life In Cartoon Motion'), 208);

-- Create a playlist with all songs from Taylor Swift and MIKA
INSERT INTO dbo.Playlist (Name, Description)
VALUES ('Taylor Swift & MIKA Playlist', 'A playlist with all songs from Taylor Swift and MIKA');

-- Add songs from Taylor Swift to the playlist
INSERT INTO dbo.PlaylistSong (PlaylistID, SongID)
SELECT (SELECT PlaylistID FROM dbo.Playlist WHERE Name = 'Taylor Swift & MIKA Playlist'), SongID
FROM dbo.Song
WHERE ArtistID = (SELECT ArtistID FROM dbo.Artist WHERE Name = 'Taylor Swift');

-- Add songs from MIKA to the playlist
INSERT INTO dbo.PlaylistSong (PlaylistID, SongID)
SELECT (SELECT PlaylistID FROM dbo.Playlist WHERE Name = 'Taylor Swift & MIKA Playlist'), SongID
FROM dbo.Song
WHERE ArtistID = (SELECT ArtistID FROM dbo.Artist WHERE Name = 'MIKA');

UPDATE dbo.Song SET PlaylistID = 1

-- Verify the inserted data
SELECT * FROM dbo.AppUser
SELECT * FROM dbo.Artist
SELECT * FROM dbo.Subscription 
SELECT * FROM dbo.Playlist
SELECT * FROM dbo.PlaylistSong
SELECT * FROM dbo.Song
SELECT * FROM dbo.Album