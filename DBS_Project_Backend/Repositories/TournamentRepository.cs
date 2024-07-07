using DBS_Project_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DBS_Project_Backend.DataBase;
using DBS_Project_Backend.Models;
using System.Xml.Linq;

namespace DBS_Project_Backend.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly DataContext context;
        private readonly ITeamRepository teamRepository;
        public TournamentRepository(DataContext context, ITeamRepository teamRepository)
        {
            this.context = context;
            this.teamRepository = teamRepository;
        }

        public JsonResult AddToTournament(string teamNames,int tournamentID)
        {
            string query = "insert into standings values(@tournamentID, @teamName ,0) ";
            string sqlDataSource = context.createConnection();
            List<string> teamsList = new List<string>(teamNames.Split(','));

            if (!TournamentExists(tournamentID))
                return new JsonResult("Tournament Doesnt Exist");

            foreach(string teamName in  teamsList)
            {
                if (!teamRepository.TeamExists(teamName))
                    return new JsonResult(teamName + " Doesnt Exist");
            }

            foreach(string teamName in teamsList)
            {
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@tournamentID", tournamentID);
                        myCommand.Parameters.AddWithValue("@teamName", teamName);
                        myCommand.ExecuteReader();
                        myCon.Close();
                    }
                }
            }

            return new JsonResult("Added Succesfully");

        }

        public JsonResult CreateTournament(Tournament tournament)
        {
            string query = "exec dbo.AddTournament @tID, @orgUsername, @name,@startDate,@endDate,@description, @location, @gameName ";
            string sqlDataSource = context.createConnection();
            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@tID", tournament.ID);
                        myCommand.Parameters.AddWithValue("@name", tournament.Name);
                        myCommand.Parameters.AddWithValue("@startDate", tournament.StartDate);
                        myCommand.Parameters.AddWithValue("@endDate", tournament.EndDate);
                        myCommand.Parameters.AddWithValue("@description",tournament.Description);
                        myCommand.Parameters.AddWithValue("@location", tournament.Location);
                        if (tournament.GameName != null)
                            myCommand.Parameters.AddWithValue("@gameName", tournament.GameName);
                        else
                            myCommand.Parameters.AddWithValue("@gameName", DBNull.Value);
                        if (tournament.organizerUserName != null)
                            myCommand.Parameters.AddWithValue("@orgUsername", tournament.organizerUserName);
                        else
                            myCommand.Parameters.AddWithValue("@orgUsername", DBNull.Value);
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

        public JsonResult DeleteTournament(int ID)
        {
            if (!TournamentExists(ID))
                return new JsonResult("Tournament Doesnt Exist");
            string query = "delete from dbo.tournament where tID = @ID";
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
            return new JsonResult("Deleted Successfully");

        }

        public JsonResult GetTeamsInTournament(int ID)
        {
            if(!TournamentExists(ID))
                return new JsonResult("Tournament Doesnt Exist");
            string query = "SELECT * FROM (SELECT teamName AS TName FROM standings WHERE tournamentID = @ID) AS temp CROSS APPLY get_team_detail(temp.TName)";

            DataTable table = new DataTable();
            string sqlDataSource = context.createConnection();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", ID);
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

        public JsonResult GetTournaments()
        {
            string query = "select * from tournament";
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

        public bool TournamentExists(int ID)
        {
            string query = "SELECT COUNT(*) FROM dbo.tournament WHERE tID = @ID";
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
    }
}
