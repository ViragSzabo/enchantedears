USE enchantedears;
GO

-- Full Backup
BACKUP DATABASE enchantedears
TO DISK = 'C:\Users\szabo\OneDrive\Dokumentumok\University\Database2\EnchantedEars Final Assignment\enchantedears\Backup\enchantedears_Full.bak'
WITH INIT;

-- Differential Backup
BACKUP DATABASE enchantedears
TO DISK = 'C:\Users\szabo\OneDrive\Dokumentumok\University\Database2\EnchantedEars Final Assignment\enchantedears\Backup\enchantedears_Differential.bak'
WITH DIFFERENTIAL;

-- Incremental Backup
BACKUP LOG enchantedears
TO DISK = 'C:\Users\szabo\OneDrive\Dokumentumok\University\Database2\EnchantedEars Final Assignment\enchantedears\Backup\enchantedears_Incremental.trn';