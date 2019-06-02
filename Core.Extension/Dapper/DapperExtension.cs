using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Core.Extension.Dapper
{
    public static partial class DapperExtension
    {
        public static T Get<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGet<T>(id, out string sql, out var parameters);
            return connection.Query<T>(sql, parameters, transaction, true, commandTimeout).FirstOrDefault();
        }

        public static async Task<T> GetAsync<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGet<T>(id, out string sql, out var parameters);
            return (await connection.QueryAsync<T>(sql, parameters, transaction, commandTimeout)).FirstOrDefault();
        }

        public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection, string whereSql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGetListByWhereSql<T>(whereSql, out var sql);
            return connection.QueryAsync<T>(sql, parameters, transaction, commandTimeout);
        }

        public static IEnumerable<T> GetList<T>(this IDbConnection connection, string whereSql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGetListByWhereSql<T>(whereSql, out var sql);
            return connection.Query<T>(sql, parameters, transaction, true, commandTimeout);
        }

        public static IEnumerable<T> GetList<T>(this IDbConnection connection)
        {
            return connection.GetList<T>(new { });
        }

        public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGetListByEntity<T>(whereConditions, out string sql);
            return connection.QueryAsync<T>(sql, whereConditions, transaction, commandTimeout);
        }

        public static Task<IEnumerable<T>> GetListAsync<T>(this IDbConnection connection)
        {
            return connection.GetListAsync<T>(new { });
        }

        public static Task<IEnumerable<T>> GetListPagedAsync<T>(this IDbConnection connection, int pageNumber, int rowsPerPage, string conditions, string orderBy, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGetListPagedByWhereSql<T>(pageNumber, rowsPerPage, orderBy, conditions, out string sql);
            return connection.QueryAsync<T>(sql, parameters, transaction, commandTimeout);
        }

        public static IEnumerable<T> GetListPaged<T>(this IDbConnection connection, int pageNumber, int rowsPerPage, string conditions, string orderBy, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            PrepareGetListPagedByWhereSql<T>(pageNumber, rowsPerPage, orderBy, conditions, out string sql);
            return connection.Query<T>(sql, parameters, transaction, true, commandTimeout);
        }
    }
}