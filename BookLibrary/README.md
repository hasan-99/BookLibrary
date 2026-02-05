# BookLibrary – ASP.NET Web Forms Application

## Overview
BookLibrary is a sample ASP.NET Web Forms application developed using C#.  
The application demonstrates CRUD operations (Create, Read, Update, Delete) on a Books database using **MSSQL Stored Procedures only**.

This project was developed as part of an academic assignment.

---

## Technologies Used
- ASP.NET Web Forms (C#)
- SQL Server / LocalDB
- ADO.NET
- HTML, CSS
- JavaScript (Client-side validation)

---

## Database Setup

### Step 1: Create Database and Objects
1. Open **SQL Server Management Studio (SSMS)**
2. Connect to:

3. Open a **New Query**
4. Run the provided SQL script:


This script will:
- Create the database **BOOKSDB** (if it does not exist)
- Create the table **TBooks**
- Insert sample data
- Create all required Stored Procedures

---

## Connection String Configuration

Open `web.config` and ensure the following connection string exists:

```xml
<connectionStrings>
<add name="BooksDB"
    connectionString="Data Source=(localdb)\MSSQLLocalDB;
                      Initial Catalog=BOOKSDB;
                      Integrated Security=True;
                      Encrypt=False;"
    providerName="System.Data.SqlClient" />
</connectionStrings>

(( If your SQL Server instance is different (e.g. SQLEXPRESS), update the Data Source value accordingly. ))

