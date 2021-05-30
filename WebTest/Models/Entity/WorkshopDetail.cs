using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebTest.Models.Entity
{
    [Table("WorkshopDetail")]
    public class WorkshopDetail
    {
        public int WorkshopId { get; set; }
        public int StudentId { get; set; }        
    }
}
