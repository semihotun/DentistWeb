using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Filter
{

    //[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    //public class JsonFilterNameAttribute : Attribute
    //{
    //    public string FilterName;
    //    public string FilterType;
    //    public JsonFilterNameAttribute(string filterName) 
    //    {
    //        this.FilterName = filterName;
    //        this.FilterType = "Json";
    //    }

    //}

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class JsonFilterNameAttribute : FilterNameAttribute
    {
        public JsonFilterNameAttribute(string filterName) : base(filterName, "Json")
        {
        }
    }

}
