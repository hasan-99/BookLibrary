
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Books</h2>

    <asp:Label ID="lblMsg" runat="server" />

    <asp:GridView ID="gvBooks" runat="server"
        AutoGenerateColumns="false"
        DataKeyNames="ISBN">
        <Columns>
            <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Author" HeaderText="Author" />
            <asp:BoundField DataField="PublishDate" HeaderText="Publish Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:N2}" />
            <asp:CheckBoxField DataField="Publish" HeaderText="Publish" />
            <asp:CommandField ShowSelectButton="true" />
        </Columns>
    </asp:GridView>

    <br />

    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />

</asp:Content>