using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using AppStudentiCommon;
using AppStudentiCommon.Model;
using AppStudentiCommon.Interface;
using AppStudentiCommon.Model.ViewModel;
using AppStudentiCommon.Model.Search;

namespace AppStudentiBusiness.Mock
{
    public class WorkshopsMockManager : IWorkshopsManager
    {
        private AppSettings appsettings;

        public WorkshopsMockManager(IOptions<AppSettings> options)
        {
            appsettings = options.Value;
        }

        public ResultData<WorkshopsViewModel> GetWorkshopsBySearch(WorkshopsSearch s)
        {
            ResultData<WorkshopsViewModel> result = new ResultData<WorkshopsViewModel>();

            List<WorkshopsViewModel> list = CreateList1();

            if (String.IsNullOrEmpty(s.Name) == false)
            {
                list = list.Where(q => q.Name == s.Name).ToList();
            }
            if (s.DateIn.HasValue == true)
            {
                list = list.Where(q => q.DateIn >= s.DateIn).ToList();
            }
            if (s.DateFi.HasValue == true)
            {
                list = list.Where(q => q.DateFi <= s.DateFi).ToList();
            }
            if (s.CourseId != 0)
            {
                list = list.Where(q => q.CourseId == s.CourseId).ToList();
            }
            if (s.TeacherId != 0)
            {
                list = list.Where(q => q.TeacherId == s.TeacherId).ToList();
            }

            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "Workshopsid")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.WorkshopId).ToList();
                else list = list.OrderByDescending(q => q.WorkshopId).ToList();
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "name")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.Name).ToList();
                else list = list.OrderByDescending(q => q.Name).ToList();
            }

            result.Count = list.Count;
            int index = s.Page * s.PageSize;
            result.Items = list.Skip(index).Take(s.PageSize).ToList();

            return result;
        }

        public WorkshopsViewModel GetWorkshopById(int id)
        {
            WorkshopsViewModel entity = new WorkshopsViewModel();

            List<WorkshopsViewModel> list = CreateList1();
            entity = list.Where(w => w.WorkshopId == id).Single();

            return entity;
        }

        public List<WorkshopStudentsViewModel> GetStudentsByWorkshop(int id)
        {
            List<WorkshopStudentsViewModel> list = CreateList2(id);

            return list;
        }

        public DtoCommandResult InsertWorkshop(WorkshopsViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();

            return result;
        }

        public DtoCommandResult UpdateWorkshop(WorkshopsViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();

            return result;
        }

        public DtoCommandResult DeleteWorkshop(int id)
        {
            DtoCommandResult result = new DtoCommandResult();

            return result;
        }

        public DtoCommandResult InsertStudentsWorkshop(int workshopId, int studentId)
        {
            DtoCommandResult result = new DtoCommandResult();

            result.Result = GetStudentsByWorkshop(workshopId);

            return result;
        }

        public DtoCommandResult DeleteStudentsWorkshop(int workshopId, int studentId)
        {
            DtoCommandResult result = new DtoCommandResult();

            result.Result = GetStudentsByWorkshop(workshopId);

            return result;
        }

        private List<WorkshopsViewModel> CreateList1()
        {
            List<WorkshopsViewModel> list = new List<WorkshopsViewModel>();

            for (int i = 1; i < 100; i++)
            {
                int teacherId = 0;
                int courseId = 0;
                DateTime start = DateTime.MinValue;
                DateTime end = DateTime.MinValue;
                if (i % 2 != 0)
                {
                    courseId = 2;
                    teacherId = 1;
                }
                else
                {
                    courseId = 1;
                    teacherId = 2;
                }
                if (i % 2 != 0)
                {
                    start = Convert.ToDateTime("2018/02/03");
                    end = Convert.ToDateTime("2018/03/05");
                }
                else
                {
                    start = Convert.ToDateTime("2018/04/13");
                    end = Convert.ToDateTime("2018/05/15");
                }
                list.Add(new WorkshopsViewModel()
                {
                    WorkshopId = i,
                    Name = $"workshops name{i}",
                    DateIn = start,
                    DateFi = end,
                    CourseId = courseId,
                    TeacherId = teacherId
                });
            }

            return list;
        }

        private List<WorkshopStudentsViewModel> CreateList2(int id)
        {
            List<WorkshopStudentsViewModel> list = new List<WorkshopStudentsViewModel>();

            for (int i = 1; i < 10; i++)
            {
                int k = i + id;
                string cap = "";
                string prov = "ge";
                if (i > 3)
                    prov = "mi";
                if (i > 6)
                    prov = "to";
                if (i % 2 != 0)
                    cap = "10100";
                else cap = "10200";
                list.Add(new WorkshopStudentsViewModel()
                {
                    WorkshopId = id,
                    StudentId = k,
                    Name = $"students name{k}",
                    Surname = $"surname{k}",
                    Address = $"address{k}",
                    Cap = cap,
                    City = $"city{k}",
                    Province = prov
                });
            }

            return list;
        }
    }
}
