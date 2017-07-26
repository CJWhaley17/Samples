using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
using Models.Restaurants;

namespace Data.Repositories
{
    public class RestaurantRepo : IRepo
    {
        private static List<Restaurant> _list;

        public RestaurantRepo()
        {
            _list = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "Mr. Zub's Deli",
                    Address = "810 W Market St",
                    PhoneNumber = "1111111111",
                    Rating = 4.4m,
                    RestId = 1
                },
                new Restaurant()
                {
                    Name = "Nuevo",
                    Address = "10 Mill St",
                    PhoneNumber = "2222222222",
                    Rating = 5.1m,
                    RestId = 2
                }
            };
        }
        public List<Restaurant> ListRestaurants()
        {
            return _list;
        }

        public Restaurant GetRestaurant(int id)
        {
            return _list.FirstOrDefault(r => r.RestId == id);
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            _list.Add(restaurant);
        }

        public void EditRestaurant(Restaurant restaurant)
        {
            //This needs to be thought out better I just kinda threw it together
            _list.RemoveAll(r => r.RestId == restaurant.RestId);
            //because I do not think this is going to place nice with the DB
            _list.Add(restaurant);
        }

        public void RemoveRestaurant(int id)
        {
            _list.RemoveAll(r => r.RestId == id);
        }
    }
}
