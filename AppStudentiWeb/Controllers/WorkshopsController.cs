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
    public class WorkshopsController : Controller
    {
        private AppSettings appsettings;
        private IHostingEnvironment host;
        private IWorkshopsManager wm;

        public WorkshopsController(IOptions<AppSettings> options, IHostingEnvironment server, IWorkshopsManager workshopsmanager)
        {
            appsettings = options.Value;
            host = server;
            wm = workshopsmanager;
        }

        public IActionResult Paging()
        {
            return View();
        }

        public ResultData<WorkshopsViewModel> AjaxPaging(string name, string dateIn, string dateFi, string courseId, 
                   string teacherId, string orderbycolumn, string orderbydirection, int pagesize, int page)
        {
            WorkshopsSearch s = new WorkshopsSearch();
            s.Name = NgConv.ToString(name);
            s.DateIn = NgConv.ToDate(dateIn);
            s.DateFi = NgConv.ToDate(dateFi);
            s.CourseId = NgConv.ToInt(courseId);
            s.TeacherId = NgConv.ToInt(teacherId);
            s.OrderbyColumn = orderbycolumn;
            s.OrderbyDirection = orderbydirection;
            s.PageSize = pagesize;
            s.Page = page;

            ResultData<WorkshopsViewModel> list = wm.GetWorkshopsBySearch(s);

            return list;
        }

        public WorkshopsViewModel GetWorkshopById(int id)
        {
            WorkshopsViewModel entity = wm.GetWorkshopById(id);
            return entity;
        }

        public DtoCommandResult InsertWorkshop([FromBody]WorkshopsViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();
            if (ModelState.IsValid == true)
            {
                result = wm.InsertWorkshop(entity);
                return result;
            }
            result = DtoCommandResult.CreateFromModelState(ModelState);
            return result;
        }

        public DtoCommandResult UpdateWorkshop([FromBody]WorkshopsViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();
            if (ModelState.IsValid == true)
            {
                result = wm.UpdateWorkshop(entity);
                return result;
            }
            result = DtoCommandResult.CreateFromModelState(ModelState);
            return result;
        }

        public DtoCommandResult DeleteWorkshop(int id)
        {
            DtoCommandResult result = new DtoCommandResult();
            result = wm.DeleteWorkshop(id);
            return result;
        }

        public List<WorkshopStudentsViewModel> GetStudentsByWorkshop(int id)
        {
            List<WorkshopStudentsViewModel> list = wm.GetStudentsByWorkshop(id);
            return list;
        }

        public DtoCommandResult InsertStudentsWorkshop(int workshopId, int studentId)
        {
            DtoCommandResult result = new DtoCommandResult();
            result = wm.InsertStudentsWorkshop(workshopId, studentId);
            return result;
        }

        public DtoCommandResult DeleteStudentsWorkshop(int workshopId, int studentId)
        {
            DtoCommandResult result = new DtoCommandResult();
            result = wm.DeleteStudentsWorkshop(workshopId, studentId);
            return result;
        }
    }
}
