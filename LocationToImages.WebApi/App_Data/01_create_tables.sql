﻿CREATE TABLE [User]
(
	Id INT IDENTITY(1, 1) NOT NULL,
	UserName VARCHAR(100) NOT NULL,
	FirstName VARCHAR(255) NOT NULL,
	LastName VARCHAR(255) NOT NULL,
	Password VARCHAR(255) NOT NULL,

    CONSTRAINT PK_User_Id PRIMARY KEY(Id)
)

CREATE TABLE dbo.[Location]
(
	Id INT IDENTITY(1, 1) NOT NULL,
	[Address] VARCHAR(1000) NOT NULL,

    CONSTRAINT PK_Location_Id PRIMARY KEY(Id),
    CONSTRAINT UK_Location_Address UNIQUE ([Address])
)

CREATE TABLE dbo.GeoLocation
(
	Id INT IDENTITY(1, 1) NOT NULL,
	GeoCodes VARCHAR(50) NOT NULL,
	[Address] VARCHAR(1000) NOT NULL,

    CONSTRAINT PK_GeoLocation_Id PRIMARY KEY(Id),
    CONSTRAINT UK_GeoLocation_GeoCodes UNIQUE (GeoCodes)
)

CREATE TABLE UserGeoLocation
(
	Id INT IDENTITY(1, 1),
	UserId INT NOT NULL,
	GeoLocationId INT NOT NULL,

    CONSTRAINT PK_UserGeoLocation_Id PRIMARY KEY(Id),
	CONSTRAINT FK_UserGeoLocation_User_UserId FOREIGN KEY(UserId) REFERENCES [User](Id),
	CONSTRAINT FK_UserGeoLocation_GeoLocation_GeoLocationId FOREIGN KEY(GeoLocationId) REFERENCES GeoLocation(Id),
    CONSTRAINT UK_UserGeoLocation_UserId_GeoLocationId UNIQUE (UserId, GeoLocationId)
)

CREATE TABLE [Photo]
(
	Id INT IDENTITY(1, 1) NOT NULL,
	[Url] VARCHAR(1000) NOT NULL,
	Title VARCHAR(255) NOT NULL,
	[Description] VARCHAR(1000) NULL,
	Latitude DECIMAL(10, 8) NOT NULL,
	Longitude DECIMAL(11, 8) NOT NULL,
	[Address] VARCHAR(1000) NULL,

    CONSTRAINT PK_Photo_Id PRIMARY KEY(Id),
    CONSTRAINT UK_Photo_Url UNIQUE ([URL])
)