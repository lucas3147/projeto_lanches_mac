﻿using System.Data;
using System.Reflection;

namespace LanchesMac.Areas.Admin.FastReportUtil
{
    public class HelperFastReport
    {
        public static DataTable GetTable<TEntity>(IEnumerable<TEntity> table, string nome) where TEntity : class
        {
            var offset = 78;
            DataTable result = new DataTable(nome);
            PropertyInfo[] infos= typeof(TEntity).GetProperties();
            foreach (PropertyInfo info in infos)
            {
                if (info.PropertyType.IsGenericType &&
                    info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    result.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType)));
                }
                else
                {
                    result.Columns.Add(new DataColumn(info.Name, info.PropertyType));
                }
            }
            foreach (var el in table)
            {
                DataRow row = result.NewRow();
                foreach (PropertyInfo info in infos)
                {
                    if (info.PropertyType.IsGenericType &&
                    info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        object t = info.GetValue(el);
                        if (t == null)
                        {
                            t = Activator.CreateInstance(Nullable.GetUnderlyingType(info.PropertyType));
                        }

                        row[info.Name] = t;
                    }
                    else
                    {
                        if (info.PropertyType == typeof(byte[]))
                        {
                            var imagemData = (byte[])info.GetValue(el);
                            var bytes = new byte[imagemData.Length - offset];
                            Array.Copy(imagemData, offset, bytes, 0, bytes.Length);
                            row[info.Name] = bytes;
                        }
                        else
                        {
                            row[info.Name] = info.GetValue(el);
                        }
                    }
                }
                result.Rows.Add(row);
            }
            return result;
        }
    }
}