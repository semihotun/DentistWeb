using Core.Utilities.Pagedlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Filter
{
    public static class TableExtension
    {
        public static async Task<IPagedList<T>> ToTableSettings<T>(this IQueryable<T> query, PagedListFilterModel pagedListFilterModel)
        {
           return await query.ApplyTableFilter(pagedListFilterModel)
                   .ToTableOrderBy(pagedListFilterModel.OrderByColumnName)
                   .ToPagedListAsync(pagedListFilterModel.PageIndex, pagedListFilterModel.PageSize);
        }

    }
}
