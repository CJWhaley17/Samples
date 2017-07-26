using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Models.Postings;

namespace Data.Repositories.DBRepositories
{
    public class DbPostingRepository : IPost
    {
        

        public List<Post> ListPostings()
        {
            List<Post> posts = new List<Post>();

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Post";
                cmd.Connection = cn;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        posts.Add(PopulatePostFromDataReader(dr));
                    }
                }
            }

            return posts;
        }

        public Post GetPost(int id)
        {
            Post post = new Post();

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GetPostById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@PostId", id);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        post = PopulatePostFromDataReader(dr);
                    }
                }

            }

            return post;

        }

        public void AddPost(Post post)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AddPost";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                if (string.IsNullOrEmpty(post.Title))
                {
                    post.Title = "Not Assigned";
                    cmd.Parameters.AddWithValue("@Title", post.Title);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Title", post.Title);
                }
                if (string.IsNullOrEmpty(post.ReviewText))
                {
                    post.ReviewText = "Sample Review";
                    cmd.Parameters.AddWithValue("@Review", post.ReviewText);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Review", post.ReviewText);
                }
                if (string.IsNullOrEmpty(post.PostOn.ToString()))
                {
                    post.PostOn = DateTime.Now;
                    cmd.Parameters.AddWithValue("@PostOnDate", post.PostOn);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PostOnDate", post.PostOn);
                }
                if (string.IsNullOrEmpty(post.DeleteOn.ToString()))
                {
                    post.DeleteOn = DateTime.Now;
                    cmd.Parameters.AddWithValue("@PostDeleteDate", post.DeleteOn);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PostDeleteDate", post.DeleteOn);
                }
                if (string.IsNullOrEmpty(post.Restaurant.RestId.ToString()))
                {
                    post.Restaurant.RestId = 1;
                    cmd.Parameters.AddWithValue("@RestaurantId", post.Restaurant.RestId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RestaurantId", post.Restaurant.RestId);
                }
                if (string.IsNullOrEmpty(post.Status.ToString()))
                {
                    post.Status = 0;
                    cmd.Parameters.AddWithValue("@PostStatus", post.Status);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PostStatus", post.Status);
                }
                if (string.IsNullOrEmpty(post.Status.ToString()))
                {
                    post.Status = 0;
                    cmd.Parameters.AddWithValue("@EmployeePostId", post.Employee.EmployeeId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@EmployeePostId", post.Employee.EmployeeId);
                }
                if (string.IsNullOrEmpty(post.Tag.TagId.ToString()))
                {
                    post.Tag.TagId = 0;
                    cmd.Parameters.AddWithValue("@TagPostId", post.Tag.TagId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TagPostId", post.Tag.TagId);
                }

                cn.Open();

                cmd.ExecuteNonQuery();

            }
       

        }

        public void EditPost(Post post)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EditPost";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@PostId", post.PostId);
                cmd.Parameters.AddWithValue("@Title", post.Title);
                cmd.Parameters.AddWithValue("@Review", post.ReviewText);
                if (string.IsNullOrEmpty(post.PostOn.ToString()) || post.PostOn < (DateTime)SqlDateTime.MinValue)
                {
                    post.PostOn = (DateTime)SqlDateTime.MinValue;
                    cmd.Parameters.AddWithValue("@PostOnDate", post.PostOn);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PostOnDate", post.PostOn);
                }
                

                if (string.IsNullOrEmpty(post.DeleteOn.ToString()) || post.DeleteOn > (DateTime)SqlDateTime.MaxValue)
                {
                    post.DeleteOn = (DateTime)SqlDateTime.MaxValue;
                    cmd.Parameters.AddWithValue("@PostDeleteDate", post.DeleteOn);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PostDeleteDate", post.DeleteOn);
                }
                
                cmd.Parameters.AddWithValue("@RestaurantId", post.Restaurant.RestId);
                cmd.Parameters.AddWithValue("@PostStatus", post.Status);
                cmd.Parameters.AddWithValue("@EmployeePostId", post.Employee.EmployeeId);
                cmd.Parameters.AddWithValue("@TagPostId", post.Tag.TagId);
               

                cn.Open();

                cmd.ExecuteNonQuery();

            }
        }

        public void RemovePost(int id)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DeletePost";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@PostId", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }


        }

        public Post PopulatePostFromDataReader(SqlDataReader dr)
        {
            Post post = new Post();

            post.PostId = (int) dr["PostId"];
            post.ReviewText = dr["Review"].ToString();
            post.Title = dr["Title"].ToString();
            post.PostOn = (DateTime) dr["PostOnDate"];
            post.Restaurant.RestId = (int) dr["RestaurantId"];
            post.Status = (int) dr["PostStatus"];
            post.Employee.EmployeeId = (int) dr["EmployeePostId"];
            post.Tag.TagId = (int) dr["TagPostId"];

            if (dr["PostDeleteDate"] != DBNull.Value)
            {
                post.DeleteOn = (DateTime) dr["PostDeleteDate"];
            }
            post.Restaurant.RestId = (int) dr["RestaurantId"];

            return post;
        }
    }
}
