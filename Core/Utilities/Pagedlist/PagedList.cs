using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Pagedlist
{
    public class PagedList<T> : IPagedList<T>
    {
        public List<T> Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex + 1 < TotalPages;

        public IEnumerable<GridPropertyInfo> PropertyInfos { get; set; }
    }
}
