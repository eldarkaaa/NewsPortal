namespace LABWEB.Models.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string HeadLine { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
    }
}
