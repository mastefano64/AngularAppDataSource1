using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon.Model.Search
{
    public class TeachersSearch
    {
        public TeachersSearch()
        {
            PageSize = 25;
        }
      
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Cap { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string OrderbyColumn { get; set; }
        public string OrderbyDirection { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
