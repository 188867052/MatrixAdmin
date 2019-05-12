using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Core.Model.DataAccess
{
    public static class SqlParameterExtensions
    {
        public static object[] ToSqlParamsArray(this object obj, SqlParameter[] additionalParams = null)
        {
            List<SqlParameter> result = ToSqlParamsList(obj, additionalParams);
            return result.ToArray<object>();
        }

        public static List<SqlParameter> ToSqlParamsList(this object obj, SqlParameter[] additionalParams = null)
        {
            var props = (
                from p in obj.GetType().GetProperties()
                let nameAttr = p.GetCustomAttributes(typeof(QueryParamNameAttribute), true)
                let ignoreAttr = p.GetCustomAttributes(typeof(QueryParamIgnoreAttribute), true)
                select new { Property = p, Names = nameAttr, Ignores = ignoreAttr }).ToList();

            List<SqlParameter> result = new List<SqlParameter>();

            props.ForEach(p =>
            {
                if (p.Ignores != null && p.Ignores.Length > 0)
                {
                    return;
                }

                QueryParamNameAttribute name = p.Names.FirstOrDefault() as QueryParamNameAttribute;
                QueryParamInfo pinfo = new QueryParamInfo();

                if (name != null && !string.IsNullOrWhiteSpace(name.Name))
                {
                    pinfo.Name = name.Name.Replace("@", string.Empty);
                }
                else
                {
                    pinfo.Name = p.Property.Name.Replace("@", string.Empty);
                }

                pinfo.Value = p.Property.GetValue(obj) ?? DBNull.Value;
                SqlParameter sqlParam = new SqlParameter(pinfo.Name, TypeConvertor.ToSqlDbType(p.Property.PropertyType))
                {
                    Value = pinfo.Value
                };

                result.Add(sqlParam);
            });

            if (additionalParams != null && additionalParams.Length > 0)
            {
                result.AddRange(additionalParams);
            }

            return result;
        }

        private class QueryParamInfo
        {
            public string Name { get; set; }

            public object Value { get; set; }
        }
    }
}
