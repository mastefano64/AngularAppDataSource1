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
    public class CoursesController : Controller
    {
        private AppSettings appsettings;
        private IHostingEnvironment host;
        private ICoursesManager cm;

        public CoursesController(IOptions<AppSettings> options, IHostingEnvironment server, ICoursesManager coursesmanager)
        {
            appsettings = options.Value;
            host = server;
            cm = coursesmanager;
        }

        public IActionResult Paging()
        {
            return View();
        }

        public ResultData<CoursesViewModel> AjaxPaging(string name, string kind, string day, string dateCreated, 
                  string price, string orderbycolumn, string orderbydirection, int pagesize, int page)
        {
            CoursesSearch s = new CoursesSearch();
            s.Name = NgConv.ToString(name);
            s.Kind = NgConv.ToInt(kind);
            s.Day = NgConv.ToInt(day);
            s.DateCreated = NgConv.ToDate(dateCreated);
            s.Price = NgConv.ToDecimal(price);
            s.OrderbyColumn = orderbycolumn;
            s.OrderbyDirection = orderbydirection;
            s.PageSize = pagesize;
            s.Page = page;

            ResultData<CoursesViewModel> list = cm.GetCoursesBySearch(s);

            return list;
        }

        public List<CoursesViewModel> GetAllCourse()
        {
            List<CoursesViewModel> list = cm.GetAllCourse();
            return list;
        }

        public CoursesViewModel GetCourseById(int id)
        {
            CoursesViewModel entity = cm.GetCourseById(id);
            return entity;
        }

        public List<CourseWorkshopsViewModel> GetWorkshopsByCourse(int id)
        {
            List<CourseWorkshopsViewModel> list = cm.GetWorkshopsByCourse(id);
            return list;
        }

        public List<CourseTeachersViewModel> GetTeachersByCourse(int id)
        {
            List<CourseTeachersViewModel> list = cm.GetTeachersByCourse(id);
            return list;
        }      

        public DtoCommandResult InsertCourse([FromBody]CoursesViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();
            if (ModelState.IsValid == true)
            {
                result = cm.InsertCourse(entity);
                return result;
            }
            result = DtoCommandResult.CreateFromModelState(ModelState);
            return result;
        }

        public DtoCommandResult UpdateCourse([FromBody]CoursesViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();
            if (ModelState.IsValid == true)
            {
                result = cm.UpdateCourse(entity);
                return result;
            }
            result = DtoCommandResult.CreateFromModelState(ModelState);
            return result;
        }

        public DtoCommandResult DeleteCourse(int id)
        {
            DtoCommandResult result = new DtoCommandResult();
            result = cm.DeleteCourse(id);
            return result;
        }
    }
}
