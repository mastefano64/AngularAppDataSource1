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
    public class StudentsMockManager : IStudentsManager
    {
        private AppSettings appsettings;

        public StudentsMockManager(IOptions<AppSettings> options)
        {
            appsettings = options.Value;
        }

        public ResultData<StudentsViewModel> GetStudentsBySearch(StudentsSearch s)
        {
            ResultData<StudentsViewModel> result = new ResultData<StudentsViewModel>();

            List<StudentsViewModel> list = CreateList();

            if (String.IsNullOrEmpty(s.Name) == false)
            {
                list = list.Where(q => q.Name == s.Name).ToList();
            }
            if (String.IsNullOrEmpty(s.Surname) == false)
            {
                list = list.Where(q => q.Surname == s.Surname).ToList();
            }
            if (String.IsNullOrEmpty(s.Address) == false)
            {
                list = list.Where(q => q.Address == s.Address).ToList();
            }
            if (String.IsNullOrEmpty(s.City) == false)
            {
                list = list.Where(q => q.City == s.City).ToList();
            }
            if (String.IsNullOrEmpty(s.Province) == false)
            {
                list = list.Where(q => q.Province == s.Province).ToList();
            }

            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "studentid")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.StudentId).ToList();
                else list = list.OrderByDescending(q => q.StudentId).ToList();
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "name")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.Name).ToList();
                else list = list.OrderByDescending(q => q.Name).ToList();
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "surname")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.Surname).ToList();
                else list = list.OrderByDescending(q => q.Surname).ToList();
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "address")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.Address).ToList();
                else list = list.OrderByDescending(q => q.Address).ToList();
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "cap")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.Cap).ToList();
                else list = list.OrderByDescending(q => q.Cap).ToList();
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "city")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.City).ToList();
                else list = list.OrderByDescending(q => q.City).ToList();
            }
            if (!String.IsNullOrEmpty(s.OrderbyColumn) && s.OrderbyColumn.ToLower() == "province")
            {
                if (s.OrderbyDirection.ToLower() == "asc")
                    list = list.OrderBy(q => q.Province).ToList();
                else list = list.OrderByDescending(q => q.Province).ToList();
            }

            result.Count = list.Count;
            int index = s.Page * s.PageSize;
            result.Items = list.Skip(index).Take(s.PageSize).ToList();

            return result;
        }

        public List<StudentsViewModel> GetAllStudent()
        {
            List<StudentsViewModel> list = CreateList();

            return list;
        }

        public StudentsViewModel GetStudentById(int id)
        {
            StudentsViewModel entity = new StudentsViewModel();

            List<StudentsViewModel> list = CreateList();
            entity = list.Where(w => w.StudentId == id).Single();

            return entity;
        }

        public List<StudentsViewModel> GetStudentByName(string name)
        {
            List<StudentsViewModel> list = CreateList();

            list = list.Where(q => q.Surname.StartsWith(name)).ToList();

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

            return result;
        }

        public DtoCommandResult DeleteStudent(int id)
        {
            DtoCommandResult result = new DtoCommandResult();

            return result;
        }

        private List<StudentsViewModel> CreateList()
        {
            List<StudentsViewModel> list = new List<StudentsViewModel>();

            for (int i = 1; i < 300; i++)
            {
                string cap = "";
                string prov = "ge";
                if (i > 100)
                    prov = "mi";
                if (i > 200)
                    prov = "to";
                if (i % 2 != 0)
                    cap = "10100";
                else cap = "10200";
                list.Add(new StudentsViewModel()
                {
                    StudentId = i,
                    Name = $"students name{i}",
                    Surname = $"surname{i}",
                    Address = $"address{i}",
                    Cap = cap,
                    City = $"city{i}",
                    Province = prov
                });
            }

            return list;
        }
    }
}
