using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon.Model.ViewModel
{
    public class WorkshopsViewModel
    {
        public int WorkshopId { get; set; }
        public string Name { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateFi { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}
