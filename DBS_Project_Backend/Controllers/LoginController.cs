using DBS_Project_Backend.DataBase;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using DBS_Project_Backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DBS_Project_Backend.DBO;
using System.Numerics;

namespace DBS_Project_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController: ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;

        public LoginController(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;

        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login(string username, string password)
        {
            string query = "select * from usertable where username = @username and password = @Password";

            DataTable table = new DataTable();
            string sqlDataSource = context.createConnection();
            SqlDataReader myReader;
            List<User> users;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@username", username);
                    myCommand.Parameters.AddWithValue("@Password", password);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    users = table.AsEnumerable().Select(row => new User
                    {
                        Username = row.Field<string>("Username"),
                        Email = row.Field<string>("Email"),
                        Password = row.Field<string>("password"),
                        Phonenumber = row.Field<string>("phone"),
                        Role = row.Field<string>("Role")
                    }).ToList();
                    myReader.Close();
                    myCon.Close();
                }
            }
            if (table.Rows.Count < 1)
                return new JsonResult("User not found");
            LoginDBO login = new LoginDBO();

            login.token = GenerateKey(users[0]);
            login.role = users[0].Role;
            return new JsonResult(login);
        }

        private string GenerateKey(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        [Route("Signup")]
        [AllowAnonymous]
        public JsonResult Signup(UserDBO user)
        {
            if (UserExists(user.Username))
                return new JsonResult("User Exists");
            string query = "exec SignUpNormalUser @Username, @Password, @Email,@Phone,@Address;";

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
                        myCommand.Parameters.AddWithValue("@Username", user.Username);
                        myCommand.Parameters.AddWithValue("@Password", user.Password);
                        myCommand.Parameters.AddWithValue("@Email", user.Email);
                        myCommand.Parameters.AddWithValue("@Phone", user.Phonenumber);
                        myCommand.Parameters.AddWithValue("@Address", user.Address);
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
        private bool UserExists(string username)
        {
            string query = "SELECT COUNT(*) FROM Usertable where username = @username";
            int count;
            string sqlDataSource = context.createConnection();
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@username", username);
                    count = (int)myCommand.ExecuteScalar();
                    myCon.Close();
                }
            }

            return count > 0;
        }
    }
}
