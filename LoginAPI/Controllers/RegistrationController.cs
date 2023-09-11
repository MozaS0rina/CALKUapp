using LoginAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //testing
        [HttpPost]
        [Route("register")]
        //register a new user
        public string registration(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("LoginCALKU").ToString());
            SqlCommand cmd = new SqlCommand("Insert into Registration(UserName,Password,Email,IsActive) values('" + registration.UserName + "','" + registration.Password + "','" + registration.Email + "','" + registration.IsActive + "')", con);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                return "Data inserted!";
            }
            else
            {
                return "Error!";
            }
        }
        //testing
        [HttpPost]
        [Route("login")]
        public string login(Registration registration)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("LoginCALKU").ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Registration where Email='" + registration.Email + "' and Password='" + registration.Password + "' and IsActive=1", connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return "Valid user!";
            }
            else
            {
                return "Invalid user!";
            }
        }
        [HttpGet]
        [Route("GetUserData/{Email}")]
        public IActionResult GetUserData(string email)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("LoginCALKU").ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Registration where Email='" + email + "' and IsActive=1", connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Registration user = new Registration
                {
                    Id = (Int32)dt.Rows[0]["Id"],
                    UserName = dt.Rows[0]["UserName"].ToString(),
                    Email = dt.Rows[0]["Email"].ToString(),
                    Password = dt.Rows[0]["Password"].ToString(),
                    IsActive = (Int32)dt.Rows[0]["IsActive"]
                };

                return Ok(user); // Returns the user data as JSON
               
            }
            else
            {
                return NotFound("User not found");
            }
        }



        [HttpPost]
        [Route("RegisterUserAsync/{UserName}/{Password}/{Email}")]
        public async Task<ActionResult<IEnumerable<Registration>>> RegisterUserAsync(string UserName, string Password, string Email)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("LoginCALKU").ToString());
            await con.OpenAsync();
            int IsActive = 1;
            SqlCommand cmd = new SqlCommand("Insert into Registration(UserName, Password, Email, IsActive) values('" + UserName + "', '" + Password + "', '" + Email + "', '" + IsActive + "')", con);
            int i = cmd.ExecuteNonQuery();
            //register
            if (i > 0)
            {
                string query2 = "Select * from Registration where UserName=@UserName and Password=@Password and IsActive=1";
                using (SqlCommand cmd2 = new SqlCommand(query2, con))
                {
                    cmd2.Parameters.AddWithValue("@UserName", UserName);
                    cmd2.Parameters.AddWithValue("@Password", Password);

                    using (SqlDataReader reader = await cmd2.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            await reader.ReadAsync();
                            Registration user = new Registration
                            {
                                Id = (Int32)reader["Id"],
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                IsActive = (Int32)reader["IsActive"]
                            };

                            return Ok(user); // Returns the user inserted data as JSON

                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            else { return NotFound(); }

        }



        [HttpGet]
        [Route("LoginUser/{UserName}/{Password}")]
        public async Task<ActionResult<Response>> LoginUser(string UserName, string Password)
        {
            Response response = new Response();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("LoginCALKU").ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Registration where UserName='" + UserName + "' and Password='" + Password + "' and IsActive=1", con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                response = new Response
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Found"
                };

            }
            else
            {
                response = new Response
                {
                    Success = false,
                    StatusCode = 422,
                    Message = "Error"
                };
            }
            return Ok(response);
        }
        [HttpGet]
        [Route("LoginUserAsync/{UserName}/{Password}")]
        public async Task<ActionResult<IEnumerable<Registration>>> LoginUserAsync(string UserName, string Password)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("LoginCALKU").ToString()))
            {
                await con.OpenAsync();

                string query = "Select * from Registration where UserName=@UserName and Password=@Password and IsActive=1";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            await reader.ReadAsync();
                            Registration user = new Registration
                            {
                                Id = (Int32)reader["Id"],
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                IsActive = (Int32)reader["IsActive"]
                            };

                            return Ok(user); // Returns the user data as JSON
                                             
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
        }
        [HttpDelete]
        [Route("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("LoginCALKU").ToString());
            SqlCommand cmd = new SqlCommand("DELETE FROM Registration WHERE UserId = @UserId", con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("User deleted successfully");
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the deletion process
                return BadRequest("An error occurred while deleting the user: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        [HttpPut]
        [Route("UpdateUser/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] Registration updatedUser)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("LoginCALKU").ToString());
            SqlCommand cmd = new SqlCommand("UPDATE Registration SET UserName = @UserName, Password = @Password, Email = @Email WHERE UserId = @UserId", con);

            cmd.Parameters.AddWithValue("@UserName", updatedUser.UserName);
            cmd.Parameters.AddWithValue("@Password", updatedUser.Password);
            // Add other properties you want to update
            cmd.Parameters.AddWithValue("@Email", updatedUser.Email);

            cmd.Parameters.AddWithValue("@UserId", userId);

            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("User updated successfully");
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the update process
                return BadRequest("An error occurred while updating the user: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        //testing synchronous
        [HttpGet]
        [Route("LoginUserSync/{UserName}/{Password}")]
        public IEnumerable<Registration> LoginUserSync(string UserName, string Password)
        {
            List<Registration> users = new List<Registration>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("LoginCALKU").ToString()))
            {
                con.Open();

                string query = "Select * from Registration where UserName=@UserName and Password=@Password and IsActive=1";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Registration user = new Registration
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                IsActive = reader.GetInt32(reader.GetOrdinal("IsActive"))
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users.Any() ? users : null;
        }
    }
}
