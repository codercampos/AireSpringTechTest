USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name = 'AireSpringDB')
BEGIN
    ALTER DATABASE AireSpringDB set single_user with rollback immediate
    DROP DATABASE AireSpringDB
END
GO

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'AireSpringDB')
BEGIN
    CREATE DATABASE AireSpringDB
END
GO

USE AireSpringDB
GO

use AireSpringDB
go

CREATE TABLE dbo.EmployeeRecord
(
    EmployeeID        int identity
        constraint EmployeeRecord_pk
            primary key,
    EmployeeLastName  nvarchar(50),
    EmployeeFirstName nvarchar(50),
    EmployeePhone     nvarchar(15),
    EmployeeZip       nvarchar(5),
    HireDate          date
)
GO

INSERT INTO EmployeeRecord
VALUES
    ('Alvarado', 'Flor', '8501234567', '12345', '2023-10-01'),
    ('Smith', 'John', '5551234567', '12345', '2023-10-02'),
    ('Johnson', 'Jane', '5559876543', '54321', '2023-09-15'),
    ('Doe', 'Richard', '5557890123', '67890', '2023-08-20'),
    ('Brown', 'Emily', '5554567890', '23456', '2023-07-10'),
    ('Anderson', 'Michael', '5552345678', '34567', '2023-06-25'),
    ('Wilson', 'Susan', '5556789012', '45678', '2023-05-30'),
    ('Lee', 'David', '5558901234', '56789', '2023-04-15'),
    ('Martinez', 'Linda', '5553456789', '67891', '2023-03-20'),
    ('Harris', 'William', '5555678901', '78901', '2023-02-05'),
    ('Clark', 'Jessica', '5556789012', '89012', '2023-01-10'),
    ('Miller', 'Daniel', '5551234567', '90123', '2022-12-15'),
    ('Thompson', 'Karen', '5557890123', '12345', '2022-11-20'),
    ('Garcia', 'James', '5559876543', '23456', '2022-10-25'),
    ('White', 'Sarah', '5552345678', '34567', '2022-09-30'),
    ('Wilson', 'Robert', '5555678901', '45678', '2022-09-05');
GO

