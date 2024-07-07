namespace DBS_Project_Backend.Models
{
    public class Game
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public float? Rating { get; set; }
    }
}
