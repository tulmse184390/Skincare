Use master;
Go

If Db_ID('Skincare') Is Not Null Drop Database Skincare;
Go

Create Database Skincare;
Go

Use Skincare;
Go

Create Table [dbo].[Roles] (
	RoleId Int Identity(1,1) Primary Key,
	RoleName Varchar(20) Not Null
);
Go

Create Table [dbo].[Users] (
    UserId Int Identity(1,1) Primary Key,
    RoleId Int Not Null,
	Email Varchar(255) Not Null Unique,
	Password Varchar(255) Not Null,
	FirstName Nvarchar(255) Not Null,
	LastName Nvarchar(255) Not Null,
	Gender Nvarchar(20) Not Null,
    PhoneNumber Varchar(12),
	Status Varchar(10) Not Null,
	Foreign Key (RoleId) References [dbo].[Roles](RoleId)
);
Go

Create Table [dbo].[Services] (
    ServiceId Int Identity(1,1) Primary Key,
	ServiceName Nvarchar(255) Not Null,
    Duration Int Not Null,
    Price Decimal(10, 2) Not Null,
	Status Varchar(10) Not Null,
    Description Ntext Not Null
);
Go

CREATE TABLE [dbo].[Appointments] (
    AppointmentId Int Identity(1,1) Primary Key,
    UserId Int Not Null,
    Total Decimal(10,2) Not Null,
    StartTime DateTime Not Null,
    EndTime DateTime Not Null,
	CreateDate DateTime Not Null,
	Status Varchar(10) Not Null,
    Foreign Key (UserId) References [dbo].[Users](UserId)
);
Go

Create Table [dbo].[AppointmentDetails] (
	AppointmentDetailId Int Identity(1,1) Primary Key,
	AppointmentId Int Not Null,
	ServiceId Int Not Null,
	Foreign Key (AppointmentId) References [dbo].[Appointments](AppointmentId),
	Foreign Key (ServiceId) References [dbo].[Services](ServiceId)
);
