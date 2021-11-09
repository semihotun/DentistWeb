using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Filter
{
    public class FilterModel
    {
        public string PropertyName { get; set; }

        public string FilterType { get; set; }

        public string Filter { get; set; }

        public bool JsonOrXml { get; set; }

        public string AndOrOperation { get; set; }

    }
}
