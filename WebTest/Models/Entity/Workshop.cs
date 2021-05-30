using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebTest.Models.Entity
{
    [Table("Workshop")]
    public class Workshop
    {
        public string Name { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateFi { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
    }
}
