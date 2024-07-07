using DBS_Project_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBS_Project_Backend.Interfaces
{
    public interface ITournamentRepository
    {
        public bool TournamentExists(int ID);
        public JsonResult GetTournaments();
        public JsonResult GetTeamsInTournament(int ID);
        public JsonResult CreateTournament(Tournament tournament);
        public JsonResult DeleteTournament(int ID);
        public JsonResult AddToTournament(string teamNames, int tournamentID);
    }
}
