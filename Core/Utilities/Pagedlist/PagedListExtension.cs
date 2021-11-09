using Core.Utilities.Filter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Utilities.Pagedlist
{
    public static class PagedListExtension
    {
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {     
            if (source == null)
                return new PagedList<T>();

            var result = new PagedList<T>();
            result.PageIndex = pageIndex;
            result.PageSize = Math.Max(pageSize, 1);
            result.TotalCount = source.Count();
            result.TotalPages = result.TotalCount / result.PageSize;

            if(result.PageSize < result.TotalCount)
                result.Data = await source.Skip((result.PageIndex - 1) * result.PageSize).Take(pageSize).ToListAsync();
            else
                result.Data =await source.ToListAsync();


            if (result.TotalCount % pageSize > 0)
                result.TotalPages++;

            var sourceType = source.ElementType;
            result.PropertyInfos = sourceType.GetProperties().Select(x =>
            {
                var data = new GridPropertyInfo();
                var attrFilterName = x.GetCustomAttributes<FilterNameAttribute>().FirstOrDefault();
                data.PropertyType = attrFilterName == null ? x.PropertyType.Name : attrFilterName.FilterType;

                data.PropertyName = char.ToLowerInvariant(x.Name[0]) + x.Name.Substring(1);
                data.AttrFilterName = x.GetCustomAttributes<JsonFilterNameAttribute>().Select(x => x.FilterName).FirstOrDefault();
                return data;
            });

            return result;
        }

        public static IPagedList<TResult> Select<TSource, TResult>(this IPagedList<TSource> source, Func<TSource, TResult> selector)
        {
            var subset=source.Data.Select(selector);
            var result = new PagedList<TResult>();
            result.PageIndex = source.PageIndex;
            result.PageSize = source.PageSize;
            result.TotalCount = source.TotalCount;
            result.TotalPages = source.TotalPages;
            result.Data = subset.ToList();

            if (source.Data.GetType() != selector.Method.ReturnType)
            {
                result.PropertyInfos = selector.Method.ReturnType.GetProperties()
                    .Select(x =>  
                    {
                        var data = new GridPropertyInfo();
                        var attrFilterName = x.GetCustomAttributes<FilterNameAttribute>().FirstOrDefault();
                        data.PropertyType = attrFilterName == null ?  x.PropertyType.Name : attrFilterName.FilterType;

                        data.PropertyName = char.ToLowerInvariant(x.Name[0]) + x.Name.Substring(1);
                        data.AttrFilterName = x.GetCustomAttributes<JsonFilterNameAttribute>().Select(x => x.FilterName).FirstOrDefault();
                        return data;
                    });
            }
            else
            {
                result.PropertyInfos = source.PropertyInfos;
            }

            return result;
        }


    }
}
