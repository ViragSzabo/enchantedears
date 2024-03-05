USE enchantedears;
GO

-- Full Backup
BACKUP DATABASE enchantedears
TO DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup\enchantedears.bak'
WITH INIT;

-- Differential Backup (Assuming a recent full backup)
BACKUP DATABASE enchantedears
TO DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup\enchantedears_Differential.bak'
WITH DIFFERENTIAL;

-- Incremental Backup (Not needed in SIMPLE recovery model)
-- SKIP this if you are not in the FULL recovery model
BACKUP LOG enchantedears
TO DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup\enchantedears_Incremental.trn';