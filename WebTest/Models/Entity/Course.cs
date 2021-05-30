using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebTest.Models.Entity
{
    [Table("Course")]
    public class Course
    {
        public string Name { get; set; }
        public int Kind { get; set; }
        public int Day { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Price { get; set; }
    }
}
