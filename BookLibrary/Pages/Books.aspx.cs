using System;
using BookLibrary.App_Code.DAL;
namespace BookLibrary.Pages;

public partial class Books : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindGrid();
    }

    private void BindGrid()
    {
        gvBooks.DataSource = BookDAL.GetAllBooks(); // DataTable
        gvBooks.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/BookDetails.aspx");
    }

    private bool TryGetSelectedISBN(out long isbn)
    {
        isbn = 0;

        if (gvBooks.SelectedIndex < 0 || gvBooks.SelectedDataKey == null)
        {
            lblMsg.Text = "Please select a book first.";
            return false;
        }

        isbn = Convert.ToInt64(gvBooks.SelectedDataKey.Value);
        return true;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (!TryGetSelectedISBN(out long isbn)) return;
        Response.Redirect("~/Pages/BookDetails.aspx?mode=edit&isbn=" + isbn);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!TryGetSelectedISBN(out long isbn)) return;
        Response.Redirect("~/Pages/BookDetails.aspx?mode=delete&isbn=" + isbn);
    }
}