USE enchantedears;
GO

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
