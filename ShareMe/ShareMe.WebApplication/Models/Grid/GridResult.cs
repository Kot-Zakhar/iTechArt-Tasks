using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.Models.Grid
{
    public class GridResult<T> where T : class
    {
        public IList<T> Values { get; set; } = new List<T>();
        public int PageIndex;
        public int PageSize;
        public bool Next = false;
        public bool Previous = false;
        public string Url = null;
        public string NextPageUrl { get; set; } = null;
        public string PreviousPageUrl { get; set; } = null;
    }
}
