-- 1. How many EnchantedEars users there are?
SELECT COUNT(*) AS 'Total Users' FROM dbo.AppUser;

-- 2. How many EnchantedEars artists there are?
SELECT COUNT(*) AS 'Total Artists' FROM dbo.Artist;

-- 3. How many EnchantedEars subscriptions there are?
SELECT COUNT(*) AS 'Total Subscriptions' FROM dbo.Subscription;

-- 4. How many songs there are?
SELECT COUNT(*) AS 'Total Songs' FROM dbo.Song;

-- 5. What the average user age is?
SELECT AVG(DATEDIFF(YEAR, BirthDate, GETDATE())) AS 'AverageAge' FROM dbo.AppUser;

-- 6. What the combined age of all users is?
SELECT SUM(DATEDIFF(YEAR, BirthDate, GETDATE())) AS 'TotalAge' FROM dbo.AppUser;

-- 7. How many different playlists there are in the database?
SELECT COUNT(DISTINCT PlaylistID) AS 'Playlists' FROM dbo.Playlist;

-- 8. How many single subscriptions there are?
SELECT COUNT(*) AS 'Single Subs' FROM dbo.Subscription WHERE Name = 'Single';

-- 9. How many family subscriptions there are?
SELECT COUNT(*) AS 'Family Subs' FROM dbo.Subscription WHERE Name = 'Family';

-- 10. How many items there are that users still want to listen?
SELECT COUNT(*) AS 'Songs Wanted' FROM dbo.Song WHERE SongID NOT IN (SELECT DISTINCT SongID FROM dbo.Listening);

-- 11. Which preferences can I set up as a user?
SELECT DISTINCT Genre AS 'Preference Options' FROM dbo.UserPreference;

-- 12. How many people have a subscription?
SELECT COUNT(DISTINCT UserID) AS 'People With Sub' FROM dbo.AppUser WHERE SubscriptionID IS NOT NULL;

-- 13. How many people are currently listening for free?
SELECT COUNT(*) AS 'People Without Sub' FROM dbo.AppUser WHERE SubscriptionID IS NULL;

-- 14. How many minutes users have listened in total?
SELECT SUM(Duration) AS 'TotalMinutes' FROM dbo.Listening;

-- 15. How often Hungarian songs have been listened?
SELECT COUNT(*) AS 'Hungarian Songs Listened' FROM dbo.Listening
JOIN dbo.Song ON dbo.Listening.SongID = dbo.Song.SongID
WHERE dbo.Song.Language = 'Hungarian';

-- 16. How often English songs have been listened to?
SELECT COUNT(*) AS 'English  Songs Listened' FROM dbo.Listening
JOIN dbo.Song ON dbo.Listening.SongID = dbo.Song.SongID
WHERE dbo.Song.Language = 'English';

-- 17. How often German songs have been listened to?
SELECT COUNT(*) AS 'German  Songs Listened' FROM dbo.Listening
JOIN dbo.Song ON dbo.Listening.SongID = dbo.Song.SongID
WHERE dbo.Song.Language = 'German';

-- 18. How often Dutch songs have been listened to?
SELECT COUNT(*) AS 'Dutch  Songs Listened' FROM dbo.Listening
JOIN dbo.Song ON dbo.Listening.SongID = dbo.Song.SongID
WHERE dbo.Song.Language = 'Dutch';

-- 19. Which users have listened to the song “Stenden”?
SELECT DISTINCT u.Username AS 'Users listened "Stenden"' FROM dbo.Listening l
JOIN dbo.AppUser u ON l.UserID = u.UserID
JOIN dbo.Song s ON l.SongID = s.SongID
WHERE s.Title = 'Stenden';

-- 20. How many users prefer metal?
SELECT COUNT(*) AS 'Users Preferring Metal' FROM dbo.UserPreference WHERE Genre = 'Metal';

-- 21. Which users have not yet listened to any songs?
SELECT Username AS 'Users Without Listening' 
FROM dbo.AppUser
WHERE UserID NOT IN (SELECT DISTINCT UserID FROM dbo.Listening);

-- 22. How many euros are earned per month?
SELECT SUM(Price) AS 'Total Earnings' 
FROM dbo.Subscription;

-- 23. How many extra euros can be earned per month when all users who do not subscribe would take out a subscription?
SELECT COUNT(*) * 3.0 AS 'PotentialRevenue' 
FROM dbo.AppUser 
WHERE SubscriptionID IS NULL;

-- 24. Which users who do not have a single subscription have listened to Dutch songs?
SELECT DISTINCT u.Username AS 'Users without sub listened Dutch songs'
FROM dbo.AppUser u
JOIN dbo.Listening l ON u.UserID = l.UserID
JOIN dbo.Song s ON l.SongID = s.SongID
WHERE u.SubscriptionID IS NULL
AND s.Language = 'Dutch';

-- 25. What the percentage is that has been used to listen to German songs?
SELECT 
  CASE 
    WHEN (SELECT COUNT(*) FROM dbo.Listening) = 0 THEN 0
    ELSE (CAST(COUNT(*) AS FLOAT) / (SELECT COUNT(*) FROM dbo.Listening)) * 100
  END AS 'Percentage'
FROM dbo.Listening l
JOIN dbo.Song s ON l.SongID = s.SongID
WHERE s.Language = 'German';

-- 26. What the most listened-to genre is for each user, excluding Dutch?
SELECT 
  l.UserID AS 'User ID', 
  s.Genre AS 'Genre', 
  COUNT(*) AS 'Listening Count',
  CASE 
    WHEN COUNT(*) = 0 THEN 'No data available for this language/genre.'
    ELSE 'Data found.'
  END AS 'Explanation'
FROM dbo.Listening l
JOIN dbo.Song s ON l.SongID = s.SongID
WHERE s.Language != 'Dutch'
GROUP BY l.UserID, s.Genre
ORDER BY COUNT(*) DESC;

-- 27. What the most listened-to genre is for each user, excluding English?
SELECT 
  l.UserID AS 'User ID', 
  s.Genre AS 'Genre', 
  COUNT(*) AS 'Listening Count',
  CASE 
    WHEN COUNT(*) = 0 THEN 'No data available for this language/genre.'
    ELSE 'Data found.'
  END AS 'Explanation'
FROM dbo.Listening l
JOIN dbo.Song s ON l.SongID = s.SongID
WHERE s.Language != 'English'
GROUP BY l.UserID, s.Genre
ORDER BY COUNT(*) DESC;

-- 28. What the most listened-to genre is for each user, excluding German?
SELECT 
  l.UserID AS 'User ID', 
  s.Genre AS 'Genre', 
  COUNT(*) AS 'Listening Count',
  CASE 
    WHEN COUNT(*) = 0 THEN 'No data available for this language/genre.'
    ELSE 'Data found.'
  END AS 'Explanation'
FROM dbo.Listening l
JOIN dbo.Song s ON l.SongID = s.SongID
WHERE s.Language != 'German'
GROUP BY l.UserID, s.Genre
ORDER BY COUNT(*) DESC;

-- 29. What the most listened-to genre is for each user, excluding Hungarian?
SELECT 
  l.UserID AS 'User ID', 
  s.Genre AS 'Genre', 
  COUNT(*) AS 'Listening Count',
  CASE 
    WHEN COUNT(*) = 0 THEN 'No data available for this language/genre.'
    ELSE 'Data found.'
  END AS 'Explanation'
FROM dbo.Listening l
JOIN dbo.Song s ON l.SongID = s.SongID
WHERE s.Language != 'Hungarian'
GROUP BY l.UserID, s.Genre
ORDER BY COUNT(*) DESC;

-- 30. Which of the following songs are listened to most on Valentine's Day?
SELECT 
  s.Title, 
  COUNT(*) AS 'ListenCount',
  CASE 
    WHEN COUNT(*) = 0 THEN 'No songs listened to on Valentines Day.'
    ELSE 'Data available for Valentines Day.'
  END AS 'Explanation'
FROM dbo.Listening l
JOIN dbo.Song s ON l.SongID = s.SongID
WHERE l.ListenDate = '2025-02-14'
GROUP BY s.Title
ORDER BY COUNT(*) DESC;
-- 31. Which of the following playlists are listened to most on Valentine's Day?
SELECT 
  p.Name, 
  COUNT(*) AS 'ListenCount',
  CASE 
    WHEN COUNT(*) = 0 THEN 'No playlists listened to on Valentines Day'
    ELSE 'Data available for Valentines Day.'
  END AS 'Explanation'
FROM dbo.Listening l
JOIN dbo.Song s ON l.SongID = s.SongID
JOIN dbo.PlaylistSong ps ON ps.SongID = s.SongID
JOIN dbo.Playlist p ON p.PlaylistID = ps.PlaylistID
WHERE l.ListenDate = '2025-02-14'
GROUP BY p.Name
ORDER BY COUNT(*) DESC;

-- 32. Return a list ordered by amount of listenings descending; Romantic, Pop, Rock, Classic, Movie, and KPOP.
SELECT 
  s.Genre, 
  COUNT(*) AS ListenCount,
  CASE 
    WHEN COUNT(*) = 0 THEN 'No listens found in this genre.'
    ELSE 'Genre data available.'
  END AS 'Explanation'
FROM dbo.Listening l
JOIN dbo.Song s ON l.SongID = s.SongID
WHERE s.Genre IN ('Romantic', 'Pop', 'Rock', 'Classic', 'Movie', 'KPOP')
GROUP BY s.Genre
ORDER BY 
  CASE s.Genre
    WHEN 'Romantic' THEN 1
    WHEN 'Pop' THEN 2
    WHEN 'Rock' THEN 3
    WHEN 'Classic' THEN 4
    WHEN 'Movie' THEN 5
    WHEN 'KPOP' THEN 6
    ELSE 7
  END,
  ListenCount DESC;