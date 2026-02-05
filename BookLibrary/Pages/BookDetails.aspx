<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/MasterPages/Site1.master"
    CodeBehind="BookDetails.aspx.cs"
    Inherits="BookLibrary.Pages.BookDetails" %>

<asp:Content ID="BookDetailsPageContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="books-titlebar">
        <asp:Label ID="lblPageTitle" runat="server" Text="Add New Book"></asp:Label>
    </div>

    <asp:Label ID="lblMsg" runat="server" CssClass="msg" />

    <table style="margin: 0 16px 10px 16px; font-family: Arial; font-size: 14px;">
        <tr>
            <td style="width: 160px;">ISBN</td>
            <td>
                <asp:TextBox ID="txtISBN" runat="server" /></td>
        </tr>
        <tr>
            <td>Title</td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="360" /></td>
        </tr>
        <tr>
            <td>Author</td>
            <td>
                <asp:TextBox ID="txtAuthor" runat="server" Width="360" /></td>
        </tr>
        <tr>
            <td>Publish Date (yyyy-MM-dd)</td>
            <td>
                <asp:TextBox ID="txtPublishDate" runat="server" /></td>
        </tr>
        <tr>
            <td>Price</td>
            <td>
                <asp:TextBox ID="txtPrice" runat="server" /></td>
        </tr>
        <tr>
            <td>Publish</td>
            <td>
                <asp:CheckBox ID="chkPublish" runat="server" /></td>
        </tr>
    </table>

    <div class="actions">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn"  OnClientClick="return validateBookDetails();"
    OnClick="btnSave_Click" />
        <asp:Button ID="btnConfirmDelete" runat="server" Text="Delete" CssClass="btn" OnClick="btnConfirmDelete_Click" Visible="false" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancel_Click" />
    </div>


</asp:Content>

