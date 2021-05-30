using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using AppStudentiCommon;
using AppStudentiCommon.Model;
using AppStudentiCommon.Interface;
using AppStudentiWeb.Helper;
using AppStudentiCommon.Model.ViewModel;
using AppStudentiCommon.Model.Search;

namespace AppStudentiWeb.Controllers
{
    public class TeachersController : Controller
    {
        private AppSettings appsettings;
        private IHostingEnvironment host;
        private ITeachersManager tm;

        public TeachersController(IOptions<AppSettings> options, IHostingEnvironment server, ITeachersManager teachersmanager)
        {
            appsettings = options.Value;
            host = server;
            tm = teachersmanager;
        }

        public IActionResult Paging()
        {
            return View();
        }

        public ResultData<TeachersViewModel> AjaxPaging(string name, string surname, string address, string cap, string city, 
                      string province, string orderbycolumn, string orderbydirection, int pagesize, int page)
        {
            TeachersSearch s = new TeachersSearch();
            s.Name = NgConv.ToString(name);
            s.Surname = NgConv.ToString(surname);
            s.Address = NgConv.ToString(address);
            s.Cap = NgConv.ToString(cap);
            s.City = NgConv.ToString(city);
            s.Province = NgConv.ToString(province);
            s.OrderbyColumn = orderbycolumn;
            s.OrderbyDirection = orderbydirection;
            s.PageSize = pagesize;
            s.Page = page;

            ResultData<TeachersViewModel> list = tm.GetTeachersBySearch(s);

            return list;
        }

        public List<TeachersViewModel> GetAllTeacher()
        {
            List<TeachersViewModel> list = tm.GetAllTeacher();
            return list;
        }

        public TeachersViewModel GetTeacherById(int id)
        {
            TeachersViewModel entity = tm.GetTeacherById(id);
            return entity;
        }

        public List<TeachersViewModel> GetTeacherByName(string name)
        {
            List<TeachersViewModel> list = tm.GetTeacherByName(name);
            return list;
        }

        public DtoCommandResult InsertTeacher([FromBody]TeachersViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();
            if (ModelState.IsValid == true)
            {
                result = tm.InsertTeacher(entity);
                return result;
            }
            result = DtoCommandResult.CreateFromModelState(ModelState);
            return result;
        }

        public DtoCommandResult UpdateTeacher([FromBody]TeachersViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();
            if (ModelState.IsValid == true)
            {
                result = tm.UpdateTeacher(entity);
                return result;
            }
            result = DtoCommandResult.CreateFromModelState(ModelState);
            return result;
        }

        public DtoCommandResult DeleteTeacher(int id)
        {
            DtoCommandResult result = new DtoCommandResult();
            result = tm.DeleteTeacher(id);
            return result;
        }
    }
}
