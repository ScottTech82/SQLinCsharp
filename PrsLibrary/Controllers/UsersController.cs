using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PrsLibrary.Models;

namespace PrsLibrary.Controllers
{

    public class UsersController
    {
        public Connection connection = null;

        public UsersController(Connection conn) //creating a contstructor and passing in the connection variable.
        {

        connection = conn; //setting conn equal to connection, rather than just listing connection.

        }
        
        public IEnumerable<User> GetAllUsers()
        {
            string sql = "SELECT * From Users;";
            SqlCommand cmd = new(sql, connection.sqlConnection); 
            SqlDataReader reader = cmd.ExecuteReader();
            List<User> users = new();
            while (reader.Read())
            {
                User user = new();
                user.ID = Convert.ToInt32(reader["ID"]);
                user.Username = Convert.ToString(reader["Username"]);
                user.Password = Convert.ToString(reader["Password"]);
                user.FirstName = Convert.ToString(reader["FirstName"]);
                user.LastName = Convert.ToString(reader["LastName"]);
                        //Since Phone can be Null, we have to add if else statement
                if (reader["Phone"] == System.DBNull.Value)
                {
                    user.Phone = null;
                } 
                else
                {
                    user.Phone = Convert.ToString(reader["Phone"]);
                }
                    //Email can be Null.
                if (reader["Email"] == System.DBNull.Value)
                {
                    user.Email = null;
                } 
                else
                {
                    user.Email = Convert.ToString(reader["Email"]);
                }
                user.IsReviewer = Convert.ToBoolean(reader["IsReviewer"]);
                user.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                //after getting all the data, add the user.
                users.Add(user);
            }
            reader.Close();
            return users; //after the while statement return users.
        }

        public User? GetByPk(int id) //create method GetByPk and want user to pass in an int called id.
        {
            string sql = $"SELECT * From Users Where Id = {id};"; //interpol to allow id passed in and search.
            SqlCommand cmd = new(sql, connection.sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            if(!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
                User user = new();
                user.ID = Convert.ToInt32(reader["ID"]);
                user.Username = Convert.ToString(reader["Username"]);
                user.Password = Convert.ToString(reader["Password"]);
                user.FirstName = Convert.ToString(reader["FirstName"]);
                user.LastName = Convert.ToString(reader["LastName"]);
                //Since Phone can be Null, we have to add if else statement
                if (reader["Phone"] == System.DBNull.Value)
                {
                    user.Phone = null;
                }
                else
                {
                    user.Phone = Convert.ToString(reader["Phone"]);
                }
                //Email can be Null.
                if (reader["Email"] == System.DBNull.Value)
                {
                    user.Email = null;
                }
                else
                {
                    user.Email = Convert.ToString(reader["Email"]);
                }
                user.IsReviewer = Convert.ToBoolean(reader["IsReviewer"]);
                user.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
               
                
            
            reader.Close();
            return user; //after the while statement return users.
        }

        public bool Delete(int id)
        {
            string sql = $"DELETE From Users Where ID = {id};";
            SqlCommand cmd = new(sql, connection.sqlConnection);
            //using ExecuteNonQuery for Delete statements.  Read is for Select statements.
            int rowsAffected = cmd.ExecuteNonQuery();
            if(rowsAffected != 1)
            {
                return false;
            }
            return true;
        }
    
        public bool Insert(User user) //pass all the data except the id, which will be 0 indicating trying to add.
        {
            string sql = $" INSERT Into Users " +
                        "( Username, Password, FirstName, LastName, " +
                        " Phone, Email, IsReviewer, IsAdmin) VALUES " +
                        " (@Username, @Password, @FirstName, @LastName, " +
                        " @Phone, @Email, @IsReviewer, @IsAdmin ); ";
            SqlCommand cmd = new(sql, connection.sqlConnection);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@IsReviewer", user.IsReviewer);
            cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
            int rowsAffected = cmd.ExecuteNonQuery();
            if(rowsAffected != 1)
            {
                return false;
            }
            return true;
        }
        
        public bool Update(User user)
                {
                    string sql = $" UPDATE Users Set " +
                                " Username = @Username, " +
                                " Password = @Password, " +
                                " FirstName = @FirstName, " +
                                " LastName = @LastName, " +
                                " Phone = @Phone, " +
                                " Email = @Email, " +
                                " IsReviewer = @IsReviewer, " +
                                " IsAdmin = @IsAdmin " +
                                " Where Id = @ID; ";
                    SqlCommand cmd = new(sql, connection.sqlConnection);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Phone", user.Phone);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@IsReviewer", user.IsReviewer);
                    cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                    cmd.Parameters.AddWithValue("@Id", user.ID);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if(rowsAffected != 1)
                    {
                        return false;
                    }
                    return true;
                }
        }
    
    
    }



    
    
    




