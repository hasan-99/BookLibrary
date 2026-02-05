function validateBookDetails() {
    var isbn = document.getElementById('<%= txtISBN.ClientID %>').value.trim();
    var title = document.getElementById('<%= txtTitle.ClientID %>').value.trim();
    var author = document.getElementById('<%= txtAuthor.ClientID %>').value.trim();
    var publishDate = document.getElementById('<%= txtPublishDate.ClientID %>').value.trim();
    var price = document.getElementById('<%= txtPrice.ClientID %>').value.trim();

    if (isbn === "" || isNaN(isbn)) { alert("ISBN must be numeric"); return false; }
    if (title === "") { alert("Title is required"); return false; }
    if (author === "") { alert("Author is required"); return false; }

    // yyyy-MM-dd check (simple)
    var re = /^\d{4}-\d{2}-\d{2}$/;
    if (!re.test(publishDate)) { alert("Publish Date must be yyyy-MM-dd"); return false; }

    if (price === "" || isNaN(price)) { alert("Price must be a number"); return false; }

    return true;
}
