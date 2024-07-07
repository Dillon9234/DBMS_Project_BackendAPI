using DBS_Project_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBS_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository repository;

        public OrganizationController(IOrganizationRepository repository)
        {
            this.repository = repository;   
        }

        [HttpGet]
        [Route("GetOrganizations")]
        public JsonResult GetOrganizations()
        {
            return repository.GetOrgs();
        }

        [HttpGet]
        [Route("GetAnOrganization")]
        public JsonResult GetAnOrganization([FromQuery] string Name)
        {
            return repository.GetOrg(Name);
        }
    }
}
