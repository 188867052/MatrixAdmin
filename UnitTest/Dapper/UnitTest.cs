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
    public class UnitTest
    {
        [Test]
        public void TestFilteredGetList()
        {
            User user = DapperExtension.Connection.QueryFirstOrDefault<User>("SELECT * FROM [user]");
            if (user != null)
            {
                var users = this.OpenConnection.GetList<User>($"where login_name = '{user.LoginName}'");
                Assert.IsNotNull(users);
            }
        }

        [Test]
        public void TestFilteredGetListParameters()
        {
            User user = DapperExtension.Connection.QueryFirstOrDefault<User>("SELECT * FROM [user]");
            if (user != null)
            {
                IEnumerable<User> users = this.OpenConnection.GetList<User>("where Id = @Id", new { user.Id });
                Assert.IsNotNull(users);
                users = this.OpenConnection.GetList<User>(new { user.Id });
                Assert.IsNotNull(users);
                users = this.OpenConnection.GetList<User>(new { });
                Assert.IsNotNull(users);
            }
        }

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

        [Test]
        public void TestDeleteById()
        {
            Log log = DapperExtension.Connection.QueryFirstOrDefault<Log>("SELECT * FROM [log]");
            if (log != null)
            {
                var count = this.OpenConnection.Delete<Log>(log.Id);
                Assert.AreEqual(count, 1);
            }

            log = DapperExtension.Connection.QueryFirstOrDefault<Log>("SELECT * FROM [log]");
            if (log != null)
            {
                var count = this.OpenConnection.DeleteAsync<Log>($"where Id = '{log.Id}'");
                Assert.AreEqual(count.Result, 1);
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
