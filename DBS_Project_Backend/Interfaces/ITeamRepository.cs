using DBS_Project_Backend.DBO;
using DBS_Project_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBS_Project_Backend.Interfaces
{
    public interface ITeamRepository
    {
        public bool TeamExists(string Name);
        public JsonResult GetTeams();
        public JsonResult GetTeamDetails(string Name);
        public JsonResult DeleteTeam(string Name);
        public JsonResult AddTeam(TeamDBO team);
        public JsonResult GetPlayersInTeam(string Name);
    }
}
