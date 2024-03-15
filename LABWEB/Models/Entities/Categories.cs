namespace LABWEB.Models.Entities
{
    public class Categories
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public ICollection<News> News { get; set; }

        public Categories()
        {
            News = new List<News>();
        }
    }
}
