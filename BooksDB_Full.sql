/* =====================================================
   1) Create Database if not exists
===================================================== */
IF DB_ID('BOOKSDB') IS NULL
BEGIN
    CREATE DATABASE BOOKSDB;
END
GO

USE BOOKSDB;
GO

/* =====================================================
   2) Create Table if not exists
===================================================== */
IF OBJECT_ID('dbo.TBooks', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TBooks (
        ISBN        NUMERIC(13,0)   NOT NULL,
        Title       NVARCHAR(200)   NOT NULL,
        Author      NVARCHAR(100)   NOT NULL,
        PublishDate DATETIME        NOT NULL,
        Price       DECIMAL(10,2)   NOT NULL,
        Publish     BIT             NOT NULL CONSTRAINT DF_TBooks_Publish DEFAULT (1),
        CONSTRAINT PK_TBooks PRIMARY KEY (ISBN)
    );
END
GO

/* =====================================================
   3) Insert Sample Data (only if table is empty)
===================================================== */
IF NOT EXISTS (SELECT 1 FROM dbo.TBooks)
BEGIN
    INSERT INTO dbo.TBooks (ISBN, Title, Author, PublishDate, Price, Publish)
    VALUES
    (9780132350884, N'Clean Code', N'Robert C. Martin', '2008-08-01', 35.99, 1),
    (9780201616224, N'The Pragmatic Programmer', N'Andrew Hunt', '1999-10-20', 39.99, 1),
    (9780131103627, N'The C Programming Language', N'Kernighan & Ritchie', '1988-04-01', 29.99, 1),
    (9780134494166, N'Effective Java', N'Joshua Bloch', '2018-01-06', 45.00, 1),
    (9781492056812, N'Learning SQL', N'Alan Beaulieu', '2020-04-14', 28.50, 0);
END
GO

/* =====================================================
   4) Drop Stored Procedures if exist
===================================================== */
IF OBJECT_ID('dbo.spBooks_GetAll', 'P') IS NOT NULL DROP PROCEDURE dbo.spBooks_GetAll;
IF OBJECT_ID('dbo.spBooks_GetByISBN', 'P') IS NOT NULL DROP PROCEDURE dbo.spBooks_GetByISBN;
IF OBJECT_ID('dbo.spBooks_Insert', 'P') IS NOT NULL DROP PROCEDURE dbo.spBooks_Insert;
IF OBJECT_ID('dbo.spBooks_Update', 'P') IS NOT NULL DROP PROCEDURE dbo.spBooks_Update;
IF OBJECT_ID('dbo.spBooks_Delete', 'P') IS NOT NULL DROP PROCEDURE dbo.spBooks_Delete;
GO

/* =====================================================
   5) Create Stored Procedures
===================================================== */

-- Get All Books
CREATE PROCEDURE dbo.spBooks_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ISBN, Title, Author, PublishDate, Price, Publish
    FROM dbo.TBooks;
END
GO

-- Get Book By ISBN
CREATE PROCEDURE dbo.spBooks_GetByISBN
    @ISBN NUMERIC(13,0)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ISBN, Title, Author, PublishDate, Price, Publish
    FROM dbo.TBooks
    WHERE ISBN = @ISBN;
END
GO

-- Insert Book
CREATE PROCEDURE dbo.spBooks_Insert
    @ISBN NUMERIC(13,0),
    @Title NVARCHAR(200),
    @Author NVARCHAR(100),
    @PublishDate DATETIME,
    @Price DECIMAL(10,2),
    @Publish BIT
AS
BEGIN
    INSERT INTO dbo.TBooks
    VALUES (@ISBN, @Title, @Author, @PublishDate, @Price, @Publish);
END
GO

-- Update Book
CREATE PROCEDURE dbo.spBooks_Update
    @ISBN NUMERIC(13,0),
    @Title NVARCHAR(200),
    @Author NVARCHAR(100),
    @PublishDate DATETIME,
    @Price DECIMAL(10,2),
    @Publish BIT
AS
BEGIN
    UPDATE dbo.TBooks
    SET Title = @Title,
        Author = @Author,
        PublishDate = @PublishDate,
        Price = @Price,
        Publish = @Publish
    WHERE ISBN = @ISBN;
END
GO

-- Delete Book
CREATE PROCEDURE dbo.spBooks_Delete
    @ISBN NUMERIC(13,0)
AS
BEGIN
    DELETE FROM dbo.TBooks
    WHERE ISBN = @ISBN;
END
GO
