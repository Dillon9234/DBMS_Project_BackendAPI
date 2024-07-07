namespace DBS_Project_Backend.Models
{
    public class Match
    {
        public int ID { get; set; }
        public int? TournamentID { get; set; }
        public string TeamAName { get; set; }
        public string TeamBName {  get; set; }
        public DateTime DateTime  { get; set; }
        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }
        public string Status { get; set; }
    }
}
