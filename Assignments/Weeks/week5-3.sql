-- Use enchantedears database
USE enchantedears;
GO

-- Create a table for demonstration
CREATE TABLE ConcurrencyDemo (
    ID INT PRIMARY KEY,
    Name VARCHAR(50)
);

-- Insert some sample data
INSERT INTO ConcurrencyDemo (ID, Name)
VALUES (1, 'Item 1'), (2, 'Item 2');

-- Transaction 1
BEGIN TRANSACTION;

-- Perform a phantom read
SELECT * FROM ConcurrencyDemo;

-- Wait for a few seconds to allow Transaction 2 to start
WAITFOR DELAY '00:00:05';

-- Update a record
UPDATE ConcurrencyDemo SET Name = 'Updated Item 1' WHERE ID = 1;

-- Commit Transaction 1
COMMIT TRANSACTION;

-- Transaction 2
BEGIN TRANSACTION;

-- Update a record (conflicting with Transaction 1)
UPDATE ConcurrencyDemo SET Name = 'Updated Item 2' WHERE ID = 2;

-- Wait for a few seconds to allow Transaction 1 to update its record
WAITFOR DELAY '00:00:05';

-- Perform a phantom read
SELECT * FROM ConcurrencyDemo;

-- Commit Transaction 2
COMMIT TRANSACTION;
