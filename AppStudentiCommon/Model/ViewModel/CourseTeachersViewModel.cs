using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon.Model.ViewModel
{
    public class CourseTeachersViewModel
    {
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Cap { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
    }
}
