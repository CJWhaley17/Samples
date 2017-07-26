using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Factories;
using Data.Interfaces;
using Models.Employees;
using Models.ErrorHandling;
using Models.Postings;
using Models.Restaurants;

namespace BLL.Manageras
{
    public class RestaurantManager
    {
        private static IRepo _repo;

        public RestaurantManager()
        {
            _repo = RepoFactory.GetRestaurantRepo();
        }

        public Response<List<Restaurant>> ListRestaurants()
        {
            var response = new Response<List<Restaurant>>();
            try
            {
                response.Data = _repo.ListRestaurants();
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
        
        public void AddRestaurant(Restaurant restaurant)
        {
            try
            {
                _repo.AddRestaurant(restaurant);
            }
            catch (Exception ex)
            {
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
        }
        
        public void EditRestaurant(Restaurant restaurant)
        {
            try
            {
                _repo.EditRestaurant(restaurant);
            }
            catch (Exception ex)
            {
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
        }
        
        public void RemoveRestaurant(int id)
        {
            var response = new Response<Restaurant>();
            try
            {
                response.Success = true;
                _repo.RemoveRestaurant(id);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error has occured";
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
        }
        
        public Response<Restaurant> GetRestaurant(int id)
        {
            var response = new Response<Restaurant>();
            try
            {
                response.Data = _repo.GetRestaurant(id);
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
