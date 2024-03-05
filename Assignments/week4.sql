USE enchantedears;
GO

-- Add indexes to columns where logical
CREATE INDEX IX_Artist_Name ON Artist(Name);
CREATE INDEX IX_Album_ReleaseDate ON Album(ReleaseDate);
CREATE INDEX IX_Song_Genre ON Song(Genre);
CREATE INDEX IX_Song_ReleaseDate ON Song(ReleaseDate);

-- Add constraints to dbo.AppUser
-- Check if the password is >= 8
ALTER TABLE AppUser ADD CONSTRAINT CK_Password_Length CHECK (LEN(Password) >= 8);

-- Add constraints to dbo.Song
-- Check non-negative duration
ALTER TABLE Song ADD CONSTRAINT CK_Duration CHECK (Duration >= 0);

-- Add constraints to dbo.AppUser
ALTER TABLE AppUser ADD CONSTRAINT UQ_Email UNIQUE (Email);

-- Add views
-- View 1: View to get information about users and their subscriptions
CREATE VIEW [usw_UserSubscriptions] AS 
SELECT au.UserID, au.Username, au.Email, s.Name AS SubscriptionName
FROM AppUser au
JOIN Subscription s ON au.SubscriptionID = s.SubscriptionID
WHERE au.SubscriptionID IS NOT NULL;

-- View 2: View to get information about playlists and their songs
CREATE VIEW [psw_PlaylistSongs] AS
SELECT p.PlaylistID, p.Name AS PlaylistName, s.Title AS SongTitle
FROM Playlist p
JOIN dbo.PlaylistSong ps ON p.PlaylistID = ps.PlaylistID
JOIN dbo.Song s ON ps.SongID = s.SongID;

-- Add Stored Procedure 1: Add a new user
CREATE PROCEDURE aup_AddUser
	@Username VARCHAR(50),
	@Email VARCHAR(100),
	@Password VARCHAR(100),
	@SubscriptionID INT
AS
BEGIN
	INSERT INTO AppUser(Username, Email, Password, SubscriptionID)
	VALUES(@Username, @Email, @Password, @SubscriptionID);
END;

-- Stored Procedure 2: Get playlist information
CREATE PROCEDURE sp_GetPlaylistInfo
	@PlaylistID INT
AS
BEGIN
	SELECT
		p.Name AS PlaylistName,
		p.Title AS SongTitle
	FROM Playlist p
	JOIN PlaylistSong ps ON p.PlaylistID = ps.PlaylistID
	JOIN Song s ON ps.SongID = s.SongID
	WHERE p.PlaylistID = @PlaylistID;
END;

-- Trigger 1: Ensure no duplicate email addresses in the AppUser table
CREATE TRIGGER tr_PreventDuplicateEmail ON AppUser
AFTER INSERT, UPDATE
AS
BEGIN
	IF EXISTS (SELECT 1 FROM inserted i1 INNER JOIN dbo.AppUser u ON i1.Email = u.Email WHERE i1.UserID <> u.UserID)
	BEGIN
		RAISEERROR('Duplicate email address detected.', 16, 1);
		ROLLBACK;
	END;
END;


-- Trigger 2a: Update Song count in Playlist when a new song is added
CREATE TRIGGER tr_InsertUpdatePlaylistSongCount ON dbo.PlaylistSong
AFTER INSERT
AS
BEGIN
    UPDATE ps
    SET SongCount = (
        SELECT COUNT(*)
        FROM dbo.PlaylistSong
        WHERE PlaylistID = ps.PlaylistID
    )
    FROM dbo.PlaylistSong ps
    INNER JOIN inserted i ON ps.PlaylistID = i.PlaylistID;
END;

-- Trigger 2b: Update Song count in Playlist when a song is deleted
CREATE TRIGGER tr_DeleteUpdatePlaylistSongCount ON dbo.PlaylistSong
AFTER DELETE
AS
BEGIN
    UPDATE ps
    SET SongCount = (
        SELECT COUNT(*)
        FROM dbo.PlaylistSong
        WHERE PlaylistID = ps.PlaylistID
    )
    FROM dbo.PlaylistSong ps
    INNER JOIN deleted d ON ps.PlaylistID = d.PlaylistID;
END;

-- Implement referential integrity for foreign key constraints
-- (Assuming SubscriptionID is the foreign key in AppUser referencing Subscription)
ALTER TABLE AppUser
ADD CONSTRAINT FK_Subscription
FOREIGN KEY (SubscriptionID)
REFERENCES Subscription(SubscriptionID)
ON DELETE CASCADE
ON UPDATE CASCADE;