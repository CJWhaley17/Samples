using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Factories;
using Data.Interfaces;
using Models.ErrorHandling;
using Models.StaticPages;

namespace BLL.Manageras
{
    public class StaticPageManager
    {
        private static IPage _page;

        public StaticPageManager()
        {
            _page = RepoFactory.GetStaticPageRepo();
        }

        public Response<List<StaticPage>> GetAll()
        {
            var response = new Response<List<StaticPage>>();
            try
            {
                var listofPages = _page.GetAll();
                response.Data = listofPages;
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

        public void AddPage(StaticPage page)
        {
            try
            {
                if (string.IsNullOrEmpty(page.PageStatus.ToString()))
                {
                    page.PageStatus = 0;
                }
                if (page.Employee.Admin == true)
                {
                    page.PageStatus = 2;
                }
                _page.AddPage(page);
            }
            catch (Exception ex)
            {
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }
        }

        public void EditPage(StaticPage page)
        {
            try
            {
                _page.EditPage(page);
            }
            catch (Exception ex)
            {
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }

        }

        public void RemovePage(int id)
        {
            try
            {
                _page.RemovePage(id);
            }
            catch (Exception ex)
            {
                ErrorLog log = new ErrorLog();
                log.Log(ex.ToString());
            }

        }

        public Response<StaticPage> GetPage(int id)
        {
            var response = new Response<StaticPage>();
            try
            {
                response.Data = _page.GetPage(id);
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
