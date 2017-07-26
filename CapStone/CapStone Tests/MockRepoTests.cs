using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Manageras;
using Data.Repositories;
using Models.Employees;
using Models.Postings;
using Models.Restaurants;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MockRepoTests
    {
        RestaurantManager _restaurantManager = new RestaurantManager(); //Restaurant RestaurantManager
        EmployeeManager _employee = new EmployeeManager();
        PostingManager _post = new PostingManager();

        [Test]
        public void CanRemovePost()
        {
            _post.RemovePost(1);
            Assert.AreEqual(2, _post.ListPostings().Data.Count);
        }
        [Test]
        public void CanRemoveRest()
        {
            _restaurantManager.RemoveRestaurant(1);
            Assert.AreEqual(2, _restaurantManager.ListRestaurants().Data.Count);
        }
        [Test]
        public void CanRemoveEmp()
        {
            _employee.RemoveEmployee(1);
            Assert.AreEqual(2, _employee.ListEmployees().Data.Count);
        }
        [Test]
        public void CanEditRestaurant()
        {
            var rest = new Restaurant();
            rest = _restaurantManager.GetRestaurant(1).Data;
            rest.Name = "New Rest";
            _restaurantManager.EditRestaurant(rest);
            Assert.AreEqual("New Rest", _restaurantManager.GetRestaurant(1).Data.Name);
        }
        [Test]
        public void CanEditPost()
        {
            var pest = new Post();
            pest = _post.GetPost(1).Data;
            pest.Title = "New Title";
            _post.EditPost(pest);
            Assert.AreEqual("New Title", _post.GetPost(1).Data.Title);
        }
        [Test]
        public void CanEditEmployee()
        {
            var emp = new Employee();
            emp = _employee.GetEmployee(1).Data;
            emp.Name = "New Name";
            _employee.EditEmployee(emp);
            Assert.AreEqual("New Name", _employee.GetEmployee(1).Data.Name);
        }
        [Test]
        public void CanGetListOfPosts()
        {
            Assert.AreEqual(3, _post.ListPostings().Data.Count);
        }
        [Test]
        public void CanGetListOfEmployees()
        {
            Assert.AreEqual(3, _employee.ListEmployees().Data.Count);
        }
        [Test]
        public void CanGetListOfRestaurants()
        {
            Assert.AreEqual(3, _restaurantManager.ListRestaurants().Data.Count);
        }

        [Test]
        public void CanGetSinglePost()
        {
            Assert.AreEqual("1111111111", _post.GetPost(1).Data.Restaurant.PhoneNumber);
        }
        [Test]
        public void CanGetSingleEmployee()
        {
            Assert.AreEqual("bob@builder.com", _employee.GetEmployee(1).Data.Email);
        }
        [Test]
        public void CanGetSingleRestaurant()
        {
            Assert.AreEqual("Nuevo", _restaurantManager.GetRestaurant(2).Data.Name);
        }

        [Test]
        public void CanAddPost()
        {
            var newPost = new Post()
            {
                Title = "lwr"
            };
            _post.AddPost(newPost);
            Assert.AreEqual(3, _post.ListPostings().Data.Count);
        }
        [Test]
        public void CanAddEmployee()
        {
            var newEmp = new Employee()
            {
                Name = "lwr"
            };
            _employee.AddEmployee(newEmp);
            Assert.AreEqual(3, _employee.ListEmployees().Data.Count);
        }
        [Test]
        public void CanAddRestaurant()
        {
            var newRes = new Restaurant()
            {
                Name = "ksabjdfsdflskdfj",
                Address = "Address",
                PhoneNumber = "111",
                Rating = 4.4M
            };
            _restaurantManager.AddRestaurant(newRes);
            Assert.AreEqual(5, _restaurantManager.ListRestaurants().Data.Count);
        }
    }
}
