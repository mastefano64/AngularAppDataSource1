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
    public class StudentsController : Controller
    {
        private AppSettings appsettings;
        private IHostingEnvironment host;
        private IStudentsManager sm;

        public StudentsController(IOptions<AppSettings> options, IHostingEnvironment server, IStudentsManager studentsmanager)
        {
            appsettings = options.Value;
            host = server;
            sm = studentsmanager;
        }

        public IActionResult Paging()
        {
            return View();
        }

        public ResultData<StudentsViewModel> AjaxPaging(string name, string surname, string address, string cap, string city, 
                      string province, string orderbycolumn, string orderbydirection, int pagesize, int page)
        {
            StudentsSearch s = new StudentsSearch();
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

            ResultData<StudentsViewModel> list = sm.GetStudentsBySearch(s);

            return list;
        }

        public List<StudentsViewModel> GetAllStudent()
        {
            List<StudentsViewModel> list = sm.GetAllStudent();
            return list;
        }

        public StudentsViewModel GetStudentById(int id)
        {
            StudentsViewModel entity = sm.GetStudentById(id);
            return entity;
        }

        public List<StudentsViewModel> GetStudentByName(string name)
        {
            List<StudentsViewModel> list = sm.GetStudentByName(name);
            return list;
        }

        public DtoCommandResult InsertStudent([FromBody]StudentsViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();
            if (ModelState.IsValid == true)
            {
                result = sm.InsertStudent(entity);
                return result;
            }
            result = DtoCommandResult.CreateFromModelState(ModelState);
            return result;
        }

        public DtoCommandResult UpdateStudent([FromBody]StudentsViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();
            if (ModelState.IsValid == true)
            {
                result = sm.UpdateStudent(entity);
                return result;
            }
            result = DtoCommandResult.CreateFromModelState(ModelState);
            return result;
        }

        public DtoCommandResult DeleteStudent(int id)
        {
            DtoCommandResult result = new DtoCommandResult();
            result = sm.DeleteStudent(id);
            return result;
        }
    }
}
