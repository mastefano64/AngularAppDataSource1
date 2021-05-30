using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon.Model.ViewModel
{
    public class CourseWorkshopsViewModel
    {
        public int CourseId { get; set; }
        public int WorkshopId { get; set; }
        public string Name { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateFi { get; set; }
    }
}
