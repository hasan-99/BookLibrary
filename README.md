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
```


<img width="1324" height="692" alt="image" src="https://github.com/user-attachments/assets/cb38d34b-6e6e-4049-8353-70dee8963749" />
<img width="1405" height="754" alt="image" src="https://github.com/user-attachments/assets/f4455970-86ca-411b-b97b-a1bb2b732b46" />

<img width="1765" height="908" alt="image" src="https://github.com/user-attachments/assets/26194970-cdfc-407c-952e-2ca2bbc7bd12" />
<img width="1271" height="847" alt="image" src="https://github.com/user-attachments/assets/72ea4153-4b21-4fec-906c-a33a3d98b997" />
