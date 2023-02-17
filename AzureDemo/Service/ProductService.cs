using AzureDemo.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDemo.Service
{
    public class ProductService
    {
        private static string db_source = "azuredemosqlserver204.database.windows.net";
        private static string db_userName = "sqladmin";
        private static string db_password = "azuredemo@123";
        private static string db_database = "azuredemosqlserver";


        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_userName;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection sqlConnection = GetConnection();
            List<Product> productsList = new List<Product>();
            string statement = "Select ProductId,ProductName,Quantity From Products";
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(statement, sqlConnection);
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = rdr.GetInt32(0),
                        ProductName = rdr.GetString(1),
                        Quantity = rdr.GetInt32(2)
                    };
                    productsList.Add(product);
                }
            }
            sqlConnection.Close();
            return productsList;
        }
    }
}
