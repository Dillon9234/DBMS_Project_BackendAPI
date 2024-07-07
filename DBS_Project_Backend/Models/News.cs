namespace DBS_Project_Backend.Models
{
    public class News
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateOnly PublishDate { get; set; }
    }
}
