using System;
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
    public class GetUnitTest
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
        public void TestFindAllByExpression()
        {
            User user = DapperExtension.Connection.QueryFirstOrDefault<User>("SELECT * FROM [user]");
            if (user != null)
            {
                var users = this.OpenConnection.FindAll<User>(o => o.Id, user.Id);
                Assert.AreEqual(users.Count, 1);
            }
        }

        [Test]
        public void TestGetListNullableWhere()
        {
            var users = this.OpenConnection.GetList<User>(new { avatar = DBNull.Value });
            Assert.IsNotNull(users);
        }

        [Test]
        public void TestGetListPaged()
        {
            var logs = this.OpenConnection.GetListPaged<Log>(2, 10, null, null);
            Assert.IsNotNull(logs);
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
                users = this.OpenConnection.GetList<User>();
                Assert.IsNotNull(users);
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
