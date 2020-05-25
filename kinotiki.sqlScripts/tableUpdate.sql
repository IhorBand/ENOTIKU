-- ihorB
-- Added kinotikirMainDB -- Main Database For Project
IF DB_ID('kinotikiMainDb') IS NOT NULL
BEGIN
	print('DataBase is allready exist');
END
ELSE
BEGIN
	USE master
	CREATE DATABASE kinotikiMainDb
END
GO

-- ihorB
USE [kinotikiMainDb]
CREATE TABLE Users 
(
	[id] INT PRIMARY KEY IDENTITY(1,1),
	[login] VARCHAR(MAX),
	[password] VARCHAR(MAX),
	[email] VARCHAR(MAX),
	[sex] INT,
	[age] INT,
	[role] INT,

	[imageData] VARBINARY(MAX),
	[imageMimeType] VARCHAR(MAX)
);
GO

-- ihorB
USE [kinotikiMainDb]
CREATE TABLE Global_Settings
(
	smtpIP VARCHAR(MAX),
	smtpPort INT,
	smtpMail VARCHAR(MAX),
	smtpPassword VARCHAR(MAX)
);
GO

-- ihorB
-- Added record To The GlobalSettings Table for smtp server (Sending emails from google )
USE [kinotikiMainDb]
--INSERT INTO Global_Settings(smtpIP,smtpPort,smtpMail,smtpPassword) VALUES ('smtp.gmail.com',587,'igor.bandura.3@gmail.com','************') --! Attention Add Real Password before using
GO


-- ihorB
USE [kinotikiMainDb]
ALTER TABLE [Users] DROP COLUMN [age]
GO

USE [kinotikiMainDb]
ALTER TABLE [Users] ADD [Birthday] DATETIME2 NOT NULL DEFAULT(GETDATE())
GO

--ihorB
use [kinotikiMainDb]
ALTER TABLE [Global_Settings] ADD [movieDBKey] VARCHAR(100) NULL
GO

UPDATE Global_Settings SET movieDBKey = 'e02e113c1e8d086578caa870a138b65a';
GO

use [kinotikiMainDb]
ALTER TABLE [Global_Settings] ADD [id] INT NOT NULL IDENTITY(1,1)
GO
