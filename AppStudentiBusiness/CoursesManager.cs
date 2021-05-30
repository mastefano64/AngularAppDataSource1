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
using AppStudentiData;
using Microsoft.EntityFrameworkCore;

namespace AppStudentiBusiness
{
    public class CoursesManager : ICoursesManager
    {
        private AppSettings appsettings;
        private appstudentiContext db;

        private string msg = "Corso non annullabile, esistono relazioni!";

        public CoursesManager(IOptions<AppSettings> options, appstudentiContext context)
        {
            appsettings = options.Value;
            db = context;
        }

        public ResultData<CoursesViewModel> GetCoursesBySearch(CoursesSearch s)
        {
            ResultData<CoursesViewModel> result = new ResultData<CoursesViewModel>();

            IQueryable<CoursesViewModel> query = CreateQuery();

            if (String.IsNullOrEmpty(s.Name) == false)
            {
                query = query.Where(q => q.Name.Contains(s.Name));
            }
            if (s.Kind != 0)
            {
                query = query.Where(q => q.Kind == s.Kind);
            }
            if (s.Day != 0)
            {
                query = query.Where(q => q.Day == s.Day);
            }
            if (s.DateCreated.HasValue == true)
            {
                query = query.Where(q => q.DateCreated == s.DateCreated);
            }
            if (s.Price != 0)
            {
                query = query.Where(q => q.Price == s.Price);
            }

            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "coursesid")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.CourseId);
                else query = query.OrderByDescending(q => q.CourseId);
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "name")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.Name);
                else query = query.OrderByDescending(q => q.Name);
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "kind")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.Kind);
                else query = query.OrderByDescending(q => q.Kind);
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "day")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.Day);
                else query = query.OrderByDescending(q => q.Day);
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "datecreated")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.DateCreated);
                else query = query.OrderByDescending(q => q.DateCreated);
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "price")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.Price);
                else query = query.OrderByDescending(q => q.Price);
            }

            int index = s.Page * s.PageSize;
            result.Count = query.Count();
            result.Items = query.Skip(index).Take(s.PageSize).ToList();

            return result;
        }

        public List<CoursesViewModel> GetAllCourse()
        {
            IQueryable<CoursesViewModel> query = CreateQuery();
            var list = query.OrderBy(t => t.Name).ToList();

            return list;
        }

        public CoursesViewModel GetCourseById(int id)
        {
            CoursesViewModel entity = new CoursesViewModel();

            IQueryable<CoursesViewModel> query = CreateQuery();
            entity = query.Where(t => t.CourseId == id).Single();

            return entity;
        }

        public List<CourseWorkshopsViewModel> GetWorkshopsByCourse(int id)
        {
            List<CourseWorkshopsViewModel> list = new List<CourseWorkshopsViewModel>();

            var query = from p in db.Workshop.AsNoTracking().Include("Course")
                        where p.CourseId == id
                        select new CourseWorkshopsViewModel()
                        {
                            CourseId = p.CourseId,
                            WorkshopId = p.WorkshopId,
                            Name = p.Name,
                            DateIn = p.DateIn,
                            DateFi = p.DateFi
                        };

            list = query.ToList();

            return list;
        }

        public List<CourseTeachersViewModel> GetTeachersByCourse(int id)
        {
            List<CourseTeachersViewModel> list = new List<CourseTeachersViewModel>();

            var query = from p in db.Workshop.AsNoTracking().Include("Teacher")
                        where p.CourseId == id
                        select new CourseTeachersViewModel()
                        {
                            CourseId = p.CourseId,
                            TeacherId = p.Teacher.TeacherId,
                            Name = p.Teacher.Name,
                            Surname = p.Teacher.Surname,
                            Address = p.Teacher.Address,
                            Cap = p.Teacher.Cap,
                            City = p.Teacher.City,
                            Province = p.Teacher.Province
                        };

            list = query.ToList();

            return list;
        }

        public DtoCommandResult InsertCourse(CoursesViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();

            Course course = new Course()
            {
                CourseId = entity.CourseId,
                Name = entity.Name,
                Kind = entity.Kind,
                Day = entity.Day,
                DateCreated = entity.DateCreated,
                Price = entity.Price,
            };
            db.Course.Add(course);
            db.SaveChanges();

            return result;
        }

        public DtoCommandResult UpdateCourse(CoursesViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();

            Course course = db.Course.Find(entity.CourseId);
            course.Name = entity.Name;
            course.Kind = entity.Kind;
            course.Day = entity.Day;
            course.DateCreated = entity.DateCreated;
            course.Price = entity.Price;
            db.SaveChanges();

            return result;
        }

        public DtoCommandResult DeleteCourse(int id)
        {
            DtoCommandResult result = new DtoCommandResult();

            try
            {
                Course course = new Course();
                course.CourseId = id;
                db.Entry(course).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                result.AddErrorMessage(msg, true);
            }
            catch (Exception ex)
            {
                result.AddErrorMessage(ex.Message);
            }

            return result;
        }

        private IQueryable<CoursesViewModel> CreateQuery()
        {
            var query = from p in db.Course.AsNoTracking()
                        select new CoursesViewModel()
                        {
                            CourseId = p.CourseId,
                            Name = p.Name,
                            Kind = p.Kind,
                            Day = p.Day,
                            DateCreated = p.DateCreated,
                            Price = p.Price
                        };

            return query;
        }
    }
}
