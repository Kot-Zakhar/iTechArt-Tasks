using System;
using System.Collections.Generic;
using System.Reflection;

namespace RateLimit.WebApp.Models
{
    public class FilterModel<T> where T : class
    {
        private bool _sort = false;
        private bool _sortASC = true;
        private string _sortField = null;
        private string _filterFieldName = null;

        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Sort {
            get
            {
                return _sort ? (_sortASC ? "ASC" : "DESC") : null;
            }
            set
            {
                switch (value.ToLower())
                {
                    case "asc":
                        _sort = true;
                        _sortASC = true;
                        break;
                    case "desc":
                        _sort = true;
                        _sortASC = false;
                        break;
                    default:
                        _sort = false;
                        break;
                }
            }
        }
        public string SortField {
            get => _sortField;
            set
            {
                _sortField = GetPropertyName(value);
                _sort = !String.IsNullOrEmpty(_sortField);
            }
        }
        public bool Filter
        {
            get => !String.IsNullOrEmpty(_filterFieldName) && FilterValues != null && FilterValues.Count > 0;
        }
        public string FilterField
        {
            get => _filterFieldName;
            set
            {
                _filterFieldName = GetPropertyName(value);
            }
        }
        public List<string> FilterValues { get; set; }

        public FilterModel()
        {
            Page = 0;
            PageSize = 10;
        }

        private static string GetPropertyName(string name)
        {
            return String.IsNullOrEmpty(name) ? null : typeof(T).GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.Name;
        }
    }

    public class FilteredResult<T> where T : class
    {
        public IEnumerable<T> Values { get; set; } = new List<T>();
        public int Page;
        public int PageSize; 
        public bool Next = false;
        public bool Previous = false;
        public string Url = null;
        public string NextPage { get; set; } = null;
        public string PreviousPage { get; set; } = null;
    }
}
