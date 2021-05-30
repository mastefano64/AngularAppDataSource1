using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon.Model.Search
{
    public class CoursesSearch
    {
        public CoursesSearch()
        {
            PageSize = 25;
        }
      
        public string Name { get; set; }
        public int Kind { get; set; }
        public int Day { get; set; }
        public DateTime? DateCreated { get; set; }
        public decimal Price { get; set; }
        public string OrderbyColumn { get; set; }
        public string OrderbyDirection { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
