# Enchanted Ears
## NHL Stenden | Final Assignment | Database 2

### Table of Content
1. [1 Background](#1-background)
2. [2 Assignments](#2-assignments)
   - [2.1 Use Case](#21-use-case)
      - [2.1.1 Entities](#211-entities)
      - [2.1.2 Users](#212-users)
      - [2.1.3 Listen](#213-listen)
      - [2.1.4 Subscription](#214-subscription)
      - [2.1.5 Operations to Test](#215-operations-to-test)
   - [2.2 DBMS](#22-dbms)
      - [2.2.1 Create a Database](#221-create-a-database)
      - [2.2.2 Users and Roles](#222-users-and-roles)
   - [2.3 SQL](#23-sql)
      - [2.3.1 Add Test Data](#231-add-test-data)
      - [2.3.2 Information Needs](#232-information-needs)
   - [2.4 Database Integrity](#24-database-integrity)
      - [2.4.1 Primary Keys, Foreign Keys, and Index](#241-primary-keys-foreign-keys-and-index)
      - [2.4.2 Constraints](#242-constraints)
      - [2.4.3 Views, Stored Procedures, and Triggers](#243-views-stored-procedures-and-triggers)
      - [2.4.4 Referential Integrity](#244-referential-integrity)
   - [2.5 Database Recovery](#25-database-recovery)
      - [2.5.1 Backup](#251-backup)
      - [2.5.2 Recovery](#252-recovery)
      - [2.5.3 Concurrency](#253-concurrency)
   - [2.6 Final Assignment](#26-final-assignment)
      - [2.6.1 ADO.NET](#261-adonet)
      - [2.6.2 Entity Framework](#262-entity-framework)
      - [2.6.3 MongoDB](#263-mongodb)
         - [2.6.3.1 Example](#2631-example)
      - [2.6.4 Report](#264-report)
3. [3 TimeTable](#3-timetable)
4. [4 Additional Information](#4-additional-information)

### 1 Background
EnchantedEars was founded in 2024 by Virag Szabo for educational purposes and her improvement in the Netherlands (Haarlem). She wanted to create something unique besides Netflix or Spotify and something that is not too complicated to build, but hard enough to create. The student is going to work hard in the upcoming weeks on this final assignment, following the steps from the subject. This student needs to make a positive contribution to the implementation of this music streaming system.

### 2 Assignments
#### 2.1 Use Case 
Develop the following use case into an Entity Relationship Diagram showing all entities, relationships, attributes and keys. Bear in mind that cardinality and optionality must be included. Make sure that the diagram covers the entire use case and consider making several components generic so that the system can easily be expanded.
[The UML Diagram](Diagrams/umlv2.png)

##### 2.1.1 Entities
**Song:** It has a songID, a title, an artist, a genre, a release date, duration, an album and a playlist.
**Playlist:** It has a playlistID, a name, a description, and a collection of songs.
**Album:** It has an albumID, a title, and a release date.
**Artist:** It has an artistID, a name, a description, and a list of albums with their songs.
**User:** It has a user ID, a username, an email address, a password, and a list of playlists - they can create a playlist of the songs from the albums, and a subscription.
**Subscription:** It has a subscription, a name and a price. This is an abstract class because there is a single package for one person and a family package involving four members.

##### 2.1.2 Users
Users can register themselves using an email address and a password. They need these to log in. Before users can log in for the first time, their account must be activated via email address. After three wrong login attempts the account is temporarily blocked. If a user has forgotten the password, it can be reset. The user will receive an email containing a link where they can change their password. After logging in successfully, their profile is automatically created. The user must provide a username.

##### 2.1.3 Listen
The users can listen to songs and create playlists.

##### 2.1.4 Subscription
The users can subscribe for a single or a family package. The payment depends on the package (single: 3 euros/month, family: 2 euros/month). They cannot have multiple subscriptions.

##### 2.1.5 Operations to Test
1. **Create:** Add new songs to the database.
2. **Read:** Retrieve information about songs, possibly with different filtering options (by artist, genre, album).
3. **Update:** Modify details of existing songs (change the genre or update the release date).
4. **Delete:** Remove songs from the database.

#### 2.2 DBMS
Install Microsoft SQL Server as a database provider and Microsoft Management Studio to access and manage the database. Then create your database and add the correct users, roles, and rights utilizing DCL queries.

##### 2.2.1 Create a database
Create the database from Assignment 1 using DDL queries. You do not have to create the queries manually, but they must be delivered as an SQL file. Deliver the DDL queries in a .sql file with the file name: week2-1.sql. It is not (yet) necessary to add test data.

##### 2.2.2 Users and Roles
The data analysts at EnchantedEars are interested in the information that the data in the database provides, but they need to have access. Add the following users along with their corresponding roles and rights. Deliver the DDL queries in a .sql file with the file name: week2-2.sql.

| User | Rights |
| --- | --- |
| sa | Server admin account. |
| Matt Smith | Chief Data Analyst; DML queries. |
| Mick Worry | Data Analyst; SELECT DML queries. |
| Adam Verogue | Data Analyst; SELECT DML queries |
| Tim Snapps | Junior Data Analyst; SELECT DML queries except for personal and financial data. |
| EnchantedEarsApplication | Account that the software uses to access the database. Cannot perform CRUD operations directly on the database tables. |

#### 2.3 SQL
Create the following DML queries to extract important information from the database. Test data must be added to obtain information.
##### 2.3.1 Add test data
For every table, create several DML queries that place several test values in the database. Make sure you have enough data to work with. To make things easy for yourself, you can use tools like "Mockaroo", that generate actual test data for you. Several situations must be retrievable; see component “information needs”. Deliver the DDL queries in a .sql file with the file name: week3-1.sql.
##### 2.3.2 Information needs
The data analysts at EnchantedEars are interested in the information that the data in the database provides. Pay attention to efficiency: JOINS and SUBQUERY'S, EXISTS(), and IN(), etc. Develop the following information needs as DML queries. Deliver the DDL queries in a .sql file with the file name: week3-2.sql.

#### 2.4 Database Integrity
The EnchantedEars user or role cannot perform queries but must use views, triggers, and stored procedures. Implement the following measures to improve the integrity of the database
##### 2.4.1 Primary Keys, Foreign Keys and Index
Provide the databases with primary and foreign keys if not yet provided. Pay attention to relationships and referential integrity. Also, provide the columns with an index wherever it is logical to do so.
##### 2.4.2 Constraints
Provide the database with a few constraints. Implement NOT NULL, UNIQUE, and CHECK for columns where it is logical to do so at least once. Tip: CASCADE and NO ACTION are not the only actions that can follow after a constraint.
##### 2.4.3 Views, Stored Procedures and Triggers 
Realise two views, two stored procedures, and two triggers. Use views, stored procedures, and triggers for the right purposes.
##### 2.4.4 Referential integrity
Implement referential integrity for all foreign key constraints for a logical use case at least once.

**Note: You are not allowed to develop the queries that you designed in week 3.**
**Deliver the above as a single week4.sql file. So all answers must have been applied in the database and the used queries must be delivered for the assessment**

#### 2.5 Database Recovery
EnchantedEars aims to provide the current database with backup, recovery, and concurrency solutions. Execute the following assignments so that the Netflix database can be used in production.
##### 2.5.1 Backup
Create a full, differential, and incremental backup and transaction log for the Netflix database using CREATE BACKUP statements. Deliver the SQL commands in a .sql file with the file name: week5-1.sql.
##### 2.5.2 Recovery
Use the RESTORE statement to restore the database with a full, differential and incremental backup and transaction log. Implement a transaction for a logical use case. Deliver the SQL commands in a .sql file with the file name: week5-2.sql.
##### 2.5.3 Concurrency
Reduce the chance of concurrency problems to a minimum using a query, for instance with phantom reads and deadlocks. In addition, create two transactions for logical use cases. Deliver the SQL commands in a .sql file with the file name: week5-3.sql.

#### 2.6 Final Assignment
EnchantedEars has asked NHL Stenden to investigate whether present-day technology can make a positive contribution to the current implementation. The EnchantedEars platform uses Microsoft technology and EnchantedEars has therefore explicitly requested that ADO.NET and Entity Framework be used as concrete final situations in the investigation. EnchantedEars has no experience with NoSQL databases and has indicated that non-relational databases must also be included in the investigation. 
Develop a program in C# that tests the speeds of ADO.NET, Entity Framework, and NoSQL using a Stopwatch. Make sure that all tests are combined in a single solution. Select one entity on which to perform CRUD operations. Do this for rows of 1, 1000, 100,000, and 1,000,000. Make sure that all tests within the various DBMSs are the same.
##### 2.6.1 ADO.NET
Reuse the database that was created in previous weeks.
##### 2.6.2 Entity Framework
Create the entire database with a Code-First approach, so you may not (and cannot) reuse the previously created database. 
##### 2.6.3 MongoDB
Use MongoDB for the NoSQL test. Do not need to redesign and implement the entire database, but instead, select a use case comprising at least 3 tables.
###### 2.6.3.1 Example
Select the “subscription” entity for the speed test, in which case you also include “user”.
###### 2.6.4 Report 
Next, generate a report in PDF format. This is not an official document and DOES NOT need to contain a cover page, table of contents, etc. Presents the speeds in a matrix (table) and graphs. For each comparison an accompanying explanatory text to explain results that are worthy of note. Description of measures taken to demonstrate that the information is reliable, along with, at any rate, a description of the specifications of the device on which the tests were run.

### 3 TimeTable
*Time can be changed here.*
| Date | Document |
| --- | --- |
| February 4 | [Use Case](Documents/UseCase.pdf) |
| February 5 | [DBMS](Documents/DBMS.pdf) |
| February 6 | [SQL](Documents/SQL.pdf) |
| February 7 | [Database Integrity](Documents/DBIntegrity.pdf) |
| February 8 | [Database Recovery](Documents/DBRecovery.pdf) |
| February 9 | [Final Assignment](Documents/FinalAssignment.pdf) |

### 4 Additional Information
Virag Szabo | BS | Information Technology | February 2024
