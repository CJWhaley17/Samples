using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Models.Employees;

namespace Data.Repositories
{
    public class EmployeeRepo : IEmp
    {
        private static List<Employee> _employees;
        public EmployeeRepo()
        {
            _employees = new List<Employee>()
            {
                new Employee()
                {
                    EmployeeId = 1,
                        Name = "Bob Builder",
                        Admin = false,
                        Email = "bob@builder.com"
                },
                new Employee()
                {
                    EmployeeId = 2,
                        Name = "Dora Exploer",
                        Admin = true,
                        Email = "dora@exploer.com"
                }
            };
        }
        public List<Employee> ListEmployees()
        {
            return _employees;
        }

        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(e => e.EmployeeId == id);
        }

        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
        }

        public void EditEmployee(Employee employee)
        {
            //this needs to be better planned in regards to the database
            _employees.RemoveAll(e => e.EmployeeId == employee.EmployeeId);
            _employees.Add(employee);
        }

        public void RemoveEmployee(int id)
        {
            _employees.RemoveAll(e => e.EmployeeId == id);
        }
    }
}
