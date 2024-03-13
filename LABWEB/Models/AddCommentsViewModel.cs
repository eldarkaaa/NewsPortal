using LABWEB.Models.Entities;

namespace LABWEB.Models
{
    public class AddCommentsViewModel
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public int? NewsId { get; set; }
        public News News { get; set; }
    }
}
