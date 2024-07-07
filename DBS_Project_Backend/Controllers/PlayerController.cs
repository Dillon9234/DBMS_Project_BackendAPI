using DBS_Project_Backend.Interfaces;
using DBS_Project_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBS_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerRepository playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }
        [HttpGet]
        [Route("GetPlayers")]
        public JsonResult GetPlayers()
        {
            return playerRepository.GetPlayers();
        }

        [HttpGet("GetPlayer")]
        public JsonResult getPlayers([FromQuery] string Alias)
        {
            return playerRepository.GetPlayer(Alias);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("AddPlayers")]
        public JsonResult AddPlayer(Player player)
        {
            return playerRepository.AddPlayer(player);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeletePlayer")]
        public JsonResult DeletePlayer([FromQuery] string Alias)
        {
            return playerRepository.DeletePlayer(Alias);
        }
    }
}
