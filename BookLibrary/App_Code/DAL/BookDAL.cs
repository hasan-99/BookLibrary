using BookLibrary.Helpers;
using BookLibrary.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BookLibrary.DAL {



    public static class BookDAL {
        public static DataTable GetAllBooks() {
            using (var con = DBHelper.GetConnection())
            using (var cmd = new SqlCommand("dbo.spBooks_GetAll", con))
            using (var da = new SqlDataAdapter(cmd)) {
                cmd.CommandType = CommandType.StoredProcedure;
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static Book GetBookByISBN(int isbn) {
            using (var con = DBHelper.GetConnection())
            using (var cmd = new SqlCommand("dbo.spBooks_GetByISBN", con)) {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ISBN", isbn);

                con.Open();
                using (var r = cmd.ExecuteReader()) {
                    if (!r.Read())
                        return null;

                    return new Book {
                        ISBN = Convert.ToInt32(r["ISBN"]),
                        Title = Convert.ToString(r["Title"]),
                        Author = Convert.ToString(r["Author"]),
                        PublishDate = Convert.ToDateTime(r["PublishDate"]),
                        Price = Convert.ToDecimal(r["Price"]),
                        Publish = Convert.ToBoolean(r["Publish"])
                    };
                }
            }
        }

        public static void InsertBook(Book book) {
            using (var con = DBHelper.GetConnection())
            using (var cmd = new SqlCommand("dbo.spBooks_Insert", con)) {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@PublishDate", book.PublishDate);
                cmd.Parameters.AddWithValue("@Price", book.Price);
                cmd.Parameters.AddWithValue("@Publish", book.Publish);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateBook(Book book) {
            using (var con = DBHelper.GetConnection())
            using (var cmd = new SqlCommand("dbo.spBooks_Update", con)) {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@PublishDate", book.PublishDate);
                cmd.Parameters.AddWithValue("@Price", book.Price);
                cmd.Parameters.AddWithValue("@Publish", book.Publish);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteBook(int isbn) {
            using (var con = DBHelper.GetConnection())
            using (var cmd = new SqlCommand("dbo.spBooks_Delete", con)) {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ISBN", isbn);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}