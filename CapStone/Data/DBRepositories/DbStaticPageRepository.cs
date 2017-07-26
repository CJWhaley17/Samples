using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Models.StaticPages;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Data.Repositories.DBRepositories
{
    public class DbStaticPageRepository : IPage
    {
        public List<StaticPage> GetAll()
        {
            List<StaticPage> pages = new List<StaticPage>();

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Pages ";
                cmd.Connection = cn;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        pages.Add(PopulatePageFromDataReader(dr));
                    }
                }
            }

            return pages;
        }

        public StaticPage GetPage(int id)
        {
            StaticPage page = new StaticPage();

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GetPageById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@PageId", id);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        page = PopulatePageFromDataReader(dr);
                    }
                }

            }

            return page;

        }

        public void AddPage(StaticPage page)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AddPage";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                if (string.IsNullOrEmpty(page.PageTitle))
                {
                    page.PageTitle = "Not Assigned";
                    cmd.Parameters.AddWithValue("@PageTitle", page.PageTitle);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PageTitle", page.PageTitle);
                }
                if (string.IsNullOrEmpty(page.PageContent))
                {
                    page.PageContent = "Sample Content";
                    cmd.Parameters.AddWithValue("@PageContent", page.PageContent);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PageContent", page.PageContent);
                }
                if (string.IsNullOrEmpty(page.PageStatus.ToString()))
                {
                    page.PageStatus = 0;
                    cmd.Parameters.AddWithValue("@PageStatus", page.PageStatus);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PageStatus", page.PageStatus);
                }
                cmd.Parameters.AddWithValue("@EmployeeId", page.Employee.EmployeeId);

                cn.Open();

                cmd.ExecuteNonQuery();

            }


        }

        public void EditPage(StaticPage page)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EditPage";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@PageId", page.PageID);
                cmd.Parameters.AddWithValue("@PageTitle", page.PageTitle);
                cmd.Parameters.AddWithValue("@PageContent", page.PageContent);
                cmd.Parameters.AddWithValue("@PageStatus", page.PageStatus);
                cmd.Parameters.AddWithValue("@EmployeeId", page.Employee.EmployeeId);

                cn.Open();

                cmd.ExecuteNonQuery();

            }
        }

        public void RemovePage(int id)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DeletePage";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@PageId", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }


        }

        public StaticPage PopulatePageFromDataReader(SqlDataReader dr)
        {
            StaticPage page = new StaticPage();

            page.PageID = (int)dr["PageId"];
            page.PageTitle = dr["PageTitle"].ToString();
            page.PageContent = dr["PageContent"].ToString();
            page.Employee.EmployeeId = (int)dr["EmployeeId"];
            page.PageStatus = (int)dr["PageStatus"];

            return page;
        }

        
    }

}
