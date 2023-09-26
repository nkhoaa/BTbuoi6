USE MASTER
GO
IF EXISTS ( SELECT * FROM SYS.DATABASES WHERE NAME = 'QuanLySinhVien')
	DROP DATABASE QuanLySinhVien
GO

CREATE DATABASE QuanLySinhVien
GO

USE QuanLySinhVien
GO
IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE NAME = 'Faculty')
	DROP TABLE Faculty
GO
CREATE TABLE Faculty
(
	FacultyID int not null primary key,
	FacultyName nvarchar(200)
)	

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE NAME = 'Student')
	DROP TABLE Student
GO
CREATE TABLE Student
(
	StudentID nvarchar(20) not null primary key,
	FullName nvarchar(200),
	AverageScore float,
	FacultyID int not null
)	

ALTER TABLE Student
	ADD CONSTRAINT FK_Student_Faculty FOREIGN KEY(FacultyID) REFERENCES Faculty(FacultyID)

INSERT INTO Faculty(FacultyID,FacultyName) VALUES('1','CNTT')
INSERT INTO Faculty(FacultyID,FacultyName) VALUES('2','NNA')
INSERT INTO Faculty(FacultyID,FacultyName) VALUES('3','QTKD')

INSERT INTO Student(StudentID,FullName,AverageScore,FacultyID) VALUES('1611061916','Nguyen Tran Hoang Lan','4.5','1')
INSERT INTO Student(StudentID,FullName,AverageScore,FacultyID) VALUES('1711060596','Dam Minh Duc','2.5','1')
INSERT INTO Student(StudentID,FullName,AverageScore,FacultyID) VALUES('1711061004','Nguyen Quoc An','10','2')

select * from Student
select * from Faculty