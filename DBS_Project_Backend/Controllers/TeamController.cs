using DBS_Project_Backend.DBO;
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
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository repository;
        public TeamController(ITeamRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("GetTeams")]
        public JsonResult GetTeams()
        {
            return repository.GetTeams();
        }

        [HttpGet]
        [Route("GetTeamDetails")]
        public JsonResult getTeam([FromQuery] string Name)
        {
            return repository.GetTeamDetails(Name);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddTeam")]
        public JsonResult AddTeam(TeamDBO team)
        {
            return repository.AddTeam(team);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeletePlayer")]
        public JsonResult DeleteTeam([FromQuery] string Name)
        {
            return repository.DeleteTeam(Name);
        }

        [HttpGet]
        [Route("GetPlayersInTeam")]
        public JsonResult GetPlayersInTeam([FromQuery] string Name)
        {
            return repository.GetPlayersInTeam(Name);
        }

    }
}
