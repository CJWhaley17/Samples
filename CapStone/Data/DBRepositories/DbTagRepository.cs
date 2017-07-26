using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Models;

namespace Data.Repositories.DBRepositories
{
    public class DbTagRepository : ITag
    {
        List<Tag> tags = new List<Tag>();

        public List<Tag> ListTags()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Tag";

                cmd.Connection = cn;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        tags.Add(PopulateTagFromDataReader(dr));
                    }
                }
            }

            return tags;
        }

        public Tag GetTag(int id)
        {
            Tag tag = new Tag();

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "GetTagById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@TagId", id);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        tag = PopulateTagFromDataReader(dr);
                    }
                }
            }

            return tag;
        }

        public void AddTag(Tag tag)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AddTag";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@TagTitle", tag.TagTitle);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveTag(int id)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DeleteTag";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@TagId", id);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void EditTag(Tag tag)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "EditTag",
                    CommandType = CommandType.StoredProcedure,
                    Connection = cn
                };

                cmd.Parameters.AddWithValue("@TagId", tag.TagId);
                cmd.Parameters.AddWithValue("@Title", tag.TagTitle);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Tag PopulateTagFromDataReader(SqlDataReader dr)
        {
            Tag tag = new Tag();

            tag.TagId = (int) dr["TagId"];
            tag.TagTitle = dr["Title"].ToString();

            return tag;
        }
    }
}
