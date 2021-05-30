using System;
using System.Collections.Generic;

namespace AppStudentiData
{
    public partial class Course
    {
        public Course()
        {
            Workshop = new HashSet<Workshop>();
        }

        public int CourseId { get; set; }
        public string Name { get; set; }
        public int Kind { get; set; }
        public int Day { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Price { get; set; }

        public ICollection<Workshop> Workshop { get; set; }
    }
}
