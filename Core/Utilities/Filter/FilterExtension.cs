using Core.Utilities.Pagedlist;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Utilities.Filter
{
    public static class FilterExtension
    {
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> contex, IFilterable filtermodel)
        {
            Expression finalExpression = Expression.Constant(true);
            var type = filtermodel.GetType();
            var parameter = Expression.Parameter(typeof(T), "x");
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                try
                {
                    Filter attr = (Filter)propertyInfo.GetCustomAttributes<Filter>().First();
                    Expression expression = null;
                    var columnName = string.IsNullOrEmpty(attr.queryColumn) ? propertyInfo.Name : attr.queryColumn;
                    var member = Expression.Property(parameter, columnName);
                    var constantValue = propertyInfo.GetValue(filtermodel, null);
                    var constant = Expression.Constant(constantValue);

                    if (constantValue != null && !string.IsNullOrEmpty(constantValue.ToString()) && constantValue.ToString() != "0")
                    {
                        switch (attr.filters)
                        {
                            case FilterOperators.Equals:
                                expression = Expression.Equal(member, constant);
                                break;
                            case FilterOperators.Contains:
                                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                                var someValue = Expression.Constant(constantValue, typeof(string));
                                expression = Expression.Call(member, method, someValue);
                                break;
                            case FilterOperators.GreaterThan:
                                expression = Expression.GreaterThanOrEqual(member, constant);
                                break;
                            case FilterOperators.LessThan:
                                expression = Expression.LessThanOrEqual(member, constant);
                                break;
                            case FilterOperators.NotEquals:
                                expression = Expression.NotEqual(member, constant);
                                break;

                        }
                        finalExpression = Expression.AndAlso(finalExpression, expression);
                    }

                }
                catch { }
            }
            var data = contex.Where(Expression.Lambda<Func<T, bool>>(finalExpression, parameter));
            return data;
        }

        public static IQueryable<T> ApplyTableFilter<T>(this IQueryable<T> contex, PagedListFilterModel filtermodel)
        {
            Expression finalExpression = Expression.Constant(true);
            var parameter = Expression.Parameter(typeof(T), "x");
            if (filtermodel.FilterModelList != null && filtermodel.FilterModelList.Count() > 0)
            {
                foreach (var item in filtermodel.FilterModelList)
                {

                    Expression expression = null;
                    var propertyInfo = contex.GetType().GetGenericArguments()[0].GetProperty(item.PropertyName);
                    var member = Expression.Property(parameter, item.PropertyName);
                    object value = TypeDescriptor.GetConverter(member.Type).ConvertFromString(item.Filter);
                    var constant = Expression.Constant(value, member.Type);

                    switch (Int32.Parse(item.FilterType))
                    {
                        case (int)FilterOperators.Equals:
                            expression = Expression.Equal(member, constant);
                            break;
                        case (int)FilterOperators.Contains:
                            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            expression = Expression.Call(member, method, constant);
                            break;
                        case (int)FilterOperators.GreaterThan:
                            expression = Expression.GreaterThanOrEqual(member, constant);
                            break;
                        case (int)FilterOperators.LessThan:
                            expression = Expression.LessThanOrEqual(member, constant);
                            break;
                        case (int)FilterOperators.NotEquals:
                            expression = Expression.NotEqual(member, constant);
                            break;
                    }

                    if (expression != null)
                    {
                        if(item.AndOrOperation == "Or")
                        {
                            finalExpression = Expression.Or(finalExpression, expression);
                        }
                        else
                        {
                            finalExpression = Expression.AndAlso(finalExpression, expression);
                        }
                    }
                }

                var data = contex.Where(Expression.Lambda<Func<T, bool>>(finalExpression, parameter));
                return data;
            }
            else
            {
                return contex;
            }

        }








    }

}