using DBS_Project_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBS_Project_Backend.Interfaces
{
    public interface IPlayerRepository
    {
        public bool PlayerExists(string Alias);
        public JsonResult GetPlayers();
        public JsonResult GetPlayer(string Alias);
        public JsonResult AddPlayer(Player player);
        public JsonResult DeletePlayer(string Alias);
    }
}
