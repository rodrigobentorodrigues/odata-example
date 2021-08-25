namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int PressId { get; set; }
        public virtual Press Press { get; set; }
    }
}
