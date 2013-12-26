USE master
IF EXISTS(select * from sys.databases where name='GameDB')
DROP DATABASE GameDB

CREATE DATABASE GameDB
GO
USE GameDB
GO

CREATE TABLE Users
(
	Id int primary key identity,
	Email varchar(50) not null,
	Password varchar(50) not null,
	Created smalldatetime not null
)

CREATE TABLE GameTypes
(
	Id int primary key identity,
	Name varchar(15) not null,
	Active bit not null DEFAULT 1
)

CREATE TABLE Games
(
	Id int primary key identity,
	Created smalldatetime not null,
	Ended smalldatetime,
	Type int not null foreign key references GameTypes(Id),
	TurnPlayerId int not null foreign key references Users(Id),
	WinnerId int foreign key references Users(Id)
)

CREATE TABLE GamesUsers
(
	UserId int not null foreign key references Users(Id),
	GameId int not null foreign key references Games(Id),
	primary key (UserId, GameId)
)

CREATE TABLE Moves
(
	Id int primary key identity,
	TimeMade smalldatetime not null,
	Value varchar(10) not null,
	UserId int not null foreign key references Users(Id),
	GameId int not null foreign key references Games(Id)
)

CREATE TABLE Tokens
(
	[Key] varchar(255) primary key not null,
	Issued smalldatetime not null,
	UserId int not null foreign key references Users(Id)
)

INSERT INTO Users VALUES('nixon.daniel.j@gmail.com', 'password', CURRENT_TIMESTAMP)
INSERT INTO Tokens VALUES('a', CURRENT_TIMESTAMP, (SELECT Id 
												   FROM Users 
												   WHERE Email='nixon.daniel.j@gmail.com'))