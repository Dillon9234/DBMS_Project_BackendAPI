using DBS_Project_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBS_Project_Backend.Interfaces
{
    public interface IMatchRepository
    {
        public JsonResult GetMatches();
        public JsonResult GetLiveMatches();
        public JsonResult GetMatchsInATournament(int ID);
        public JsonResult CreateMatch(Match match);
        public JsonResult DeleteMatch(int ID);
        public JsonResult UpdateMatch(Match match);
        public bool MatchExists(int ID);
    }
}
