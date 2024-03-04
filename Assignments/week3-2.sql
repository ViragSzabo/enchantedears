-- 1. How many EnchantedEars users there are?
SELECT COUNT(UserID) AS 'Total number of users' FROM AppUser

-- 2. How many EnchantedEars artists there are?
SELECT COUNT(*) AS 'Total number of artists' FROM dbo.Artist;

-- 3. How many EnchantedEars subscriptions there are?
SELECT COUNT(*) AS 'Total number of subscriptions' FROM dbo.Subscription;

-- 4. How many songs there are?
SELECT COUNT(*) AS TotalSongs FROM dbo.Song;

-- 5. What the average user age is? (Assuming age is a column in the AppUser table)
-- Update table, add birthdate

-- 6. What the combined age of all users is? (Assuming age is a column in the AppUser table)
-- Update table, add birthdate

-- 7. How many different playlists there are in the database?
SELECT COUNT(DISTINCT PlaylistID) AS TotalPlaylists FROM dbo.Playlist;

-- 8. How many single subscriptions there are?
SELECT COUNT(*) AS TotalSingleSubscriptions FROM dbo.Subscription WHERE CHARINDEX('Single', Name) > 0;

-- 9. How many family subscriptions there are?
SELECT COUNT(*) AS TotalFamilySubscriptions FROM dbo.Subscription WHERE CHARINDEX('Family', Name) > 0;

-- 10. How many items there are that users still want to listen? (Assuming there's a "WantToListen" column)
-- Add column wanttolisten: SELECT COUNT(*) AS ItemsToListen FROM dbo.AppUser WHERE WantToListen = 1;

-- 11. Which preferences I can set up as a user?
-- Add userpreferences: SELECT DISTINCT Preference FROM dbo.UserPreferences;

-- 12. How many people have a subscription?
SELECT COUNT(*) AS SubscribedUsers FROM dbo.AppUser WHERE SubscriptionID IS NOT NULL;

-- 13. How many people are currently listening for free?
SELECT COUNT(*) AS FreeListeners FROM dbo.AppUser WHERE SubscriptionID IS NULL;

-- 14. How many minutes users have listened in total?
-- Add listeningMinutes: SELECT SUM(ListeningMinutes) AS TotalListeningMinutes FROM dbo.UserListeningHistory;

-- 15. How often Hungarian songs have been listened?
SELECT COUNT(*) AS HungarianListenings FROM dbo.Song WHERE Genre = 'Hungarian';

-- 16. How often English songs have been listened to?
SELECT COUNT(*) AS EnglishListenings FROM dbo.Song WHERE Genre = 'English';

-- 17. How often German songs have been listened to?
SELECT COUNT(*) AS GermanListenings FROM dbo.Song WHERE Genre = 'German';

-- 18. How often Dutch songs have been listened to?
SELECT COUNT(*) AS DutchListenings FROM dbo.Song WHERE Genre = 'Dutch';


-- 17. How often pop songs have been listened to?
SELECT COUNT(*) AS DancePopSongs FROM dbo.Song WHERE Genre = 'Dance Pop';

-- 18. How often dance pop songs have been listened to?
SELECT COUNT(*) AS PopSongs FROM dbo.Song WHERE Genre = 'Pop';

-- 19. Which users have listened to the song “Stenden”?
-- Add userlisteninghistory: SELECT Username
-- FROM dbo.AppUser AU
--	JOIN dbo.UserListeningHistory ULH ON AU.UserID = ULH.UserID
--	JOIN dbo.Song S ON ULH.SongID = S.SongID
-- WHERE S.Title = 'Stenden';

-- 20. How many users prefer metal?
-- Add preferences and metal song(s): SELECT COUNT(*) AS MetalListeners FROM dbo.AppUser WHERE Preference = 'Metal';

-- 21. Which users have not yet listened to any songs?
-- Add userlisteninghistory: SELECT Username
-- FROM dbo.AppUser AU
-- WHERE NOT EXISTS (
--    SELECT 1
--    FROM dbo.UserListeningHistory ULH
--    WHERE AU.UserID = ULH.UserID
-- );

-- 22. How many euros are earned per month? (Assuming there's a SubscriptionFee column)
-- Add subscriptionhistory table: SELECT MONTH(StartDate) AS Month, SUM(SubscriptionFee) AS MonthlyEarnings
-- FROM dbo.SubscriptionHistory
-- GROUP BY MONTH(StartDate);

-- 23. How many extra euros can be earned per month when all users who do not subscribe would take out a subscription?
-- Add subscriptionhistory table: SELECT MONTH(StartDate) AS Month, COUNT(*) * SubscriptionFee AS PotentialMonthlyEarnings
-- FROM dbo.AppUser
-- WHERE SubscriptionID IS NULL
-- GROUP BY MONTH(StartDate);

-- 24. Which users who do not have a single subscription have listened to Dutch songs?
-- SELECT DISTINCT AU.Username
-- FROM dbo.AppUser AU
-- JOIN dbo.UserListeningHistory ULH ON AU.UserID = ULH.UserID
-- JOIN dbo.Song S ON ULH.SongID = S.SongID
-- WHERE S.Genre = 'Dutch' AND AU.SubscriptionID IS NULL;

-- 25. What the percentage is that has been used to listen to German songs?
-- SELECT (COUNT(*) * 100.0) / (SELECT COUNT(*) FROM dbo.UserListeningHistory) AS PercentageGermanListenings
-- FROM dbo.Song
-- WHERE Genre = 'German';

-- 26. What the most listened-to genre is for each user, excluding Dutch?
-- SELECT AU.Username, MAX(Genre) AS MostListenedGenre
-- FROM dbo.AppUser AU
-- JOIN dbo.UserListeningHistory ULH ON AU.UserID = ULH.UserID
-- JOIN dbo.Song S ON ULH.SongID = S.SongID
-- WHERE S.Genre != 'Dutch'
-- GROUP BY AU.Username;

-- 27. What the most listened-to genre is for each user, excluding English?
-- SELECT AU.Username, MAX(Genre) AS MostListenedGenre
-- FROM dbo.AppUser AU
-- JOIN dbo.UserListeningHistory ULH ON AU.UserID = ULH.UserID
-- JOIN dbo.Song S ON ULH.SongID = S.SongID
-- WHERE S.Genre != 'English'
-- GROUP BY AU.Username;

-- 28. What the most listened-to genre is for each user, excluding German?
-- SELECT AU.Username, MAX(Genre) AS MostListenedGenre
-- FROM dbo.AppUser AU
-- JOIN dbo.UserListeningHistory ULH ON AU.UserID = ULH.UserID
-- JOIN dbo.Song S ON ULH.SongID = S.SongID
-- WHERE S.Genre != 'German'
-- GROUP BY AU.Username;

-- 29. What the most listened-to genre is for each user, excluding Hungarian?
-- SELECT AU.Username, MAX(Genre) AS MostListenedGenre
-- FROM dbo.AppUser AU
-- JOIN dbo.UserListeningHistory ULH ON AU.UserID = ULH.UserID
-- JOIN dbo.Song S ON ULH.SongID = S.SongID
-- WHERE S.Genre != 'Hungarian'
-- GROUP BY AU.Username;

-- 30. Which of the following songs are listened to most on Valentine's Day?
-- SELECT S.Title, COUNT(*) AS Listenings
-- FROM dbo.UserListeningHistory ULH
-- JOIN dbo.Song S ON ULH.SongID = S.SongID
-- WHERE CONVERT(DATE, ULH.ListeningDate) = '2024-02-14'
-- GROUP BY S.Title
-- ORDER BY Listenings DESC;

-- 31. Which of the following playlists are listened to most on Valentine's Day?
-- SELECT P.Name, COUNT(*) AS Listenings
-- FROM dbo.UserListeningHistory ULH
-- JOIN dbo.PlaylistSong PS ON ULH.SongID = PS.SongID
-- JOIN dbo.Playlist P ON PS.PlaylistID = P.PlaylistID
-- WHERE CONVERT(DATE, ULH.ListeningDate) = '2024-02-14'
-- GROUP BY P.Name
-- ORDER BY Listenings DESC;