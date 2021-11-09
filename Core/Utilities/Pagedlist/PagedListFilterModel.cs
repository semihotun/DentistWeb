using Core.Utilities.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Pagedlist
{
     public class PagedListFilterModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string OrderByColumnName { get; set; }
        public List<FilterModel> FilterModelList { get; set; }
    }
}
