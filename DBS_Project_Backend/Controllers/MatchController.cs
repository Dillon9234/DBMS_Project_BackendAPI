using DBS_Project_Backend.Interfaces;
using DBS_Project_Backend.Models;
using DBS_Project_Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBS_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchRepository repository;
        public MatchController(IMatchRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("GetMatches")]
        public JsonResult GetMatches()
        {
            return repository.GetMatches();
        }


        [HttpGet]
        [Route("GetLiveMatches")]
        public JsonResult GetLiveMatches()
        { 
            return repository.GetLiveMatches();
        }

        [HttpGet]
        [Route("GetMatchsInATournament/{ID:int}")]
        public JsonResult GetMatchsInATournament(int ID)
        {
            return repository.GetMatchsInATournament(ID);
        }

        [HttpPost]
        [Route("CreateMatch")]
        [Authorize(Roles = "Admin")]
        public JsonResult CreateMatch(Match match)
        {
            return repository.CreateMatch(match);
        }

        [HttpPost]
        [Route("UpdateMatch")]
        [Authorize(Roles = "Admin")]
        public JsonResult UpdateMatch(Match match)
        {
            return repository.UpdateMatch(match);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteMatch/{ID:int}")]
        public JsonResult DeletePlayer(int ID)
        {
            return repository.DeleteMatch(ID);
        }
    }
}
