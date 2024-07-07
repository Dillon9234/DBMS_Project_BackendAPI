using DBS_Project_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DBS_Project_Backend.DataBase;

namespace DBS_Project_Backend.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly DataContext context;
        public RegionRepository(DataContext context) {
            this.context = context;
        }
        public JsonResult GetRegions()
        {
            string query = "select * from region";

            DataTable table = new DataTable();
            string sqlDataSource = context.createConnection();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            if (table.Rows.Count <1)
            {
                return new JsonResult("No Data");
            }

            return new JsonResult(table);
        }

        public JsonResult GetTeamsInARegion(string Name)
        {
            if (!RegionExists(Name))
                return new JsonResult("Region Doesnt Exist");
            string query = "select * from dbo.get_teams_in_region (@Name)";

            DataTable table = new DataTable();
            string sqlDataSource = context.createConnection();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Name", Name);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            if (table.Rows.Count < 1)
                return new JsonResult("No Data");

            return new JsonResult(table);
        }

        public bool RegionExists(string Name)
        {
            string query = "select count(*) from region where name = @Name";
            int count;
            string sqlDataSource = context.createConnection();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Name", Name);
                    count = (int)myCommand.ExecuteScalar();
                    myCon.Close();
                }
            }

            return count > 0;
        }
    }
}
