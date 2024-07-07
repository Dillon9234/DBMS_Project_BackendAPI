using DBS_Project_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DBS_Project_Backend.DataBase;

namespace DBS_Project_Backend.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly DataContext context;
        public NewsRepository(DataContext context)
        {
            this.context = context;
        }
        public JsonResult GetNews()
        {
            string query = "select * from dbo.news";

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

        public JsonResult GetNewsByDate(string date)
        {
            if(!NewsOnDateExists(date))
                return new JsonResult("No Data");
            string query = "select * from dbo.get_news_by_pubdate (@date)";

            DataTable table = new DataTable();
            string sqlDataSource = context.createConnection();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@date",date);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        public bool NewsOnDateExists(string date)
        {
            string query = "SELECT COUNT(*) FROM dbo.get_news_by_pubdate(@date)";
            int count;
            string sqlDataSource = context.createConnection();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@date", date);
                    count = (int)myCommand.ExecuteScalar();
                    myCon.Close();
                }
            }

            return count > 0;
        }
    }
 }

