using DBS_Project_Backend.Interfaces;
using DBS_Project_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBS_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentRepository repository;
        public TournamentController(ITournamentRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("GetTournaments")]
        public JsonResult GetTournaments()
        {
            return repository.GetTournaments();
        }

        [HttpGet]
        [Route("GetTeamsInTournament/{ID:int}")]
        public JsonResult GetTeamsInTournament(int ID)
        {
            return repository.GetTeamsInTournament(ID);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("CreateTournament")]
        public JsonResult CreateTournament(Tournament tournament)
        {
            return repository.CreateTournament(tournament);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteTournament/{ID:int}")]
        public JsonResult DeleteTournament(int ID)
        {
            return repository.DeleteTournament(ID);
        }

        [HttpPost]
        [Route("AddTeamToTournament")]
        [Authorize(Roles = "Admin")]
        public JsonResult AddTeamToTournament(string teamNames, int tournamentID)
        {
            return repository.AddToTournament(teamNames, tournamentID);
        }

    }
}
