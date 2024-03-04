USE enchantedears

CREATE TABLE Subscription (
    SubscriptionID INT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL
);

CREATE TABLE FamilySubscription (
    SubscriptionID INT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    MemberCount INT NOT NULL CHECK (MemberCount = 4)
);

CREATE TABLE AppUser (
    UserID INT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    SubscriptionID INT,
    FOREIGN KEY (SubscriptionID) REFERENCES Subscription(SubscriptionID)
);

CREATE TABLE Artist (
	ArtistID INT PRIMARY KEY,
	Name VARCHAR(100) NOT NULL,
	Description TEXT
);

CREATE TABLE Album (
    AlbumID INT PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    ReleaseDate DATE,
    ArtistID INT,
    FOREIGN KEY (ArtistID) REFERENCES Artist(ArtistID)
);

CREATE TABLE Playlist (
    PlaylistID INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description TEXT
);

CREATE TABLE Song (
    SongID INT PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    ArtistID INT,
    Genre VARCHAR(50),
    ReleaseDate DATE,
    Duration INT, -- Assuming duration is in seconds
    AlbumID INT,
    PlaylistID INT,
    FOREIGN KEY (ArtistID) REFERENCES Artist(ArtistID),
    FOREIGN KEY (AlbumID) REFERENCES Album(AlbumID),
    FOREIGN KEY (PlaylistID) REFERENCES Playlist(PlaylistID)
);

CREATE TABLE PlaylistSong (
    PlaylistID INT,
    SongID INT,
    PRIMARY KEY (PlaylistID, SongID),
    FOREIGN KEY (PlaylistID) REFERENCES Playlist(PlaylistID),
    FOREIGN KEY (SongID) REFERENCES Song(SongID)
);