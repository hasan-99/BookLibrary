using BookLibrary.App_Code.Models;
using System.Data;

namespace BookLibrary.App_Code.DAL;

public class BookDAL
{
    public static DataTable GetAllBooks() { }
    public static Book GetBookByISBN(long isbn) { }
    public static void InsertBook(Book book) { }
    public static void UpdateBook(Book book) { }
    public static void DeleteBook(long isbn) { }
}