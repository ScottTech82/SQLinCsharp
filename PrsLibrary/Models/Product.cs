using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrsLibrary.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string PartNbr { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Unit { get; set; } = "Each";
        public string? PhotoPath { get; set; } = null;
        public int VendorID { get; set; }
          //virtual instance of the foreign key
        public virtual Vendor Vendor { get; set; }

        public static string SqlSelectAll = " SELECT * " +
                                            " From Products;";
        
        public static string SqlSelectByPk = "SELECT * From Products Where ID = @ID;";
       
        public static string SqlDelete = $"DELETE From Products Where ID = @ID;";

        public static void SetSqlParameterId(SqlCommand cmd, int id)
        {
            cmd.Parameters.AddWithValue("@ID", id);
        }

        //creating a static method to pass in the info instead of typing it out in controller multiple times
        public static void LoadFromReader(Product product, SqlDataReader reader)
        {
            product.ID = Convert.ToInt32(reader["ID"]);
            product.PartNbr = Convert.ToString(reader["PartNbr"]);
            product.Name = Convert.ToString(reader["Name"]);
            product.Price = Convert.ToDecimal(reader["Price"]);
            product.Unit = Convert.ToString(reader["Unit"]);
            product.PhotoPath = Convert.ToString(reader["PhotoPath"]);
            product.VendorID = Convert.ToInt32(reader["VendorID"]);
            
        }

        //instance Method.  Meaning we have to get an instance of product
        //we can have the instance method call the static method, not reverse. 
        //passing the above static method so we dont have to duplicate the data/code, instance of this.
        public void LoadFromReader(SqlDataReader reader)
        {
            LoadFromReader(this, reader);

        }

        
        public static string SqlUpdate =
                       $" UPDATE Products Set " +
                       " PartNbr = @PartNbr, " +
                       " Name = @Name, " +
                       " Price = @Price, " +
                       " Unit = @Unit, " +
                       " PhotoPath = @PhotoPath, " +
                       " VendorID = @VendorID, " +
                       " Where Id = @ID; ";
     
      public static string SqlInsert =
                        $" INSERT Into Products " +
                        "( PartNbr, Name, Price, Unit, PhotoPath, VendorID ) VALUES " +
                        " (@PartNbr, @Name, @Price, @Unit, " +
                        " @PhotoPath, @VendorID ); ";

        public void SetSqlParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@PartNbr", PartNbr);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Price", Price);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@PhotoPath", PhotoPath);
            cmd.Parameters.AddWithValue("@VendorID", VendorID);
            SetSqlParameterId(cmd, ID);
        }

    }
}
