/* 09. Find Names of All Employees by Salary in Range */
USE [SoftUni]
GO

SELECT FirstName, LastName, JobTitle
  FROM Employees
 WHERE Salary BETWEEN 20000 AND 30000;