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
