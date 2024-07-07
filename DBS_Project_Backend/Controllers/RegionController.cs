using DBS_Project_Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBS_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository repository;
        public RegionController(IRegionRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("GetRegions")]
        public JsonResult GetRegions()
        {
            return repository.GetRegions();
        }

        [HttpGet("GetTeamsInARegion")]
        public JsonResult GetTeamsInARegion([FromQuery] string Name)
        {
            return repository.GetTeamsInARegion(Name);
        }
    }
}
