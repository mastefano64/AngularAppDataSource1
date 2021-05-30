using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon.Model.ViewModel
{
    public class CoursesViewModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int Kind { get; set; }
        public int Day { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Price { get; set; }
    }
}
