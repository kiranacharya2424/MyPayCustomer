using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Common
{
    public static class CommonEntityConverter
    {
        public static List<T> DataTableToList<T>(this DataTable table) where T : new()
        {
            List<T> list = new List<T>();
            //if (table.Rows.Count == 0 || table == null) { return list; }
            var typeProperties = typeof(T).GetProperties().Select(propertyInfo => new
            {
                PropertyInfo = propertyInfo,
                Type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType
            }).ToList();

            foreach (var row in table.Rows.Cast<DataRow>())
            {
                T obj = new T();
                foreach (var typeProperty in typeProperties)
                {
                    try
                    {
                        if (table.Columns.Contains(typeProperty.PropertyInfo.Name))
                        {
                            object value = row[typeProperty.PropertyInfo.Name];
                            object safeValue = value == null || DBNull.Value.Equals(value)
                                ? null
                                : Convert.ChangeType(value, typeProperty.Type);

                            typeProperty.PropertyInfo.SetValue(obj, safeValue, null);
                        }
                    }
                    catch { }

                }
                list.Add(obj);
            }
            return list;
        }

        public static T DataTableToRecord<T>(this DataTable table) where T : new()
        {
            T obj = new T();
            //if (table.Rows.Count == 0 || table == null) { return list; }
            var typeProperties = typeof(T).GetProperties().Select(propertyInfo => new
            {
                PropertyInfo = propertyInfo,
                Type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType
            }).ToList();

            foreach (var row in table.Rows.Cast<DataRow>())
            {
                
                foreach (var typeProperty in typeProperties)
                {
                    try
                    {
                        if (table.Columns.Contains(typeProperty.PropertyInfo.Name))
                        {
                            object value = row[typeProperty.PropertyInfo.Name];
                            object safeValue = value == null || DBNull.Value.Equals(value)
                                ? null
                                : Convert.ChangeType(value, typeProperty.Type);

                            typeProperty.PropertyInfo.SetValue(obj, safeValue, null);
                        }
                    }
                    catch { }
                   
                }
            }
            return obj;
        }
    }
}