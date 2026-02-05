using System;

namespace BookLibrary.Models {
    public class Book {
        public decimal ISBN { get; set; }          // numeric(13,0)
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public decimal Price { get; set; }
        public bool Publish { get; set; }
    }
}