using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using W1.Domain.Entities;
using System.Web;

namespace DataBasesAPI
{
    public class DataBaseExplorer
    {
        private static string connectionString =
            @"Data Source=HOME\SQLEXPRESS;Initial Catalog=W1;Integrated Security=True;";

        public static int GetTotalCount(string category = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                cmd = connection.CreateCommand();
                cmd.CommandText = "GetTotalCount";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Category", category);

                connection.Open();
                var obj = cmd.ExecuteReader();
                int totalCount = 0;

                if (obj.Read())
                    totalCount = int.Parse(obj["Count"].ToString());

                connection.Close();
                return totalCount;
            }
        }

        public static IEnumerable<Product> GetProductsFromDataBase(string category = null, int page = 1, int count = 4)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("TakeProducts", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@Count", count);

                connection.Open();
                var obj = cmd.ExecuteReader();

                List<Product> collection = new List<Product>();
                Product product;

                while (obj.Read())
                {
                    product = new Product();
                    product.ProductID = int.Parse(obj["ProductID"].ToString());
                    product.Name = obj["Name"].ToString();
                    product.Category = obj["Category"].ToString();
                    product.Description = obj["Description"].ToString();
                    product.Price = decimal.Parse(obj["Price"].ToString());
                    product.ImageMimeType = obj["ImageMimeType"].ToString();
                    if (product.ImageMimeType != "")
                        product.ImageData = (byte[])obj["ImageData"];
                    collection.Add(product);
                }
                connection.Close();
                return collection;
            }
        }

        public static Product GetProductFromDataBase(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("TakeProduct", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductID", productId);

                connection.Open();
                var obj = cmd.ExecuteReader();

                Product product = null;

                while (obj.Read())
                {
                    product = new Product();
                    product.ProductID = int.Parse(obj["ProductID"].ToString());
                    product.Name = obj["Name"].ToString();
                    product.Category = obj["Category"].ToString();
                    product.Description = obj["Description"].ToString();
                    product.Price = decimal.Parse(obj["Price"].ToString());
                    product.ImageMimeType = obj["ImageMimeType"].ToString();
                    if (product.ImageMimeType != "")
                        product.ImageData = (byte[])obj["ImageData"];
                }

                connection.Close();
                return product;
            }
        }

        public static IEnumerable<string> GetCategories()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetCategories", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();
                var obj = cmd.ExecuteReader();

                List<string> collection = new List<string>();

                while (obj.Read())
                {
                    collection.Add(obj["Category"].ToString());
                }

                connection.Close();
                return collection;
            }
        }

        public static void DeleteProduct(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteProduct", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductID", productId);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void InsertProduct(bool Create, Product product, HttpPostedFileBase image)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                SqlParameter parameter = new SqlParameter();

                cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                if (Create)
                {
                    cmd.CommandText = "InsertProduct";
                }
                else
                {
                    cmd.CommandText = "UpdateProduct";
                    cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                }

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@ImageMimeType", image != null ? image.ContentType : product.ImageMimeType);

                byte[] ImageData = new byte[0];
                if (image != null)
                {
                    ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(ImageData, 0, image.ContentLength);
                    parameter.Value = ImageData;
                }

                cmd.Parameters.AddWithValue("@ImageData", image != null ? ImageData : product.ImageData);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}