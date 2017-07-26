using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Factories;
using Data.Interfaces;
using Data.Repositories.DBRepositories;
using Models;
using Models.ErrorHandling;

namespace BLL.Manageras
{
    public class TagManager
    {
        private ITag _tag;

        public TagManager()
        {
            _tag = RepoFactory.GetTagRepo();
        }

        public Response<List<Tag>> ListTags()
        {
            var response = new Response<List<Tag>>();
            try
            {
                response.Data = _tag.ListTags();
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

        public Response<Tag> GetTag(int id)
        {
            var response = new Response<Tag>();
            try
            {
                response.Data = _tag.GetTag(id);
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

        public void AddTag(Tag tag)
        {
            var response = new Response<Tag>();
            try
            {
                _tag.AddTag(tag);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error has occured";
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
        }

        public void RemoveTag(int id)
        {
            var response = new Response<Tag>();
            try
            {
                _tag.RemoveTag(id);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error has occured";
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
        }

        public void EditTag(Tag tag)
        {
            var response = new Response<Tag>();
            try
            {
                _tag.EditTag(tag);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error has occured";
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
        }
    }
}
