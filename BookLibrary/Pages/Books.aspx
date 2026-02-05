<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/MasterPages/Site1.master"
    CodeBehind="Books.aspx.cs"
    Inherits="BookLibrary.Pages.Books" %>

<asp:Content ID="BooksPageContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="books-titlebar">Books</div>

    <asp:Label ID="lblMsg" runat="server" CssClass="msg msg-center" />

    <asp:GridView ID="gvBooks" runat="server"
        AutoGenerateColumns="false"
        DataKeyNames="ISBN"
        CssClass="grid"
        GridLines="None"
        AlternatingRowStyle-CssClass="alt"
        SelectedRowStyle-CssClass="selected-row">
        <Columns>
            <asp:CommandField ShowSelectButton="true" SelectText="Select" />
            <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Author" HeaderText="Author" />
            <asp:BoundField DataField="PublishDate" HeaderText="PublishDate" DataFormatString="{0:dd/MM/yyyy 00:00:00}" />
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Publish" HeaderText="Publish" />
        </Columns>
    </asp:GridView>

    <div class="actions">
        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn" OnClick="btnAdd_Click" />
        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn" OnClick="btnEdit_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn" OnClick="btnDelete_Click" />
    </div>

</asp:Content>

