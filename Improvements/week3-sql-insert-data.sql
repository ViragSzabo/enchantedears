INSERT INTO dbo.Subscription (Name, Price)
VALUES
('Single', 3.0),
('Family', 2.0);

INSERT INTO dbo.AppUser (Username, Email, Password, BirthDate, Country, PreferredGenre, SubscriptionID)
VALUES 
('FrodoBaggins', 'frodo@shiremail.com', 'OneRing!', '1990-09-22', 'UK', 'Folk', 15),
('AragornStrider', 'aragorn@gondor.net', 'HeirOfIsildur', '1985-03-01', 'Germany', 'Rock', 15),
('LeiaOrgana', 'leia@rebellion.org', 'Hope2025', '1993-10-21', 'USA', 'Pop', 16),
('TonyStark', 'tony.stark@starkindustries.com', 'IamIronMan', '1980-05-29', 'Netherlands', 'Metal', NULL),
('LunaLovegood', 'luna@hogwarts.ac.uk', 'RadishEarrings', '1996-02-13', 'Hungary', 'Classic', NULL);

INSERT INTO dbo.Artist (Name, Description)
VALUES 
('BTS', 'A K-pop group from South Korea.'),
('Queen', 'A legendary British rock band.'),
('Ludwig van Beethoven', 'A classical composer from Germany.'),
('Hans Zimmer', 'German composer known for movie soundtracks.'),
('Luis Fonsi', 'Puerto Rican singer known for Latin pop hits.'),
('Adele', 'British artist known for emotional ballads.');

-- BTS Album
INSERT INTO dbo.Album (Title, ReleaseDate, ArtistID)
VALUES ('Map of the Soul: 7', '2020-02-21', (SELECT TOP 1 ArtistID FROM dbo.Artist WHERE Name = 'BTS'));

-- Queen Album
INSERT INTO dbo.Album (Title, ReleaseDate, ArtistID)
VALUES ('A Night at the Opera', '1975-11-21', (SELECT TOP 1 ArtistID FROM dbo.Artist WHERE Name = 'Queen'));

-- Beethoven Compilation
INSERT INTO dbo.Album (Title, ReleaseDate, ArtistID)
VALUES ('Beethoven Essentials', '1801-01-01', (SELECT TOP 1 ArtistID FROM dbo.Artist WHERE Name = 'Ludwig van Beethoven'));

-- Hans Zimmer Album
INSERT INTO dbo.Album (Title, ReleaseDate, ArtistID)
VALUES ('Inception OST', '2010-07-13', (SELECT TOP 1 ArtistID FROM dbo.Artist WHERE Name = 'Hans Zimmer'));

-- Luis Fonsi Album
INSERT INTO dbo.Album (Title, ReleaseDate, ArtistID)
VALUES ('Vida', '2019-02-01', (SELECT TOP 1 ArtistID FROM dbo.Artist WHERE Name = 'Luis Fonsi'));

-- Adele Album
INSERT INTO dbo.Album (Title, ReleaseDate, ArtistID)
VALUES ('25', '2015-11-20', (SELECT TOP 1 ArtistID FROM dbo.Artist WHERE Name = 'Adele'));

INSERT INTO dbo.Song (Title, ArtistID, Genre, ReleaseDate, AlbumID, Duration, Language)
VALUES
-- BTS
('ON',
 (SELECT TOP 1 ArtistID FROM dbo.Artist WHERE Name = 'BTS'),
 'K-pop', '2020-02-21',
 (SELECT TOP 1 AlbumID FROM dbo.Album WHERE Title = 'Map of the Soul: 7'),
 299, 'Korean'),

-- Queen
('Bohemian Rhapsody',
 (SELECT TOP 1 ArtistID FROM dbo.Artist WHERE Name = 'Queen'),
 'Rock', '1975-10-31',
 (SELECT TOP 1 AlbumID FROM dbo.Album WHERE Title = 'A Night at the Opera'),
 354, 'English'),

-- Beethoven
('Moonlight Sonata',
 (SELECT TOP 1 ArtistID FROM dbo.Artist WHERE Name = 'Ludwig van Beethoven'),
 'Classical', '1801-01-01',
 (SELECT TOP 1 AlbumID FROM dbo.Album WHERE Title = 'Beethoven Essentials'),
 900, 'Instrumental'),

-- Hans Zimmer
('Time',
 (SELECT TOP 1 ArtistID FROM dbo.Artist WHERE Name = 'Hans Zimmer'),
 'Soundtrack', '2010-07-13',
 (SELECT TOP 1 AlbumID FROM dbo.Album WHERE Title = 'Inception OST'),
 270, 'Instrumental'),

-- Luis Fonsi
('Despacito',
 (SELECT TOP 1 ArtistID FROM dbo.Artist WHERE Name = 'Luis Fonsi'),
 'Latin Pop', '2017-01-13',
 (SELECT TOP 1 AlbumID FROM dbo.Album WHERE Title = 'Vida'),
 229, 'Spanish'),

-- Adele
('Hello',
 (SELECT TOP 1 ArtistID FROM dbo.Artist WHERE Name = 'Adele'),
 'Romantic', '2015-10-23',
 (SELECT TOP 1 AlbumID FROM dbo.Album WHERE Title = '25'),
 295, 'English');