namespace DBS_Project_Backend.Models
{
    public class Team
    {
        public string Name { get; set; }
        public float rating { get; set; }
        public string? OrganizationName { get; set; }
        public string? gameName { get; set; }
        public int? RegionID { get; set; }
    }
}
