-- DBMS

-- Check Permissions: The users have the correct table permissions.
GRANT SELECT, INSERT, UPDATE ON UserPreference TO [Matt Smith];
GRANT SELECT, INSERT, UPDATE ON UserPreference TO [Mick Worry];
GRANT SELECT, INSERT, UPDATE ON UserPreference TO [Adam Verogue];
GRANT SELECT, INSERT, UPDATE ON UserPreference TO [Tim Snapps];

GRANT SELECT, INSERT, UPDATE ON Subscription TO [Matt Smith];
GRANT SELECT, INSERT, UPDATE ON Subscription TO [Mick Worry];
GRANT SELECT, INSERT, UPDATE ON Subscription TO [Adam Verogue];
GRANT SELECT, INSERT, UPDATE ON Subscription TO [Tim Snapps];

-- Revoke Delete Permissions
REVOKE DELETE ON UserPreference FROM [Matt Smith];
REVOKE DELETE ON UserPreference FROM [Mick Worry];
REVOKE DELETE ON UserPreference FROM [Adam Verogue];
REVOKE DELETE ON UserPreference FROM [Tim Snapps];

REVOKE DELETE ON Subscription FROM [Matt Smith];
REVOKE DELETE ON Subscription FROM [Mick Worry];
REVOKE DELETE ON Subscription FROM [Adam Verogue];
REVOKE DELETE ON Subscription FROM [Tim Snapps];

-- Financial Data Restrictions: Financial/personal data rights have been implemented.
REVOKE ALL PRIVILEGES ON ConcurrencyDemo FROM JuniorUserRole;
REVOKE ALL PRIVILEGES ON UserPreference FROM JuniorUserRole;
REVOKE ALL PRIVILEGES ON Subscription FROM JuniorUserRole;