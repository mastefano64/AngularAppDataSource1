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
    public class CoursesMockManager : ICoursesManager
    {
        private AppSettings appsettings;

        public CoursesMockManager(IOptions<AppSettings> options)
        {
            appsettings = options.Value;
        }

        public ResultData<CoursesViewModel> GetCoursesBySearch(CoursesSearch s)
        {
            ResultData<CoursesViewModel> result = new ResultData<CoursesViewModel>();

            List<CoursesViewModel> list = CreateList();

            if (String.IsNullOrEmpty(s.Name) == false)
            {
                list = list.Where(q => q.Name == s.Name).ToList();
            }
            if (s.Kind != 0)
            {
                list = list.Where(q => q.Kind == s.Kind).ToList();
            }
            if (s.Day != 0)
            {
                list = list.Where(q => q.Day == s.Day).ToList();
            }
            if (s.DateCreated.HasValue == true)
            {
                list = list.Where(q => q.DateCreated == s.DateCreated).ToList();
            }
            if (s.Price != 0)
            {
                list = list.Where(q => q.Price == s.Price).ToList();
            }

            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "coursesid")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.CourseId).ToList();
                else list = list.OrderByDescending(q => q.CourseId).ToList();
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "name")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.Name).ToList();
                else list = list.OrderByDescending(q => q.Name).ToList();
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "kind")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.Kind).ToList();
                else list = list.OrderByDescending(q => q.Kind).ToList();
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "day")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.Day).ToList();
                else list = list.OrderByDescending(q => q.Day).ToList();
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "datecreated")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.DateCreated).ToList();
                else list = list.OrderByDescending(q => q.DateCreated).ToList();
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "price")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.Price).ToList();
                else list = list.OrderByDescending(q => q.Price).ToList();
            }

            result.Count = list.Count;
            int index = s.Page * s.PageSize;
            result.Items = list.Skip(index).Take(s.PageSize).ToList();

            return result;
        }

        public List<CoursesViewModel> GetAllCourse()
        {
            List<CoursesViewModel> list = CreateList();

            return list;
        }

        public CoursesViewModel GetCourseById(int id)
        {
            CoursesViewModel entity = new CoursesViewModel();

            List<CoursesViewModel> list = CreateList();
            entity = list.Where(w => w.CourseId == id).Single();

            return entity;
        }

        public List<CourseTeachersViewModel> GetTeachersByCourse(int id)
        {
            List<CourseTeachersViewModel> list = new List<CourseTeachersViewModel>();

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
                list.Add(new CourseTeachersViewModel()
                {
                    CourseId = id,
                    TeacherId = k,
                    Name = $"teachers name{k}",
                    Surname = $"surname{k}",
                    Address = $"address{k}",
                    Cap = cap,
                    City = $"city{k}",
                    Province = prov
                });
            }

            return list;
        }

        public List<CourseWorkshopsViewModel> GetWorkshopsByCourse(int id)
        {
            List<CourseWorkshopsViewModel> list = new List<CourseWorkshopsViewModel>();

            for (int i = 1; i < 10; i++)
            {
                int k = i + id;
                DateTime start = DateTime.MinValue;
                DateTime end = DateTime.MinValue;
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
                list.Add(new CourseWorkshopsViewModel()
                {
                    CourseId = id,
                    WorkshopId = k,
                    Name = $"workshops name{i}",
                    DateIn = start,
                    DateFi = end,
                });
            }

            return list;
        }

        public DtoCommandResult InsertCourse(CoursesViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();

            return result;
        }

        public DtoCommandResult UpdateCourse(CoursesViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();

            return result;
        }

        public DtoCommandResult DeleteCourse(int id)
        {
            DtoCommandResult result = new DtoCommandResult();

            return result;
        }

        private List<CoursesViewModel> CreateList()
        {
            List<CoursesViewModel> list = new List<CoursesViewModel>();

            for (int i = 1; i < 300; i++)
            {
                int kind = 1;
                int day = 0;
                decimal price = 0;
                DateTime datecreated = DateTime.MinValue;
                if (i > 100)
                    kind = 2;
                if (i > 200)
                    kind = 3;
                if (i % 2 != 0)
                    day = 2;
                else day = 3;
                if (i % 2 != 0)
                    datecreated = Convert.ToDateTime("2018/02/03");
                else datecreated = Convert.ToDateTime("2018/03/04");
                if (i % 2 != 0)
                    price = 112.50m;
                else price = 114.50m;
                list.Add(new CoursesViewModel()
                {
                    CourseId = i,
                    Name = $"courses name{i}",
                    Kind = kind,
                    Day = day,
                    DateCreated = datecreated,
                    Price = price,
                });
            }

            return list;
        }
    }
}
