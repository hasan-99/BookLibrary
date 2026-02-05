using BookLibrary.Repo;
using System;
using System.Globalization;
using System.Web.UI;

namespace BookLibrary.Pages {
    public partial class BookDetails : Page {
        private string Mode => ( Request.QueryString["mode"] ?? "add" ).ToLowerInvariant();

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                SetupByMode();
            }
        }

        private void SetupByMode() {
            lblMsg.Text = "";

            if (Mode == "add") {
                lblPageTitle.Text = "Add New Book";
                btnConfirmDelete.Visible = false;
                btnSave.Visible = true;
                SetInputsEnabled(true);
                return; // keep empty
            }

            // edit/delete must have isbn
            if (!decimal.TryParse(Request.QueryString["isbn"], out var isbn)) {
                lblMsg.Text = "Missing ISBN.";
                btnSave.Visible = false;
                btnConfirmDelete.Visible = false;
                return;
            }

            var b = BooksRepository.GetByIsbn(isbn);
            if (b == null) {
                lblMsg.Text = "Book not found.";
                btnSave.Visible = false;
                btnConfirmDelete.Visible = false;
                return;
            }

            txtISBN.Text = b.ISBN.ToString(CultureInfo.InvariantCulture);
            txtTitle.Text = b.Title;
            txtAuthor.Text = b.Author;
            txtPublishDate.Text = b.PublishDate.ToString("yyyy-MM-dd");
            txtPrice.Text = b.Price.ToString("0.00", CultureInfo.InvariantCulture);
            chkPublish.Checked = b.Publish;

            if (Mode == "edit") {
                btnSave.Visible = true;
                lblPageTitle.Text = "Edit: " + b.Title;
                btnConfirmDelete.Visible = false;
                txtISBN.Enabled = false; // key shouldn’t change
                SetInputsEnabled(true, keepIsbnDisabled: true);
            } else if (Mode == "delete") {
                lblPageTitle.Text = "Delete: " + b.Title;
                btnSave.Visible = false;
                btnConfirmDelete.Visible = true;
                SetInputsEnabled(false);
            }
        }

        private void SetInputsEnabled(bool enabled, bool keepIsbnDisabled = false) {
            txtISBN.Enabled = enabled && !keepIsbnDisabled;
            txtTitle.Enabled = enabled;
            txtAuthor.Enabled = enabled;
            txtPublishDate.Enabled = enabled;
            txtPrice.Enabled = enabled;
            chkPublish.Enabled = enabled;
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            try {
                if (!decimal.TryParse(txtISBN.Text.Trim(), out var isbn)) {
                    lblMsg.Text = "ISBN must be numeric.";
                    return;
                }

                var title = txtTitle.Text.Trim();
                var author = txtAuthor.Text.Trim();

                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author)) {
                    lblMsg.Text = "Title and Author are required.";
                    return;
                }

                if (!DateTime.TryParseExact(txtPublishDate.Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out var pubDate)) {
                    lblMsg.Text = "Publish Date must be in yyyy-MM-dd format.";
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var price)) {
                    lblMsg.Text = "Price must be a number.";
                    return;
                }

                var publish = chkPublish.Checked;

                if (Mode == "add") {
                    BooksRepository.Insert(isbn, title, author, pubDate, price, publish);
                } else // edit
                  {
                    BooksRepository.Update(isbn, title, author, pubDate, price, publish);
                }

                Response.Redirect("~/Pages/Books.aspx");
            } catch (Exception ex) {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e) {
            try {
                if (!decimal.TryParse(txtISBN.Text.Trim(), out var isbn)) {
                    lblMsg.Text = "Missing ISBN.";
                    return;
                }

                BooksRepository.Delete(isbn);
                Response.Redirect("~/Pages/Books.aspx");
            } catch (Exception ex) {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect("~/Pages/Books.aspx");
        }
    }
}
