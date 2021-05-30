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
    public class WorkshopsManager : IWorkshopsManager
    {
        private AppSettings appsettings;
        private appstudentiContext db;

        private string msg = "Workshop non annullabile, esistono relazioni!";

        public WorkshopsManager(IOptions<AppSettings> options, appstudentiContext context)
        {
            appsettings = options.Value;
            db = context;
        }

        public ResultData<WorkshopsViewModel> GetWorkshopsBySearch(WorkshopsSearch s)
        {
            ResultData<WorkshopsViewModel> result = new ResultData<WorkshopsViewModel>();

            IQueryable<WorkshopsViewModel> query = CreateQuery();

            if (String.IsNullOrEmpty(s.Name) == false)
            {
                query = query.Where(q => q.Name == s.Name);
            }
            if (s.DateIn.HasValue == true)
            {
                query = query.Where(q => q.DateIn >= s.DateIn);
            }
            if (s.DateFi.HasValue == true)
            {
                query = query.Where(q => q.DateFi <= s.DateFi);
            }
            if (s.CourseId != 0)
            {
                query = query.Where(q => q.CourseId == s.CourseId);
            }
            if (s.TeacherId != 0)
            {
                query = query.Where(q => q.TeacherId == s.TeacherId);
            }

            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "Workshopsid")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.WorkshopId);
                else query = query.OrderByDescending(q => q.WorkshopId);
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "name")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.Name);
                else query = query.OrderByDescending(q => q.Name);
            }

            int index = s.Page * s.PageSize;
            result.Count = query.Count();
            result.Items = query.Skip(index).Take(s.PageSize).ToList();

            return result;
        }

        public WorkshopsViewModel GetWorkshopById(int id)
        {
            WorkshopsViewModel entity = new WorkshopsViewModel();

            IQueryable<WorkshopsViewModel> query = CreateQuery();
            entity = query.Where(t => t.WorkshopId == id).Single();

            return entity;
        }

        public DtoCommandResult InsertWorkshop(WorkshopsViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();

            Workshop workshop = new Workshop()
            {
                WorkshopId = entity.WorkshopId,
                TeacherId = entity.TeacherId,
                CourseId = entity.CourseId,
                Name = entity.Name,
                DateIn = entity.DateIn,
                DateFi = entity.DateFi,
            };
            db.Workshop.Add(workshop);
            db.SaveChanges();

            return result;
        }

        public DtoCommandResult UpdateWorkshop(WorkshopsViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();

            Workshop workshop = db.Workshop.Find(entity.WorkshopId);
            workshop.TeacherId = entity.TeacherId;
            workshop.CourseId = entity.CourseId;
            workshop.Name = entity.Name;
            workshop.DateIn = entity.DateIn;
            workshop.DateFi = entity.DateFi;
            db.SaveChanges();

            return result;
        }

        public DtoCommandResult DeleteWorkshop(int id)
        {
            DtoCommandResult result = new DtoCommandResult();

            try
            {
                Workshop workshop = new Workshop();
                workshop.WorkshopId = id;
                db.Entry(workshop).State = EntityState.Deleted;
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

        public List<WorkshopStudentsViewModel> GetStudentsByWorkshop(int id)
        {
            List<WorkshopStudentsViewModel> list = new List<WorkshopStudentsViewModel>();

            var query = (from p in db.Workshop.AsNoTracking().Include("WorkshopDetail.Student")
                         where p.WorkshopId == id
                         select p);

            foreach (Workshop row1 in query)
            {
                foreach (WorkshopDetail row2 in row1.WorkshopDetail)
                {
                    WorkshopStudentsViewModel detail = new WorkshopStudentsViewModel()
                    {
                        WorkshopId = row2.WorkshopId,
                        StudentId = row2.Student.StudentId,
                        Name = row2.Student.Name,
                        Surname = row2.Student.Surname,
                        Address = row2.Student.Address,
                        Cap = row2.Student.Cap,
                        City = row2.Student.City,
                        Province = row2.Student.Province
                    };
                    list.Add(detail);
                }
            }
            list = list.OrderBy(p => p.Surname).ToList();

            return list;
        }

        public DtoCommandResult InsertStudentsWorkshop(int workshopId, int studentId)
        {
            DtoCommandResult result = new DtoCommandResult();

            WorkshopDetail detail = new WorkshopDetail()
            {
                WorkshopId = workshopId,
                StudentId = studentId
            };
            db.WorkshopDetail.Add(detail);
            db.SaveChanges();

            result.Result = GetStudentsByWorkshop(workshopId);

            return result;
        }

        public DtoCommandResult DeleteStudentsWorkshop(int workshopId, int studentId)
        {
            DtoCommandResult result = new DtoCommandResult();

            var detail = db.WorkshopDetail.Where(w => w.WorkshopId == workshopId
                              && w.StudentId == studentId).Single();
            db.Entry(detail).State = EntityState.Deleted;
            db.SaveChanges();

            result.Result = GetStudentsByWorkshop(workshopId);

            return result;
        }

        private IQueryable<WorkshopsViewModel> CreateQuery()
        {
            var query = from p in db.Workshop.AsNoTracking()
                        join c in db.Course on p.CourseId equals c.CourseId
                        join t in db.Teacher on p.TeacherId equals t.TeacherId
                        select new WorkshopsViewModel()
                        {
                            WorkshopId = p.WorkshopId,
                            CourseId = p.CourseId,
                            CourseName = c.Name,
                            TeacherId = p.TeacherId,
                            TeacherName = t.Name + " " + t.Surname,
                            Name = p.Name,
                            DateIn = p.DateIn,
                            DateFi = p.DateFi
                        };

            return query;
        }
    }
}
