using System;

namespace Core.Utilities.Filter
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class Filter : Attribute
    {
        public FilterOperators filters;
        public String queryColumn;
        public Filter(FilterOperators filters, string queryFilter) : this(filters)
        {
            this.filters = filters;
            this.queryColumn = queryFilter;
        }
        public Filter(FilterOperators filters)
        {
            this.filters = filters;
        }
    }

}
