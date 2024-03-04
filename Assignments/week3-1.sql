-- Add test data for the AppUser table
INSERT INTO dbo.Subscription (Name, Price)
VALUES
('Single', 3.0),
('Family', 2.0);

INSERT INTO dbo.Artist (Name, Description)
VALUES
('Taylor Swift', 'An artist of Pop.'),
('MIKA', 'An artist of Dance Pop.');

INSERT INTO dbo.AppUser (Username, Email, Password, SubscriptionID)
VALUES 
('DrSheldonCooper', 'sheldon.cooper@gmail.com', '32trainNerD49', 16),
('LeonardHofstadter', 'leonard.hofstadter@gmail.com', 'Kal-el', 16),
('WizardWolowitz', 'wizard.wolowitz@gmail.com', 'Wolowitz-0231', 16),
('RajKoothrappali', 'raj.koothrappali@gmail.com', 'StarS304Beyonce', 15);

-- Verify the inserted data
SELECT * FROM dbo.AppUser
SELECT * FROM dbo.Artist
SELECT * FROM dbo.Subscription 