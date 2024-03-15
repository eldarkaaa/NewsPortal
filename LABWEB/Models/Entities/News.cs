namespace LABWEB.Models.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string HeadLine { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public int? CategoriesId { get; set; }
        public Categories Categories { get; set; }
        public News()
        {
            Comments = new List<Comments>();
        }
    }
}
