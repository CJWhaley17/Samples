using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BLL.Manageras;
using Data.Repositories.DBRepositories;
using Models.Postings;
using Models.Restaurants;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DbRepositoriesTests
    {
        // MAKE SURE TO CHANGE THE APP CONFIG FROM \aql2014
        //AND MAKE IT THIS: \SQL2014
        PostingManager _post = new PostingManager();
        RestaurantManager _rest = new RestaurantManager();
        [Test]
        public void CanListAllFromDbRestaurantRepo()
        {
            Assert.AreEqual(4, _rest.ListRestaurants().Data.Count);
        }

        [Test]
        public void CanListOneRestaurantFromDbById()
        {
            var restaurant = _rest.GetRestaurant(3).Data;
            Assert.AreEqual("New Rest", restaurant.Name);

        }

        [Test]
        public void CanAddNewRestaurantToDb()
        {
            var newRestaurant = _rest.GetRestaurant(8).Data;
            newRestaurant.Name = "Eddies";
            newRestaurant.Rating = 11.1m;
            _rest.AddRestaurant(newRestaurant);
            Assert.AreEqual(19, _rest.ListRestaurants().Data.Count);
        }

        [Test]
        public void CanDeleteRestaurantFromDbById()
        {
           _rest.RemoveRestaurant(10);
            Assert.AreEqual(3, _rest.ListRestaurants().Data.Count);
        }

        [Test]
        public void CanEditRestaurantRecordInDb()
        {
            var repo = new DbRestaurantRepositories();
            var manager = new RestaurantManager();

            var rest = manager.GetRestaurant(3).Data;
            rest.Name = "New Rest";
            manager.EditRestaurant(rest);
            Assert.AreEqual("New Rest", manager.GetRestaurant(3).Data.Name);
        }
    }
}
