using DBS_Project_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DBS_Project_Backend.DataBase;
using DBS_Project_Backend.Models;
using System.Numerics;
using DBS_Project_Backend.DBO;

namespace DBS_Project_Backend.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext context;
        public TeamRepository(DataContext context) {
            this.context = context;
        }

        public JsonResult AddTeam(TeamDBO team)
        {
            string query = "exec dbo.add_team @Name,@Rating, @OrganizationName, @GameName, @RegionName ";
            string sqlDataSource = context.createConnection();
            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Name", team.Name);
                        myCommand.Parameters.AddWithValue("@Rating", team.rating);
                        if (team.RegionName != null)
                            myCommand.Parameters.AddWithValue("@RegionName", team.RegionName);
                        else
                            myCommand.Parameters.AddWithValue("@RegionName", DBNull.Value);
                        if (team.gameName != null)
                            myCommand.Parameters.AddWithValue("@GameName", team.gameName);
                        else
                            myCommand.Parameters.AddWithValue("@GameName", DBNull.Value);
                        if (team.OrganizationName != null)
                            myCommand.Parameters.AddWithValue("@OrganizationName", team.OrganizationName);
                        else
                            myCommand.Parameters.AddWithValue("@OrganizationName", DBNull.Value);
                        myCommand.ExecuteReader();
                        myCon.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                return new JsonResult("Invalid Input");
            }

            return new JsonResult("Added Succesfully");
        }

        public JsonResult DeleteTeam(string Name)
        {
            if (!TeamExists(Name))
                return new JsonResult("No Data");
            string query = "delete from dbo.team where name = @Name";
            string sqlDataSource = context.createConnection();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Name", Name);
                    myCommand.ExecuteReader();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");

        }

        public JsonResult GetPlayersInTeam(string Name)
        {
            if (!TeamExists(Name))
                return new JsonResult("No Data");
            string query = "select * from player where teamName = @teamName;";

            DataTable table = new DataTable();
            string sqlDataSource = context.createConnection();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@teamName", Name);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        public JsonResult GetTeamDetails(string Name)
        {
            if (!TeamExists(Name))
                return new JsonResult("No Data");
            string query = "select * from dbo.get_team_detail (@Name)";

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

            return new JsonResult(table);
        }
        public JsonResult GetTeams()
        {
            string query = "select * from team";

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
            if (table.Rows.Count < 1)
                return new JsonResult("No Data");

            return new JsonResult(table);
        }

        public bool TeamExists(string Name)
        {
            string query = "SELECT COUNT(*) FROM dbo.get_team_detail(@Name)";
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
