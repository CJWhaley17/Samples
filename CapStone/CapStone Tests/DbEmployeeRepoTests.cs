using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Manageras;
using Data.Repositories.DBRepositories;
using Models.Employees;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DbEmployeeRepoTests
    {
        EmployeeManager _manager = new EmployeeManager();
        [Test]
        public void CanAddAnEmployeeToDb()
        {
            Employee newEmployee = new Employee()
            {
                Name = "James",
                Email = "James@swg.com"
            };
            _manager.AddEmployee(newEmployee);
            Assert.AreEqual(3, _manager.ListEmployees().Data.Count);
        }
        [Test]
        public void CanRemoveAnEmployeeFromDb()
        {
            _manager.RemoveEmployee(1);
        }
        [Test]
        public void CanUpdateAnEmployeeInTheDb()
        {
            var emp = _manager.GetEmployee(3);
            emp.Data.Name = "Dave v3";
            emp.Data.Email = "asd@email.com";
            _manager.EditEmployee(emp.Data);

            Assert.AreEqual("Dave v3", _manager.GetEmployee(3).Data.Name);
            Assert.AreEqual("asd@email.com", emp.Data.Email);
        }
        [Test]
        public void CanListAllOfTheEmployeesInDb()
        {
            Assert.AreEqual(1, _manager.ListEmployees().Data.Count);
        }
        [Test]
        public void CanGetSingleEmployeeFromDb()
        {
            var getOne = _manager.GetEmployee(2);
            Assert.AreEqual("Dave", getOne.Data.Name);
        }
    }
}
