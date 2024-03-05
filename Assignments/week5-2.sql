USE master;
GO

-- Restore Full Backup
RESTORE DATABASE enchantedears
FROM DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup\enchantedears.bak'
WITH REPLACE, NORECOVERY;

-- Restore Differential Backup
RESTORE DATABASE enchantedears
FROM DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup\enchantedears_Differential.bak'
WITH NORECOVERY;

-- Restore Incremental Backup (Assuming FULL recovery model)
-- SKIP this if you are not in the FULL recovery model
RESTORE LOG enchantedears
FROM DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup\enchantedears_Incremental.trn'
WITH RECOVERY;

-- Logical Use Case: Rollback to a specific point in time
USE enchantedears;
GO

-- Assuming 'YourDesiredDateTime' is the point in time you want to roll back to
BEGIN TRY
    BEGIN TRAN;

    -- This is the correct placement of STOPAT
    RESTORE LOG enchantedears
    FROM DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup\enchantedears.bak'
    WITH STOPAT = '2024-03-09 12:00:00'; -- Adjust the date and time accordingly

    -- If everything is correct, commit the transaction
    COMMIT TRAN;
END TRY
BEGIN CATCH
    -- If there's an error, rollback the transaction
    ROLLBACK TRAN;
    THROW;
END CATCH;
