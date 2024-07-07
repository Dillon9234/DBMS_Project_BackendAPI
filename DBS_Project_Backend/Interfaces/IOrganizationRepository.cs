using Microsoft.AspNetCore.Mvc;

namespace DBS_Project_Backend.Interfaces
{
    public interface IOrganizationRepository
    {
        public JsonResult GetOrgs();
        public JsonResult GetOrg(string name);
    }
}
