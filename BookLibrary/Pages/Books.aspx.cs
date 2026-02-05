using BookLibrary.Repo;
using System;
using System.Web.UI;

namespace BookLibrary.Pages {
    public partial class Books : Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                BindGrid();
            }
        }

        private void BindGrid() {
            try {
                gvBooks.DataSource = BooksRepository.GetAll();
                gvBooks.DataBind();
                lblMsg.Text = "";
            } catch (Exception ex) {
                lblMsg.Text = ex.Message;
            }
        }

        private bool TryGetSelectedIsbn(out decimal isbn) {
            isbn = 0;
            if (gvBooks.SelectedDataKey == null)
                return false;

            // DataKey is ISBN (numeric(13,0)) => best to treat as decimal
            return decimal.TryParse(gvBooks.SelectedDataKey.Value.ToString(), out isbn);
        }

        protected void btnAdd_Click(object sender, EventArgs e) {
            Response.Redirect("~/Pages/BookDetails.aspx?mode=add");
        }

        protected void btnEdit_Click(object sender, EventArgs e) {
            if (!TryGetSelectedIsbn(out var isbn)) {
                lblMsg.Text = "Please select a book first.";
                return;
            }
            Response.Redirect($"~/Pages/BookDetails.aspx?mode=edit&isbn={isbn}");
        }

        protected void btnDelete_Click(object sender, EventArgs e) {
            if (!TryGetSelectedIsbn(out var isbn)) {
                lblMsg.Text = "Please select a book first.";
                return;
            }
            Response.Redirect($"~/Pages/BookDetails.aspx?mode=delete&isbn={isbn}");
        }
    }
}
