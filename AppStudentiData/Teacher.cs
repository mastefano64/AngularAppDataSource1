using System;
using System.Collections.Generic;

namespace AppStudentiData
{
    public partial class Teacher
    {
        public Teacher()
        {
            Workshop = new HashSet<Workshop>();
        }

        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Cap { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        public ICollection<Workshop> Workshop { get; set; }
    }
}
