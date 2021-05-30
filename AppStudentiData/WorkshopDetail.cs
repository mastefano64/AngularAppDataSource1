using System;
using System.Collections.Generic;

namespace AppStudentiData
{
    public partial class WorkshopDetail
    {
        public int Id { get; set; }
        public int WorkshopId { get; set; }
        public int StudentId { get; set; }

        public Student Student { get; set; }
        public Workshop Workshop { get; set; }
    }
}
