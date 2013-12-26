USE master
IF EXISTS(select * from sys.databases where name='GameDB')
DROP DATABASE GameDB

CREATE DATABASE GameDB
GO
USE GameDB
GO
