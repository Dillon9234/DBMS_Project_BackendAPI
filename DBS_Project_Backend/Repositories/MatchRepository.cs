using DBS_Project_Backend.DataBase;
using DBS_Project_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DBS_Project_Backend.Models;
using System.Numerics;

namespace DBS_Project_Backend.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly DataContext context;
        public MatchRepository(DataContext context)
        {
            this.context = context;
        }
        public JsonResult GetLiveMatches()
        {
            string query = "SELECT * FROM MATCHES where status = 'live'";

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

        public JsonResult GetMatches()
        {
            string query = "SELECT * FROM MATCHES";

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

        public JsonResult GetMatchsInATournament(int ID)
        {
            string query = "SELECT * FROM MATCHES where tournamentID = @tournamentID";

            DataTable table = new DataTable();
            string sqlDataSource = context.createConnection();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@tournamentID", ID);
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

        public JsonResult CreateMatch(Match match)
        {
            string query = "exec dbo.CreateMatch  @matchID, @tournamentID, @matchDate, @teamAName, @teamBName, @teamAscore, @teamBscore, @status";

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
                        myCommand.Parameters.AddWithValue("@matchID", match.ID);
                        myCommand.Parameters.AddWithValue("@tournamentID", match.TournamentID);
                        myCommand.Parameters.AddWithValue("@matchDate", match.DateTime);
                        myCommand.Parameters.AddWithValue("@teamAName", match.TeamAName);
                        myCommand.Parameters.AddWithValue("@teamBName", match.TeamBName);
                        myCommand.Parameters.AddWithValue("@teamAscore",match.TeamAScore);
                        myCommand.Parameters.AddWithValue("@teamBscore", match.TeamBScore);
                        myCommand.Parameters.AddWithValue("@status", match.Status);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
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

        public JsonResult UpdateMatch(Match match)
        {
            if (!MatchExists(match.ID))
                return new JsonResult("No Data");

            string query = "exec dbo.UpdateMatch  @matchID, @tournamentID, @matchDate, @teamAName, @teamBName, @teamAscore, @teamBscore, @status";

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
                        myCommand.Parameters.AddWithValue("@matchID", match.ID);
                        myCommand.Parameters.AddWithValue("@tournamentID", match.TournamentID);
                        myCommand.Parameters.AddWithValue("@matchDate", match.DateTime);
                        myCommand.Parameters.AddWithValue("@teamAName", match.TeamAName);
                        myCommand.Parameters.AddWithValue("@teamBName", match.TeamBName);
                        myCommand.Parameters.AddWithValue("@teamAscore", match.TeamAScore);
                        myCommand.Parameters.AddWithValue("@teamBscore", match.TeamBScore);
                        myCommand.Parameters.AddWithValue("@status", match.Status);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                return new JsonResult("Invalid Input");
            }

            return new JsonResult("Updated Succesfully");
        }

        public bool MatchExists(int ID)
        {
            string query = "SELECT COUNT(*) FROM Matches where matchID = @ID";
            int count;
            string sqlDataSource = context.createConnection();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", ID);
                    count = (int)myCommand.ExecuteScalar();
                    myCon.Close();
                }
            }

            return count > 0;

        }
        public JsonResult DeleteMatch(int ID)
        {
            if (!MatchExists(ID))
                return new JsonResult("No Data");
            string query = "delete from dbo.matches where matchID = @ID";

            string sqlDataSource = context.createConnection();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", ID);
                    myCommand.ExecuteReader();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted");
        }
    }
}
