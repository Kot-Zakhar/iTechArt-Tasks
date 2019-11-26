using System.Collections.Generic;
using System.Reflection;

namespace ShareMe.WebApplication.Models.Grid
{
    public class GridModel<T> where T : ApiModels.ApiModel
    {
        private const string _asc = "ASC";
        private const string _desc = "DESC";
        private const int _defaultPageSize = 10;
        private string _sortField = null;
        private string _filterFieldName = null;

        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = _defaultPageSize;
        public bool IsSorting { get; private set; } = false;
        public bool IsSortingASC { get; private set; } = true;
        public string SortType
        {
            get
            {
                return IsSorting ? IsSortingASC ? _asc : _desc : null;
            }
            set
            {
                switch (value.ToUpper())
                {
                    case _asc:
                        IsSorting = true;
                        IsSortingASC = true;
                        break;
                    case _desc:
                        IsSorting = true;
                        IsSortingASC = false;
                        break;
                    default:
                        IsSorting = false;
                        break;
                }
            }
        }
        public string SortField
        {
            get => _sortField;
            set
            {
                _sortField = GetPropertyName(value);
                IsSorting = !string.IsNullOrEmpty(_sortField);
            }
        }
        public bool IsFiltering
        {
            get => !string.IsNullOrEmpty(_filterFieldName) && FilterValues != null && FilterValues.Count > 0;
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

        public GridModel()
        {}

        private static string GetPropertyName(string name)
        {
            return string.IsNullOrEmpty(name) ? null : typeof(T).GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.Name;
        }
    }
}
