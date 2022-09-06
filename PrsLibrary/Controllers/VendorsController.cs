using Microsoft.Data.SqlClient;
using PrsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrsLibrary.Controllers
{
    public class VendorsController
    {
        public Connection connection = null;

        public VendorsController(Connection conn) //creating a contstructor and passing in the connection variable.
        {

            connection = conn; //setting conn equal to connection, rather than just listing connection.

        }

        public IEnumerable<Vendor> GetAllVendors()
        {
            string sql = "SELECT * From Vendors;";
            SqlCommand cmd = new(sql, connection.sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Vendor> vendors = new();
            while (reader.Read())
            {
                Vendor vendor = new();
                vendor.ID = Convert.ToInt32(reader["ID"]);
                vendor.Code = Convert.ToString(reader["Code"]);
                vendor.Name = Convert.ToString(reader["Name"]);
                vendor.Address = Convert.ToString(reader["Address"]);
                vendor.City = Convert.ToString(reader["City"]);
                vendor.State = Convert.ToString(reader["State"]);
                vendor.Zip = Convert.ToString(reader["Zip"]);
                //Since Phone can be Null, we have to add if else statement
                if (reader["Phone"] == System.DBNull.Value)
                {
                    vendor.Phone = null;
                }
                else
                {
                    vendor.Phone = Convert.ToString(reader["Phone"]);
                }
                //Email can be Null.
                if (reader["Email"] == System.DBNull.Value)
                {
                    vendor.Email = null;
                }
                else
                {
                    vendor.Email = Convert.ToString(reader["Email"]);
                }
                
                vendors.Add(vendor);
            }
            reader.Close();
            return vendors; //after the while statement return users.
        }

        public Vendor? GetVendorByPk(int id) //create method GetByPk and want user to pass in an int called id.
        {
            string sql = $"SELECT * From Vendors Where Id = {id};"; //interpol to allow id passed in and search.
            SqlCommand cmd = new(sql, connection.sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            Vendor vendor = new();
            vendor.ID = Convert.ToInt32(reader["ID"]);
            vendor.Code = Convert.ToString(reader["Code"]);
            vendor.Name = Convert.ToString(reader["Name"]);
            vendor.Address = Convert.ToString(reader["Address"]);
            vendor.City = Convert.ToString(reader["City"]);
            vendor.State = Convert.ToString(reader["State"]);
            vendor.Zip = Convert.ToString(reader["Zip"]);
            //Since Phone can be Null, we have to add if else statement
            if (reader["Phone"] == System.DBNull.Value)
            {
                vendor.Phone = null;
            }
            else
            {
                vendor.Phone = Convert.ToString(reader["Phone"]);
            }
            //Email can be Null.
            if (reader["Email"] == System.DBNull.Value)
            {
                vendor.Email = null;
            }
            else
            {
                vendor.Email = Convert.ToString(reader["Email"]);
            }

            reader.Close();
            return vendor; //after the while statement return users.
           
        }

        public bool Delete(int id)
        {
            string sql = $"DELETE From Vendors Where ID = {id};";
            SqlCommand cmd = new(sql, connection.sqlConnection);
            //using ExecuteNonQuery for Delete statements.  Read is for Select statements.
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1)
            {
                return false;
            }
            return true;
        }

        public bool Insert(Vendor vendor) //pass all the data except the id, which will be 0 indicating trying to add.
        {
            string sql = $" INSERT Into Vendors " +
                        "( Code, Name, Address, City, State, Zip, " +
                        " Phone, Email) VALUES " +
                        " (@Code, @Name, @Address, @City, " +
                        " @State, @Zip, @Phone, @Email ); ";
            SqlCommand cmd = new(sql, connection.sqlConnection);
            cmd.Parameters.AddWithValue("@Code", vendor.Code);
            cmd.Parameters.AddWithValue("@Name", vendor.Name);
            cmd.Parameters.AddWithValue("@Address", vendor.Address);
            cmd.Parameters.AddWithValue("@City", vendor.City);
            cmd.Parameters.AddWithValue("@State", vendor.State);
            cmd.Parameters.AddWithValue("@Zip", vendor.Zip);
            cmd.Parameters.AddWithValue("@Phone", vendor.Phone);
            cmd.Parameters.AddWithValue("@Email", vendor.Email);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1)
            {
                return false;
            }
            return true;
        }

        public bool Update(Vendor vendor)
        {
            string sql = $" UPDATE Vendors Set " +
                        " Code = @Code, " +
                        " Name = @Name, " +
                        " Address = @Address, " +
                        " City = @City, " +
                        " State = @State, " +
                        " Zip = @Zip, " +
                        " Phone = @Phone, " +
                        " Email = @Email " +
                        " Where Id = @ID; ";
            SqlCommand cmd = new(sql, connection.sqlConnection);
            cmd.Parameters.AddWithValue("@Code", vendor.Code);
            cmd.Parameters.AddWithValue("@Name", vendor.Name);
            cmd.Parameters.AddWithValue("@Address", vendor.Address);
            cmd.Parameters.AddWithValue("@City", vendor.City);
            cmd.Parameters.AddWithValue("@State", vendor.State);
            cmd.Parameters.AddWithValue("@Zip", vendor.Zip);
            cmd.Parameters.AddWithValue("@Phone", vendor.Phone);
            cmd.Parameters.AddWithValue("@Email", vendor.Email);
            cmd.Parameters.AddWithValue("@Id", vendor.ID);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1)
            {
                return false;
            }
            return true;
        }
    }
}
