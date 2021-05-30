using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTest.Models.Entity;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;

namespace WebTest.Controllers
{
    public class DataBaseController : Controller
    {
        private IHostingEnvironment server;

        public DataBaseController(IHostingEnvironment hosting)
        {
            server = hosting;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            int index = 0;
            string file0 = server.ContentRootPath + "\\files\\file0.csv";
            string file1 = server.ContentRootPath + "\\files\\file1.csv";
            string file2 = server.ContentRootPath + "\\files\\file2.csv";
            string[] lines0 = System.IO.File.ReadAllLines(file0);
            string[] lines1 = System.IO.File.ReadAllLines(file1);
            string[] lines2 = System.IO.File.ReadAllLines(file2);

            List<Province> plist = new List<Province>();
            foreach (string line in lines0)
            {
                string[] field = line.Split(",");

                Province vm = new Province()
                {
                    ProvinceId = field[0],
                    ProvinceName = field[1]
                };
                plist.Add(vm);
            }

            List<Teacher> tlist = new List<Teacher>();
            List<Student> slist = new List<Student>();
            foreach (string line in lines1)
            {
                string[] field = line.Split(",");

                if (index < 200)
                {
                    Teacher vm = new Teacher()
                    {
                        Name = field[1],
                        Surname = field[2],
                        Address = field[3],
                        Cap = field[4],
                        City = field[5],
                        Province = field[6],
                    };
                    tlist.Add(vm);
                }
                else
                {
                    Student vm = new Student()
                    {
                        Name = field[1],
                        Surname = field[2],
                        Address = field[3],
                        Cap = field[4],
                        City = field[5],
                        Province = field[6],
                    };
                    slist.Add(vm);
                }

                index++;
            }

            var ic = CultureInfo.InvariantCulture;
            List<Course> clist = new List<Course>();
            foreach (string line in lines2)
            {
                string[] field = line.Split(",");

                Course vm = new Course()
                {
                    Name = field[1],
                    Kind = Convert.ToInt32(field[2]),
                    Day = Convert.ToInt32(field[3]),
                    DateCreated = Convert.ToDateTime(field[4]),
                    Price = Convert.ToDecimal(field[5], ic),
                };
                clist.Add(vm);
            }

            List<Workshop> wlist = new List<Workshop>();
            DateTime today = Convert.ToDateTime("2018/10/01");
            for (int j = 1; j < lines2.Length; j++)
            {
                if (j % 2 == 0)
                    continue;

                for (int k = 1; k < 4; k++)
                {
                    int start = k * 4;
                    string[] field = lines2[j - 1].Split(",");
                    int day = Convert.ToInt32(field[3]);

                    Workshop vm = new Workshop()
                    {
                        Name = field[1],
                        DateIn = today.AddDays(start),
                        DateFi = today.AddDays(start + (day - 1)),
                        CourseId = Convert.ToInt32(j),
                        TeacherId = j + 2
                    };
                    wlist.Add(vm);
                }
            }

            int startstudent = 1;
            List<WorkshopDetail> dlist = new List<WorkshopDetail>();
            for (int j = 1; j < wlist.Count; j++)
            {
                for (int k = 1; k < 8; k++)
                {
                    WorkshopDetail vm = new WorkshopDetail()
                    {
                        WorkshopId = j,
                        StudentId = startstudent + j + k
                    };
                    dlist.Add(vm);
                }
                startstudent = startstudent + 10;
            }

            string strconn = "Server=(localdb)\\mssqllocaldb;Database=appstudenti;Trusted_Connection=True;";
            SqlConnection conn = new SqlConnection(strconn);

            conn.Open();

            //conn.Insert(plist);
            //conn.Insert(tlist);
            //conn.Insert(slist);
            //conn.Insert(clist);
            //conn.Insert(wlist);
            //conn.Insert(dlist);

            conn.Close();

            return View();
        }
    }
}
