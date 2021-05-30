using System;
using System.Collections.Generic;
using System.Text;

namespace AppStudentiCommon
{
    public class ResultData<T>
    {
        public int Page { get; set; }
        public string Url { get; set; }
        public List<T> Items { get; set; }
        public int Count { get; set; }
    }
}
