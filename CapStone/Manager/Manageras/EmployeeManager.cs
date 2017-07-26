using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Factories;
using Data.Interfaces;
using Models.Employees;
using Models.ErrorHandling;

namespace BLL.Manageras
{
    public class EmployeeManager
    {
        private IEmp _emp;

        public EmployeeManager()
        {
            _emp = RepoFactory.GetEmployeeRepo();
        }

        public Response<List<Employee>> ListEmployees()
        {
            var response = new Response<List<Employee>>();
            try
            {
                response.Data = _emp.ListEmployees();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error has occured";
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
            return response;
        }
        
        public void AddEmployee(Employee employee)
        {
            var response = new Response<Employee>();
            try
            {
                _emp.AddEmployee(employee);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error has occured";
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
        }

        public void EditEmployee(Employee employee)
        {
            var response = new Response<Employee>();
            try
            {
                _emp.EditEmployee(employee);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error has occured";
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
        }

        public void RemoveEmployee(int id)
        {
            var response = new Response<Employee>();
            try
            {
                _emp.RemoveEmployee(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error has occured";
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
        }

        public Response<Employee> GetEmployee(int id)
        {
            var response = new Response<Employee>();
            try
            {
                response.Data = _emp.GetEmployee(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error has occured";
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
            return response;
        }
    }
}
