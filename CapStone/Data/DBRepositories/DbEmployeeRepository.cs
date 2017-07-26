using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Models.Employees;


namespace Data.Repositories.DBRepositories
{
    public class DbEmployeeRepository : IEmp
    {
        List<Employee> _employees = new List<Employee>();

        public List<Employee> ListEmployees()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Employee";

                cmd.Connection = cn;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        _employees.Add(PopulateEmployeeFromDataReader(dr));
                    }
                }
            }

            return _employees;
        }

        public Employee GetEmployee(int id)
        {
            Employee employee = new Employee();

            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "GetEmployeeById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@EmployeeId", id);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        employee = PopulateEmployeeFromDataReader(dr);

                    }
                }
                return employee;
            }
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "AddEmployee";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                if (string.IsNullOrEmpty(employee.Name))
                {
                    employee.Name = "Not Assigned";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@EmployeeName", employee.Name);
                }
                if (string.IsNullOrEmpty(employee.Email))
                {
                    employee.Email = "example@email.com";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@EmployeeEmail", employee.Email);
                }

                cn.Open();

                cmd.ExecuteNonQuery();

            }
        }

        public void EditEmployee(Employee employee)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EditEmployee";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.Name);
                cmd.Parameters.AddWithValue("@EmployeeEmail", employee.Email);

                cn.Open();

                cmd.ExecuteNonQuery();

            }
        }

        public void RemoveEmployee(int id)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DeleteEmployee";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@EmployeeId", id);


                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public Employee PopulateEmployeeFromDataReader(SqlDataReader dr)
        {
            Employee employee = new Employee();

            employee.EmployeeId = (int)dr["EmployeeId"];
            employee.Name = dr["EmployeeName"].ToString();
            employee.Email = dr["EmployeeEmail"].ToString();

            return employee;
        }
    }
}
