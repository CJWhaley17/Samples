using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Factories;
using Data.Interfaces;
using Models.ErrorHandling;
using Models.Postings;
using Models.Restaurants;

namespace BLL.Manageras
{
    public class PostingManager
    {
        private static IPost _post;
        RestaurantManager _restaurant = new RestaurantManager();
        EmployeeManager _employee = new EmployeeManager();
        TagManager _tag = new TagManager();

        public PostingManager()
        {
            _post = RepoFactory.GetPostingRepo();
        }
        public Response<List<Post>> ListPostings()
        {
            var response = new Response<List<Post>>();
            try
            {
                var listofPosts = _post.ListPostings();
                foreach (var post in listofPosts)
                {
                    post.Restaurant = _restaurant.GetRestaurant(post.Restaurant.RestId).Data;
                    post.Employee = _employee.GetEmployee(post.Employee.EmployeeId).Data;
                    post.Tag = _tag.GetTag(post.Tag.TagId).Data;
                }
                response.Data = listofPosts;
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

        public void AddPost(Post post)
        {
            try
            {
                if (string.IsNullOrEmpty(post.Status.ToString()))
                {
                    post.Status = 0;
                }
                if (post.Employee.Admin == true)
                {
                    post.Status = 2;
                }
                _post.AddPost(post);
            }
            catch (Exception ex)
            {
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
        }

        public void EditPost(Post post)
        {
            try
            {
               _post.EditPost(post);
            }
            catch (Exception ex)
            {
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }

        }

        public void RemovePost(int id)
        {
            try
            {
                _post.RemovePost(id);
            }
            catch (Exception ex)
            {
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
            
        }

        public Response<Post> GetPost(int id)
        {
            var response = new Response<Post>();
            try
            {
                response.Data = _post.GetPost(id);
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
