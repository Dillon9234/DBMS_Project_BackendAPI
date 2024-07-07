namespace DBS_Project_Backend.Models
{
    public class Player
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public string? TeamName { get; set; }
        public float? Rating { get; set; }
        public string? CountryName { get; set; }
        public int Age { get; set; }
    }
}
