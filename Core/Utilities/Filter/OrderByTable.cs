using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace Core.Utilities.Filter
{
    public static class OrderByTable
    {
        public static IQueryable<T> ToTableOrderBy<T>(this IQueryable<T> contex,string OrderByColumnName)
        {
            if (!string.IsNullOrEmpty(OrderByColumnName))
                contex = contex.OrderBy(OrderByColumnName.Replace(",", " "));

            return contex;
        }

    }
}
