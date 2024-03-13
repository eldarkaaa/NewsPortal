namespace LABWEB.Models.Entities
{
    public class Comments
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int? NewsId { get; set; }
        public News News { get; set; }
    }
}
