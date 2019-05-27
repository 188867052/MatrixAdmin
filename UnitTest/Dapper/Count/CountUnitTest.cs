using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Core.Entity;
using Core.Extension.Dapper;
using Dapper;
using NUnit.Framework;

namespace Core.UnitTest.Dapper
{
    /// <summary>
    /// Api unit test.
    /// </summary>
    [TestFixture]
    public class CountUnitTest
    {
        [Test]
        public void TestRecordCount()
        {
            User user = DapperExtension.Connection.QueryFirstOrDefault<User>("SELECT * FROM [user]");
            if (user != null)
            {
                var count = this.OpenConnection.RecordCount<User>($"where Id = '{user.Id}'");
                Assert.AreEqual(count, 1);
                count = this.OpenConnection.RecordCount<User>();
                Assert.GreaterOrEqual(count, 0);
            }
        }

        private IDbConnection OpenConnection
        {
            get
            {
                SqlConnection connection = new SqlConnection(DapperExtension.Connection.ConnectionString);
                SimpleCRUD.SetDialect(Dialect.SQLServer);
                connection.Open();
                return connection;
            }
        }
    }
}
