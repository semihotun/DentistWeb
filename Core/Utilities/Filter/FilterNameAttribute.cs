using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Filter
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class FilterNameAttribute : Attribute
    {
        public string FilterName;
        public string FilterType;
        public FilterNameAttribute(string filterName,string filterType)
        {
            this.FilterName = filterName;
            this.FilterType = filterType;
        }

    }


}
