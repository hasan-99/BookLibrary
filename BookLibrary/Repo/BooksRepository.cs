using BookLibrary.Helpers;
using global::BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BookLibrary.Repo {
    public static class BooksRepository {
        private const string SpGetAll = "dbo.spBooks_GetAll";
        private const string SpGetByIsbn = "dbo.spBooks_GetByISBN";
        private const string SpInsert = "dbo.spBooks_Insert";
        private const string SpUpdate = "dbo.spBooks_Update";
        private const string SpDelete = "dbo.spBooks_Delete";

        public static List<Book> GetAll() {
            var list = new List<Book>();

            using (var con = DBHelper.GetConnection())
            using (var cmd = new SqlCommand(SpGetAll, con)) {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (var r = cmd.ExecuteReader()) {
                    while (r.Read())
                        list.Add(Map(r));
                }
            }
            return list;
        }

        public static Book GetByIsbn(decimal isbn) {
            using (var con = DBHelper.GetConnection())
            using (var cmd = new SqlCommand(SpGetByIsbn, con)) {
                cmd.CommandType = CommandType.StoredProcedure;
                AddIsbnParam(cmd, isbn);

                con.Open();
                using (var r = cmd.ExecuteReader()) {
                    return r.Read() ? Map(r) : null;
                }
            }
        }

        public static OperationResult Insert(decimal isbn, string title, string author,
         DateTime publishDate, decimal price, bool publish) {
            try {
                if (publishDate < new DateTime(1753, 1, 1))
                    return OperationResult.Fail("Publish Date must be 1753-01-01 or later.");

                using (var con = DBHelper.GetConnection())
                using (var cmd = new SqlCommand(SpInsert, con)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    FillParams(cmd, isbn, title, author, publishDate, price, publish);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return OperationResult.Ok("Book added successfully.");
            } catch (SqlException ex) {
                // 2627 = Violation of PRIMARY KEY constraint
                // 2601 = Cannot insert duplicate key row in unique index
                if (ex.Number == 2627 || ex.Number == 2601)
                    return OperationResult.Fail("ISBN already exists. Please use a different ISBN.");

                return OperationResult.Fail("Database error. Please try again.");
            } catch {
                return OperationResult.Fail("Unexpected error. Please try again.");
            }
        }

        public static OperationResult Update(decimal isbn, string title, string author,
             DateTime publishDate, decimal price, bool publish) {
            try {
                if (publishDate < new DateTime(1753, 1, 1))
                    return OperationResult.Fail("Publish Date must be 1753-01-01 or later.");

                var existing = GetByIsbn(isbn);
                if (existing == null)
                    return OperationResult.Fail("Book not found.");

                using (var con = DBHelper.GetConnection())
                using (var cmd = new SqlCommand(SpUpdate, con)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    FillParams(cmd, isbn, title, author, publishDate, price, publish);

                    con.Open();
                    var affected = cmd.ExecuteNonQuery();

                    if (affected == 0)
                        return OperationResult.Ok("No changes detected.");

                    return OperationResult.Ok("Book updated successfully.");
                }
            } catch (SqlException ex) {
                if (ex.Number == 2627 || ex.Number == 2601)
                    return OperationResult.Fail("Duplicate value. Please use different values.");

                return OperationResult.Fail("Database error. Please try again.");
            } catch (Exception ex) {
                return OperationResult.Fail(ex.Message);
            }
        }


        public static OperationResult Delete(decimal isbn) {
            try {
                var existing = GetByIsbn(isbn);
                if (existing == null)
                    return OperationResult.Fail("Book not found.");

                using (var con = DBHelper.GetConnection())
                using (var cmd = new SqlCommand(SpDelete, con)) {
                    cmd.CommandType = CommandType.StoredProcedure;
                    AddIsbnParam(cmd, isbn);

                    con.Open();
                    var affected = cmd.ExecuteNonQuery();

                    if (affected == 0)
                        return OperationResult.Ok("Delete request completed.");

                    return OperationResult.Ok("Book deleted successfully.");
                }
            } catch (SqlException ex) {
                return OperationResult.Fail("Database error. Please try again.");
            } catch {
                return OperationResult.Fail("Unexpected error. Please try again.");
            }
        }

        private static void FillParams(SqlCommand cmd, decimal isbn, string title, string author,
            DateTime publishDate, decimal price, bool publish) {
            AddIsbnParam(cmd, isbn);

            cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 200).Value =
                (object)title ?? DBNull.Value;

            cmd.Parameters.Add("@Author", SqlDbType.NVarChar, 100).Value =
                (object)author ?? DBNull.Value;

            cmd.Parameters.Add("@PublishDate", SqlDbType.DateTime).Value = publishDate;

            var pPrice = cmd.Parameters.Add("@Price", SqlDbType.Decimal);
            pPrice.Precision = 10;
            pPrice.Scale = 2;
            pPrice.Value = price;

            cmd.Parameters.Add("@Publish", SqlDbType.Bit).Value = publish;
        }

        private static void AddIsbnParam(SqlCommand cmd, decimal isbn) {
            var pIsbn = cmd.Parameters.Add("@ISBN", SqlDbType.Decimal);
            pIsbn.Precision = 13;
            pIsbn.Scale = 0;
            pIsbn.Value = isbn;
        }

        private static Book Map(SqlDataReader r) {
            return new Book {
                ISBN = r["ISBN"] == DBNull.Value ? 0m : Convert.ToDecimal(r["ISBN"]),
                Title = r["Title"] == DBNull.Value ? "" : Convert.ToString(r["Title"]),
                Author = r["Author"] == DBNull.Value ? "" : Convert.ToString(r["Author"]),
                PublishDate = r["PublishDate"] == DBNull.Value ? new DateTime(1753, 1, 1) : Convert.ToDateTime(r["PublishDate"]),
                Price = r["Price"] == DBNull.Value ? 0m : Convert.ToDecimal(r["Price"]),
                Publish = r["Publish"] != DBNull.Value && Convert.ToBoolean(r["Publish"])
            };
        }
    }
}
