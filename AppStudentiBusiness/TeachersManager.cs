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
    public class TeachersManager : ITeachersManager
    {
        private AppSettings appsettings;
        private appstudentiContext db;

        private string msg = "Teacher non annullabile, esistono relazioni!";

        public TeachersManager(IOptions<AppSettings> options, appstudentiContext context)
        {
            appsettings = options.Value;
            db = context;
        }

        public ResultData<TeachersViewModel> GetTeachersBySearch(TeachersSearch s)
        {
            ResultData<TeachersViewModel> result = new ResultData<TeachersViewModel>();

            IQueryable<TeachersViewModel> query = CreateQuery();

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

            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "teacherid")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    query = query.OrderBy(q => q.TeacherId);
                else query = query.OrderByDescending(q => q.TeacherId);
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

        public List<TeachersViewModel> GetAllTeacher()
        {
            IQueryable<TeachersViewModel> query = CreateQuery();
            var list = query.OrderBy(t => t.Surname).ToList();

            return list;
        }

        public TeachersViewModel GetTeacherById(int id)
        {
            TeachersViewModel entity = new TeachersViewModel();

            IQueryable<TeachersViewModel> query = CreateQuery();
            entity = query.Where(t => t.TeacherId == id).Single();

            return entity;
        }

        public List<TeachersViewModel> GetTeacherByName(string name)
        {
            List<TeachersViewModel> list = new List<TeachersViewModel>();

            IQueryable<TeachersViewModel> query = CreateQuery();
            list = query.Where(t => t.Surname.StartsWith(name)).ToList();

            return list;
        }      

        public DtoCommandResult InsertTeacher(TeachersViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();

            if (entity.Name == "qwe2")
            {
                result.AddErrorMessage("Name/2 not valid!");
                return result;
            }

            Teacher teacher = new Teacher()
            {
                TeacherId = entity.TeacherId,
                Name = entity.Name,
                Surname = entity.Surname,
                Address = entity.Address,
                Cap = entity.Cap,
                City = entity.City,
                Province = entity.Province
            };
            db.Teacher.Add(teacher);
            db.SaveChanges();

            return result;
        }

        public DtoCommandResult UpdateTeacher(TeachersViewModel entity)
        {
            DtoCommandResult result = new DtoCommandResult();

            if (entity.Name == "qwe2")
            {
                result.AddErrorMessage("Name/2 not valid!");
                return result;
            }

            Teacher teacher = db.Teacher.Find(entity.TeacherId);
            teacher.Name = entity.Name;
            teacher.Surname = entity.Surname;
            teacher.Address = entity.Address;
            teacher.Cap = entity.Cap;
            teacher.City = entity.City;
            teacher.Province = entity.Province;
            db.SaveChanges();

            return result;
        }

        public DtoCommandResult DeleteTeacher(int id)
        {
            DtoCommandResult result = new DtoCommandResult();

            try
            {
                Teacher teacher = new Teacher();
                teacher.TeacherId = id;
                db.Entry(teacher).State = EntityState.Deleted;
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

        private IQueryable<TeachersViewModel> CreateQuery()
        {
            var query = from p in db.Teacher.AsNoTracking()
                        select new TeachersViewModel()
                        {
                            TeacherId = p.TeacherId,
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
