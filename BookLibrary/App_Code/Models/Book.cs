using System;

namespace BookLibrary.App_Code.Models;

public class Book
{
    public long ISBN { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublishDate { get; set; }
    public decimal Price { get; set; }
    public bool Publish { get; set; }
}
