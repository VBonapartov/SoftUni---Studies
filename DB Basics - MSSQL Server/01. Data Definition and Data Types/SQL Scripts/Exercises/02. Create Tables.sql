/* 02. Create Tables */
USE [Minions]
GO

CREATE TABLE Minions
(
	Id     INT PRIMARY KEY NOT NULL,
	Name   NVARCHAR(50) NOT NULL,
	Age    INT
)
GO

CREATE TABLE Towns
(
	Id     INT PRIMARY KEY NOT NULL,
	Name   NVARCHAR(50) NOT NULL,
)
GO
