using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AppStudentiCommon;
using AppStudentiCommon.Model;
using AppStudentiCommon.Interface;
using AppStudentiCommon.Model.ViewModel;
using AppStudentiCommon.Model.Search;
using AppStudentiData;

namespace AppStudentiBusiness
{
    public class StudentsManager : IStudentsManager
    {
        private AppSettings appsettings;
        private appstudentiContext db;

        private string msg = "Student non annullabile, esistono relazioni!";

        public StudentsManager(IOptions<AppSettings> options, appstudentiContext context)
        {
            appsettings = options.Value;
            db = context;
        }

        public ResultData<StudentsViewModel> GetStudentsBySearch(StudentsSearch s)
        {
            ResultData<StudentsViewModel> result = new ResultData<StudentsViewModel>();

            IQueryable<StudentsViewModel> query = CreateQuery();

            if (String.IsNullOrEmpty(s.Name) == false)
            {
                query = query.Where(q => q.Name.StartsWith(s.Name));
            }
            if (String.IsNullOrEmpty(s.Surname) == false)
            {
                query = query.Where(q => q.Surname.StartsWith(s.Surname));
            }
            if (String.IsNullOrEmpty(s.Address) == false)
            {
                query = query.Where(q => q.Address.StartsWith(s.Address));
            }
            if (String.IsNullOrEmpty(s.City) == false)
            {
                query = query.Where(q => q.City.StartsWith(s.City));
            }
            if (String.IsNullOrEmpty(s.Province) == false)
            {
                query = query.Where(q => q.Province == s.Province);
            }

            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "studentid")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.StudentId);
                else query = query.OrderByDescending(q => q.StudentId);
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "name")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.Name);
                else query = query.OrderByDescending(q => q.Name);
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "surname")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.Surname);
                else query = query.OrderByDescending(q => q.Surname);
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "address")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.Address);
                else query = query.OrderByDescending(q => q.Address);
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "cap")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.Cap);
                else query = query.OrderByDescending(q => q.Cap);
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "city")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.City);
                else query = query.OrderByDescending(q => q.City);
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "province")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.Province);
                else query = query.OrderByDescending(q => q.Province);
            }

            int index = s.Page * s.PageSize;
            result.Count = query.Count();
            result.Items = query.Skip(index).Take(s.PageSize).ToList();

            return result;
        }

        public List<StudentsViewModel> GetAllStudent()
        {
            IQueryable<StudentsViewModel> query = CreateQuery();
            var list = query.OrderBy(t => t.Surname).ToList();

            return list;
        }

        public StudentsViewModel GetStudentById(int id)
        {
            StudentsViewModel entity = new StudentsViewModel();

            IQueryable<StudentsViewModel> query = CreateQuery();
            entity = query.Where(t => t.StudentId == id).Single();

            return entity;
        }

        public List<StudentsViewModel> GetStudentByName(string name)
        {
            List<StudentsViewModel> list = new List<StudentsViewModel>();

            IQueryable<StudentsViewModel> query = CreateQuery();
            list = query.Where(t => t.Surname.StartsWith(name)).ToList();

            return list;
        }

        public DtoCommandResult InsertStudent(StudentsViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();

            if (entity.Name == "qwe2")
            {
                result.AddErrorMessage("Name/2 not valid!");
                return result;
            }

            Student student = new Student()
            {
                StudentId = entity.StudentId,
                Name = entity.Name,
                Surname = entity.Surname,
                Address = entity.Address,
                Cap = entity.Cap,
                City = entity.City,
                Province = entity.Province
            };
            db.Student.Add(student);
            db.SaveChanges();

            return result;
        }

        public DtoCommandResult UpdateStudent(StudentsViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();

            if (entity.Name == "qwe2")
            {
                result.AddErrorMessage("Name/2 not valid!");
                return result;
            }

            Student student = db.Student.Find(entity.StudentId);
            student.Name = entity.Name;
            student.Surname = entity.Surname;
            student.Address = entity.Address;
            student.Cap = entity.Cap;
            student.City = entity.City;
            student.Province = entity.Province;
            db.SaveChanges();

            return result;
        }

        public DtoCommandResult DeleteStudent(int id)
        {
            DtoCommandResult result = new DtoCommandResult();

            try
            {
                Student student = new Student();
                student.StudentId = id;
                db.Entry(student).State = EntityState.Deleted;
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

        private IQueryable<StudentsViewModel> CreateQuery()
        {
            var query = from p in db.Student.AsNoTracking()
                        select new StudentsViewModel()
                        {
                            StudentId = p.StudentId,
                            Name = p.Name,
                            Surname = p.Surname,
                            Address = p.Address,
                            Cap = p.Cap,
                            City = p.City,
                            Province = p.Province
                        };

            return query;
        }
    }
}
