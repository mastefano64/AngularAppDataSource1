using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon.Model.Search
{
    public class WorkshopsSearch
    {
        public WorkshopsSearch()
        {
            PageSize = 25;
        }

        public string Name { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateFi { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string OrderbyColumn { get; set; }
        public string OrderbyDirection { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}
