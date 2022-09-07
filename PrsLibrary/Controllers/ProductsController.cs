using Microsoft.Data.SqlClient;
using PrsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrsLibrary.Controllers
{
    public class ProductsController
    {

        public readonly Connection connection = null;

        private VendorsController vendCtrl = null;

        public ProductsController(Connection conn)
        {

            connection = conn;
            vendCtrl = new(connection);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            string sql = Product.SqlSelectAll;
            SqlCommand cmd = new(sql, connection.sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Product> products = new();
            while (reader.Read())
            {
                Product product = new();
                Product.LoadFromReader(product, reader);
                products.Add(product);

            }
            reader.Close();
            foreach(Product p in products)
            {
                p.Vendor = vendCtrl.GetVendorByPk(p.VendorID);
            }
            return products;
        }

        public Product? GetByPk(int id)
        {
            string sql = Product.SqlSelectByPk;
            SqlCommand cmd = new(sql, connection.sqlConnection);
            Product.SetSqlParameterId(cmd, id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            Product product = new();
            product.LoadFromReader(reader);
            reader.Close();
            product.Vendor = vendCtrl.GetVendorByPk(product.VendorID);
            return product;
        }

        public bool Delete(int id)
        {
            string sql = Product.SqlDelete;
            SqlCommand cmd = new(sql, connection.sqlConnection);
            Product.SetSqlParameterId(cmd, id);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1)
            {
                return false;
            }
            return true;
        }

        
        public bool Insert(Product product)
        {
            string sql = Product.SqlInsert;
            SqlCommand cmd = new(sql, connection.sqlConnection);
            product.SetSqlParameters(cmd);
            int rowsAffected = cmd.ExecuteNonQuery();
            if(rowsAffected != 1)
            {
                return false;
            }
            return true;
        }
        

        
        public bool Update(Product product)
        {
            string sql = Product.SqlUpdate;
            SqlCommand cmd = new(sql, connection.sqlConnection);
            product.SetSqlParameters(cmd);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1)
            {
                return false;
            }
            return true;
        }
       


    }
}
