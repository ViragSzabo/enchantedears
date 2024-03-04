-- Create roles and users
-- The server principal 'sa' already exists. So, I reset the password
ALTER LOGIN sa WITH PASSWORD = '1234yourPass', CHECK_POLICY = OFF;

CREATE LOGIN [Matt Smith] WITH PASSWORD = 'passMS1234', CHECK_POLICY = OFF;
CREATE USER [Matt Smith] FOR LOGIN [Matt Smith];
EXEC sp_addrolemember 'db_datareader', 'Matt Smith';

CREATE LOGIN [Mick Worry] WITH PASSWORD = 'passMW1234', CHECK_POLICY = OFF;
CREATE USER [Mick Worry] FOR LOGIN [Mick Worry];
EXEC sp_addrolemember 'db_datareader', 'Mick Worry';

CREATE LOGIN [Adam Verogue] WITH PASSWORD = 'passAV1234', CHECK_POLICY = OFF;
CREATE USER [Adam Verogue] FOR LOGIN [Adam Verogue];
EXEC sp_addrolemember 'db_datareader', 'Adam Verogue';

CREATE LOGIN [Tim Snapps] WITH PASSWORD = 'passTS1234', CHECK_POLICY = OFF;
CREATE USER [Tim Snapps] FOR LOGIN [Tim Snapps];
EXEC sp_addrolemember 'db_datareader', 'Tim Snapps';

CREATE LOGIN [EnchantedEarsApplication] WITH PASSWORD = 'ApplicationPassword', CHECK_POLICY = OFF;
CREATE USER [EnchantedEarsApplication] FOR LOGIN [EnchantedEarsApplication];