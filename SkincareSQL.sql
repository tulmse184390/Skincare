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
-- Thêm dữ liệu vào bảng Roles
INSERT INTO [dbo].[Roles] (RoleName) VALUES
('Admin'), ('Employee'), ('Customer');

-- Thêm dữ liệu vào bảng Users
INSERT INTO [dbo].[Users] (RoleId, Email, Password, FirstName, LastName, Gender, PhoneNumber, Status) VALUES
(1, 'admin@skincare.com', 'admin123', 'John', 'Doe', 'Male', '0123456789', 'Active'),
(2, 'employee@skincare.com', 'emp123', 'Jane', 'Smith', 'Female', '0987654321', 'Active'),
(3, 'customer1@gmail.com', 'cust123', 'Alice', 'Brown', 'Female', '0112233445', 'Active'),
(3, 'customer2@gmail.com', 'cust456', 'Bob', 'Johnson', 'Male', '0998877665', 'Inactive');

-- Thêm dữ liệu vào bảng Services
INSERT INTO [dbo].[Services] (ServiceName, Duration, Price, Status, Description) VALUES
(N'Facial Treatment', 60, 50.00, 'Active', N'Luxury facial skincare treatment.'),
(N'Body Massage', 90, 75.00, 'Active', N'Relaxing full body massage.'),
(N'Acne Treatment', 45, 40.00, 'Active', N'Treatment for acne-prone skin.'),
(N'Skin Whitening', 60, 100.00, 'Inactive', N'Brightening and whitening treatment.');

-- Thêm dữ liệu vào bảng Appointments
INSERT INTO [dbo].[Appointments] (UserId, Total, StartTime, EndTime, CreateDate, Status) VALUES
(3, 50.00, '2025-03-25 10:00:00', '2025-03-25 11:00:00', '2025-03-20', 'Confirmed'),
(3, 75.00, '2025-03-26 14:00:00', '2025-03-26 15:30:00', '2025-03-21', 'Pending'),
(4, 40.00, '2025-03-27 09:00:00', '2025-03-27 09:45:00', '2025-03-22', 'Completed');

-- Thêm dữ liệu vào bảng AppointmentDetails
INSERT INTO [dbo].[AppointmentDetails] (AppointmentId, ServiceId) VALUES
(1, 1), (2, 2), (3, 3);