using PoeAgriEnergyConnect.Models;
using System.Data.SqlClient;
using System.Net;
using System.Xml.Linq;

namespace PoeAgriEnergyConnect
{
    public class DatabaseHelper
    {
        public string connectionString;
        public DatabaseHelper()
        {
            //connectionString = new SqlConnection();
            // Fetch the connection string from the configuration file
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Database1.mdf;Integrated Security=True";
            //connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Database1.mdf;Integrated Security=True;";
            //connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AgriEnergyConnectEntities"].ConnectionString;
        }

        public List<Farmer> ReadFarmersData()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            
                List<Farmer> farmersList   = new List<Farmer>();
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Farmers"; // Replace with your actual table name
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Id = int.Parse(reader["Id"].ToString());
                                //Name = reader["Name"].ToString(),
                                //Address = reader["Address"].ToString(),
                                //Email = reader["Email"].ToString(),
                                //Password = reader["Password"].ToString(),
                                //PhoneNumber = reader["PhoneNumber"].ToString(),
                                //UserType = reader["UserType"].ToString()
                            farmersList.Add(new Farmer
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Address = reader["Address"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                UserType = reader["UserType"].ToString()

                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
           
                return farmersList;
            
        }
        public List<Product> ReadProductsData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Product> productList = new List<Product>();
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Product"; // Replace with your actual table name
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            productList.Add(new Product
                            {
                                Name = reader["Name"].ToString(),
                                Category = reader["Category"].ToString(),
                                ProductionDate = DateTime.Parse(reader["ProductionDate"].ToString()),
                                FarmerId = reader["FarmerId"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
               

                return productList;
            }
        }

        public void InsertFarmer(Farmer farmer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Farmers (Name, Address, PhoneNumber, Email, Password, UserType) " +
                                   "VALUES (@Name, @Address, @PhoneNumber, @Email, @Password, @UserType)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", farmer.Name);
                    command.Parameters.AddWithValue("@Address", farmer.Address);
                    command.Parameters.AddWithValue("@PhoneNumber", farmer.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", farmer.Email);
                    command.Parameters.AddWithValue("@Password", farmer.Password);
                    command.Parameters.AddWithValue("@UserType", farmer.UserType);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
        
            }
        }

        public void InsertProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Product (Name, Category, ProductionDate, FarmerId) " +
                                   "VALUES (@Name, @Category, @ProductionDate, @FarmerId)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Category", product.Category);
                    command.Parameters.AddWithValue("@ProductionDate", product.ProductionDate);
                    command.Parameters.AddWithValue("@FarmerId", product.FarmerId);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

            }
        }
    }
}
