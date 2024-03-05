USE enchantedears;
GO

-- Implement referential integrity for foreign key constraints
-- (Assuming SubscriptionID is the foreign key in AppUser referencing Subscription)
ALTER TABLE AppUser
ADD CONSTRAINT FK_Subscription
FOREIGN KEY (SubscriptionID)
REFERENCES Subscription(SubscriptionID)
ON DELETE CASCADE
ON UPDATE CASCADE;