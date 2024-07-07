namespace DBS_Project_Backend.Models
{
    public class MatchPlayerStats
    {
        public string Alias { get; set; }
        public int MatchID { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public float TotalDamage { get; set; }
        public int TotalRounds { get; set; }
        public int FirstKills { get; set; }
        public float Rating { get; set; }
        public int Assists { get; set; }
        public int NumberOfCluthes { get; set; }
        public int NumberOfHeadShotKills { get;set; }

    }
}
