using Microsoft.Data.SqlClient;

namespace PrsLibrary
{
    public class Connection
    {
        //Hard coding is not best way, will change later. String to the sql server for connection.
       private string connectionString = @"server=localhost\sqlexpress01;database=Prs;trusted_connection=true;trustServerCertificate=true;";

        //adding property connection, to create a new instance and set to null to begin.
        //used the Microsoft.Data.SqlClient package install to call the SqlConnection class, ? so it can be null and default null.
        public SqlConnection? sqlConnection { get; set; } = null;

        //creating a method to pass in the sql Select statement to execute
        public void SelectSql(string sql)
        {
            sql = "Select * From Users;";
            SqlCommand sqlcommand = new(sql, sqlConnection); //creating sqlcommand as new, passing in sql, connection from our class.
            SqlDataReader reader = sqlcommand.ExecuteReader(); //Execute Reader returns sqldatareader
            
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["ID"]);
                string? username = Convert.ToString(reader["Username"]);
                string? firstname = Convert.ToString(reader["FirstName"]);
                string? lastname = Convert.ToString(reader["LastName"]);
                System.Diagnostics.Debug.WriteLine($"{id} | {username} | {firstname} | {lastname}");
            }
            reader.Close();
        }

        public void Connect()
        {   //in our method, checking if connection is alraedy null, if yes opening a new connection.
            //if not null, saying the connection already exists
            if(sqlConnection is not null)
            {
                System.Diagnostics.Debug.WriteLine("Connection already exists!");
                return;
            }
            sqlConnection = new SqlConnection(connectionString); //setting a new connection
            sqlConnection.Open();  //opening the connection
            
            //Testing the connection.  If its anything other than .Open it didnt open properly.
            if(sqlConnection.State != System.Data.ConnectionState.Open)
            {  //if it does not open properly, throwing exception
                throw new Exception("Could not make connection to database!");
            }
            //This is written so you can see in Debug output that it opened successfully.
            System.Diagnostics.Debug.WriteLine("Connection open successfully!");
        }

        //Closing the connection, do not want to keep it open, may be a limit.
        public void Disconnect()
        {
            if(sqlConnection is not null) //putting is not null instead of != null.
            {
                sqlConnection.Close(); //if the connection is not null, closing the connection.
                sqlConnection = null; //after its closed, setting connection back to null.
            }
        }
    }
}