namespace DBS_Project_Backend.Models
{
    public class Tournament
    {
        public int ID { get; set; }
        public string? organizerUserName { get; set; }
        public string? Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string? Description {  get; set; }
        public string? Location { get; set; }
        public string? GameName { get; set; }
    }
}
