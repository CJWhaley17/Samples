using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Models.Restaurants;

namespace Data.Repositories.DBRepositories
{
    public class DbRestaurantRepositories : IRepo
    {
        public List<Restaurant> ListRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Restaurant ";
                cmd.Connection = cn;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        restaurants.Add(PopulateRestaurantFromDataReader(dr));
                    }
                }
            }

            return restaurants;
        }

        public Restaurant GetRestaurant(int id)
        {
            Restaurant restaurant = new Restaurant();

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "GetRestaurantById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@RestaurantId", id);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        restaurant = PopulateRestaurantFromDataReader(dr);

                    }
                    
                }

                return restaurant;

            }
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AddRestaurant";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                if (string.IsNullOrEmpty(restaurant.Name))
                {
                    restaurant.Name = "NA";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RestaurantName", restaurant.Name);
                }
                if (string.IsNullOrEmpty(restaurant.Address))
                {
                    restaurant.Address = "NA";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RestaurantAddress", restaurant.Address);
                }
                if (string.IsNullOrEmpty(restaurant.PhoneNumber))
                {
                    restaurant.PhoneNumber = "NA";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RestaurantPhoneNumber", restaurant.PhoneNumber);
                }
                if (string.IsNullOrEmpty(restaurant.Rating.ToString()))
                {
                    restaurant.Rating = 0;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RestaurantRating", restaurant.Rating);
                }
                if (string.IsNullOrEmpty(restaurant.Website))
                {
                    restaurant.Website = "NA";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RestaurantWebsite", restaurant.Website);
                }

                cn.Open();

                cmd.ExecuteNonQuery();

            }
        }

        public void EditRestaurant(Restaurant restaurant)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EditRestaurant";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@RestaurantId", restaurant.RestId);
                cmd.Parameters.AddWithValue("@RestaurantName", restaurant.Name);
                cmd.Parameters.AddWithValue("@RestaurantAddress", restaurant.Address);
                cmd.Parameters.AddWithValue("@RestaurantPhoneNumber", restaurant.PhoneNumber);
                cmd.Parameters.AddWithValue("@RestaurantRating", restaurant.Rating);
                cmd.Parameters.AddWithValue("@RestaurantWebsite", restaurant.Website);
                


                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveRestaurant(int id)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DeleteRestaurant";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@RestaurantId", id);


                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }


        public Restaurant PopulateRestaurantFromDataReader(SqlDataReader dr)
        {
            Restaurant restaurant = new Restaurant();

            restaurant.RestId = (int)dr["RestaurantId"];
            restaurant.Name = dr["RestaurantName"].ToString();
            restaurant.Address = dr["RestaurantAddress"].ToString();
            restaurant.PhoneNumber = dr["PhoneNumber"].ToString();
            restaurant.Website = dr["Website"].ToString() ?? String.Empty;
            restaurant.Rating = (decimal) dr["Rating"];

            return restaurant;
        }
    }
}
