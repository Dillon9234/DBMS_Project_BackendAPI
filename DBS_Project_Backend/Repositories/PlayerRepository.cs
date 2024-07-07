using DBS_Project_Backend.DataBase;
using DBS_Project_Backend.Interfaces;
using DBS_Project_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace DBS_Project_Backend.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DataContext context;
        private readonly IWebHostEnvironment env;
        public PlayerRepository(DataContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public bool PlayerExists(string Alias)
        {
            string query = "SELECT COUNT(*) FROM GetPlayerDetails(@Alias)";
            int count;
            string sqlDataSource = context.createConnection();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Alias", Alias);
                    count = (int)myCommand.ExecuteScalar();
                    myCon.Close();
                }
            }

            return count > 0;

        }
        public JsonResult GetPlayers()
        {
            string query = "select * from dbo.Player";

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
        public JsonResult AddPlayer(Player player)
        {
            string query = "exec dbo.add_player  @Alias,@Name,@TeamName,@Rating,@CountryName,@Age";

            DataTable table = new DataTable();
            string sqlDataSource = context.createConnection();
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Name", player.Name);
                        myCommand.Parameters.AddWithValue("@Alias", player.Alias);
                        if(player.TeamName != null)
                            myCommand.Parameters.AddWithValue("@TeamName", player.TeamName);
                        else
                            myCommand.Parameters.AddWithValue("@TeamName", DBNull.Value);
                        myCommand.Parameters.AddWithValue("@Rating", player.Rating);
                        if(player.CountryName != null)
                            myCommand.Parameters.AddWithValue("@CountryName", player.CountryName);
                        else
                            myCommand.Parameters.AddWithValue("@CountryName", DBNull.Value);
                        myCommand.Parameters.AddWithValue("@Age", player.Age);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            }catch (SqlException ex)
            {
                return new JsonResult("Invalid Input");
            }

            return new JsonResult("Added Succesfully");
        }

        public JsonResult GetPlayer(string Alias)
        {

            if (!PlayerExists(Alias))
                return new JsonResult("No Data");
            string query = "select * from GetPlayerDetails(@Alias)";

            DataTable table = new DataTable();
            string sqlDataSource = context.createConnection();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Alias", Alias);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        public JsonResult DeletePlayer(string Alias)
        {
            if (!PlayerExists(Alias))
                return new JsonResult("No Data");
            string query = "delete from dbo.Player where alias = @Alias";

            string sqlDataSource = context.createConnection();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Alias", Alias);
                    myCommand.ExecuteReader();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted");
        }
    }
}
