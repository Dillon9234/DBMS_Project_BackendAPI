using DBS_Project_Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DBS_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository repository;
        public NewsController(INewsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("GetNews")]
        public JsonResult GetNews()
        {
            return repository.GetNews();
        }

        [HttpGet]
        [Route("GetNewsByDate/{dateTime}")]
        public JsonResult GetNewsByDate(string dateTime)
        {
            return repository.GetNewsByDate(dateTime);
        }
    }
}
