using Microsoft.AspNetCore.Mvc;

namespace DBS_Project_Backend.Interfaces
{
    public interface INewsRepository
    {
        public JsonResult GetNews();
        public JsonResult GetNewsByDate(string date);
        public bool NewsOnDateExists(string date);
    }
}
