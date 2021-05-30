using System;
using System.Collections.Generic;

namespace AppStudentiData
{
    public partial class Workshop
    {
        public Workshop()
        {
            WorkshopDetail = new HashSet<WorkshopDetail>();
        }

        public int WorkshopId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateFi { get; set; }

        public Course Course { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<WorkshopDetail> WorkshopDetail { get; set; }
    }
}
