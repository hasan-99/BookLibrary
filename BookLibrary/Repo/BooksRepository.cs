using BookLibrary.Helpers;
using global::BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BookLibrary.Repo {

    public static class BooksRepository {
        public static List<Book> GetAll() {
            var list = new List<Book>();

            using (var con = DBHelper.GetConnection())
            using (var cmd = new SqlCommand("dbo.spBooks_GetAll", con)) {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (var r = cmd.ExecuteReader()) {
                    while (r.Read()) {
                        list.Add(Map(r));
                    }
                }
            }
            return list;
        }

        public static Book GetByIsbn(decimal isbn) {
            using (var con = DBHelper.GetConnection())
            using (var cmd = new SqlCommand("dbo.spBooks_GetByISBN", con)) {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ISBN", SqlDbType.Decimal).Value = isbn;
                con.Open();

                using (var r = cmd.ExecuteReader()) {
                    return r.Read() ? Map(r) : null;
                }
            }
        }

        public static void Insert(decimal isbn, string title, string author, DateTime publishDate, decimal price, bool publish) {
            using (var con = DBHelper.GetConnection())
            using (var cmd = new SqlCommand("dbo.spBooks_Insert", con)) {
                cmd.CommandType = CommandType.StoredProcedure;
                FillParams(cmd, isbn, title, author, publishDate, price, publish);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(decimal isbn, string title, string author, DateTime publishDate, decimal price, bool publish) {
            using (var con = DBHelper.GetConnection())
            using (var cmd = new SqlCommand("dbo.spBooks_Update", con)) {
                cmd.CommandType = CommandType.StoredProcedure;
                FillParams(cmd, isbn, title, author, publishDate, price, publish);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(decimal isbn) {
            using (var con = DBHelper.GetConnection())
            using (var cmd = new SqlCommand("dbo.spBooks_Delete", con)) {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ISBN", SqlDbType.Decimal).Value = isbn;

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void FillParams(SqlCommand cmd, decimal isbn, string title, string author, DateTime publishDate, decimal price, bool publish) {
            cmd.Parameters.Add("@ISBN", SqlDbType.Decimal).Value = isbn;
            cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 200).Value = title;
            cmd.Parameters.Add("@Author", SqlDbType.NVarChar, 100).Value = author;
            cmd.Parameters.Add("@PublishDate", SqlDbType.DateTime).Value = publishDate;
            cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = price;
            cmd.Parameters.Add("@Publish", SqlDbType.Bit).Value = publish;
        }

        private static Book Map(SqlDataReader r) {
            return new Book {
                ISBN = Convert.ToDecimal(r["ISBN"]),
                Title = Convert.ToString(r["Title"]),
                Author = Convert.ToString(r["Author"]),
                PublishDate = Convert.ToDateTime(r["PublishDate"]),
                Price = Convert.ToDecimal(r["Price"]),
                Publish = Convert.ToBoolean(r["Publish"])
            };
        }
    }
}
