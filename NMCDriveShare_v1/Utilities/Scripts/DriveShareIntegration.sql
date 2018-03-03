-- Title: v1-schema integration table
-- Author: Gabriel Hanna
-- Date Created: 2 March 2018
-- Date Modified: 2 March 2018
-- Description:
--		Adds models and database objects from the main project's
--		schema and integrates them into the authentication model

-- This script is for connecting to a file, not a database on a server
-- DO NOT EXECUTE WITH THIS USING STATEMENT
--USE [aspnet-NMCDriveShare_v1-20180226063808.mdf]
--GO

-- Create tables in order of dependency

-- 1) Recreate Geolocation table
DROP TABLE IF EXISTS Geolocation;
GO
CREATE TABLE dbo.Geolocation (
    locationId INT            NOT NULL,
    longitude  DECIMAL (8, 5) NULL,
    latitude   DECIMAL (8, 5) NULL,
    PRIMARY KEY CLUSTERED (locationId ASC)
);
GO

-- 2) Recreate User table
DROP TABLE IF EXISTS [User];
GO
CREATE TABLE dbo.[User] (
    userID      INT           IDENTITY (100000, 1) NOT NULL,
    lastName    VARCHAR (50)  NULL,
    firstName   VARCHAR (50)  NULL,
--  username    VARCHAR (50)  NULL,
--  [password]  VARCHAR (100) NULL,
--  phoneNumber VARCHAR (10)  NULL,
    isDriver    BIT           DEFAULT ((0)) NULL,
    gender      VARCHAR (6)   NULL,
--  nmcEmail    VARCHAR (100) NULL,
    locationId  INT           NULL,
    isActive    BIT           DEFAULT ((0)) NOT NULL,
	authUserId  NVARCHAR(128) NOT NULL,
    PRIMARY KEY CLUSTERED (userID ASC),
    FOREIGN KEY (locationId) REFERENCES dbo.Geolocation (locationId),
	FOREIGN KEY (authUserId) REFERENCES dbo.AspNetUsers (Id)
		ON UPDATE CASCADE
		ON DELETE CASCADE
);
GO

-- 3) Recreate Comment table
DROP TABLE IF EXISTS Comment;
GO
CREATE TABLE dbo.Comment (
    commentID   INT            IDENTITY (200000, 1) NOT NULL,
    userID      INT            NOT NULL,
    commentText VARCHAR (1000) NULL,
    PRIMARY KEY CLUSTERED (commentID ASC),
    FOREIGN KEY (userID) REFERENCES dbo.[User] (userID)
);
GO

-- 4) Recreate Rideboard table
DROP TABLE IF EXISTS Rideboard;
GO
CREATE TABLE dbo.RideBoard (
    boardId       INT NOT NULL,
    driverId      INT NOT NULL,
    destinationId INT NULL,
    PRIMARY KEY CLUSTERED (boardId ASC),
    FOREIGN KEY (destinationId) REFERENCES dbo.Geolocation (locationId),
    FOREIGN KEY (driverId) REFERENCES dbo.[User] (userID)
);
GO

-- 5) Recreate RideBoardSlot table (joining table between Users and Rideboards)
DROP TABLE IF EXISTS RideBoardSlot;
GO
CREATE TABLE dbo.RideBoardSlot (
    boardId INT NOT NULL,
    userId  INT NOT NULL,
    PRIMARY KEY CLUSTERED (boardId ASC, userId ASC),
    FOREIGN KEY (boardId) REFERENCES dbo.RideBoard (boardId),
    FOREIGN KEY (userId) REFERENCES dbo.[User] (userID)
);
GO

-- Create views

-- 1) Recreate vw_UserLocations
DROP VIEW IF EXISTS vw_UserLocations;
GO
CREATE VIEW vw_UserLocations AS
	SELECT
		au.UserName,
		u.isDriver,
		u.firstName,
		u.lastName,
		g.longitude,
		g.latitude
	FROM [User] AS u
		RIGHT JOIN Geolocation AS g
		ON u.locationId = g.locationId
		INNER JOIN AspNetUsers AS au
		ON au.Id = u.authUserId
		WHERE u.isActive = 1;
GO

-- Create stored procedures

-- 1) Recreate AddNewUser()
DROP PROCEDURE IF EXISTS AddNewUser;
GO
CREATE PROCEDURE dbo.AddNewUser
	@firstName varchar(50),
	@lastName varchar(50),
	@username varchar(50),
	@phoneNumber varchar(10),
	@isDriver bit,
	@nmcEmail varchar(100),
	@passwordHash nvarchar(MAX),
	@gender varchar(10),
	@authUserId nvarchar(128) NOT NULL
AS
BEGIN
	-- create new user
	INSERT INTO dbo.[User]
		VALUES (@lastName, @firstName, @isDriver, @gender, NULL, 0, @authUserId);

	-- update the authentication counterpart
	UPDATE dbo.AspNetUsers SET
		Email = @nmcEmail,
		PasswordHash = @passwordHash
	WHERE Id = @authUserId;
END

