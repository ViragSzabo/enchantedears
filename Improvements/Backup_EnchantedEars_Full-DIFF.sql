-- BACKUP, RECOVERY AND CONCURRENCY

-- Transaction
-- Isolation level: serializable
-- Serializable prevents phantom reads 
-- and ensures the highest level of isolation between transactions, 
-- preventing dirty, non-repeatable, and phantom reads.

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

BEGIN TRY
	BEGIN TRANSACTION

	-- 1. Insert a new subscription plan
	INSERT INTO Subscription (Name, Price)
	VALUES ('Monthly', 1.50);

	-- 1.5 Capture new subscription ID
	DECLARE @NewSubID INT;
	SET @NewSubID = SCOPE_IDENTITY();

	-- 2. Create a new user subscribed to the new plan
	INSERT INTO AppUser (Username, Email, Password, SubscriptionID)
	VALUES ('Chandler', 'chandler.bing@gmail.com', '12345', @NewSubID);

	-- 3. Create a new playlist and update its name
	INSERT INTO Playlist (Name, Description)
	VALUES ('Unknown', 'None');

	UPDATE Playlist 
	SET Name = 'Spring' 
	WHERE Name = 'Unknown';

	COMMIT;
END TRY
BEGIN CATCH
	ROLLBACK;
	PRINT 'Transaction failed' + ERROR_MESSAGE();
END CATCH;

-- Full Backup: create a full backup of the enchantedears database.
BACKUP DATABASE enchantedears 
TO DISK = 'C:\Backup\enchantedears_Full.bak';

-- Differential Backup: create a differential backup including changes since the last full backup.
BACKUP DATABASE enchantedears 
TO DISK = 'C:\Backup\enchantedears_Diff.bak' 
WITH DIFFERENTIAL;

-- NOTE: SQL Server does not support incremental backups.
-- Only full, differential, and transaction log backups are available.
-- Differential backups include all changes since the last full backup.
-- Reference: https://learn.microsoft.com/en-us/sql/relational-databases/backup-restore/differential-backups-sql-server?view=sql-server-ver16