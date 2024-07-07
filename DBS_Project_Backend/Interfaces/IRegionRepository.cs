using Microsoft.AspNetCore.Mvc;

namespace DBS_Project_Backend.Interfaces
{
    public interface IRegionRepository
    {
        public bool RegionExists(string Name);
        public JsonResult GetRegions();
        public JsonResult GetTeamsInARegion(string Name);
    }
}
