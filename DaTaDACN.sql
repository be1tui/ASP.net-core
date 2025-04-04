﻿CREATE DATABASE WebCNTT;
USE WebCNTT;
GO

CREATE TABLE AdminUser (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(255) NULL,
    Email NVARCHAR(255) NULL,
    Password NVARCHAR(255) NULL,
    IsActive BIT NULL
);
ALTER TABLE AdminUser
ADD PasswordResetToken NVARCHAR(255) NULL;
ALTER TABLE AdminUser
ADD Role INT NULL;

select * from AdminUser