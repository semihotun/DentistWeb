using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Filter
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class XmlFilterNameAttribute : FilterNameAttribute
    {
        public XmlFilterNameAttribute(string filterName):base(filterName,"Xml")
        {
        }
    }

}
