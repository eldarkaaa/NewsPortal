namespace LABWEB.Models
{
    public class AddNewsViewModel
    {
        public string HeadLine { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public int? CategoriesId { get; set; }
    }
}
