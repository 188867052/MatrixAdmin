using System;
using System.Collections.Generic;
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
                var users = DapperExtension.Connection.GetList<User>($"where login_name = '{user.LoginName}'");
                Assert.IsNotNull(users);
            }
        }

        [Test]
        public void TestFindAllByExpression()
        {
            User user = DapperExtension.Connection.QueryFirstOrDefault<User>("SELECT * FROM [user]");
            if (user != null)
            {
                var users = DapperExtension.Connection.FindAll<User>(o => o.Id, user.Id);
                Assert.AreEqual(users.Count, 1);
                user = DapperExtension.Connection.Find<User>(user.Id);
                Assert.IsNotNull(user);
                var roles = DapperExtension.Connection.FindAll<Role>();
                Assert.IsNotNull(roles);
            }
        }

        [Test]
        public void TestGetListNullableWhere()
        {
            var users = DapperExtension.Connection.GetList<User>(new { avatar = DBNull.Value });
            Assert.IsNotNull(users);
        }

        [Test]
        public void TestGetListPaged()
        {
            var logs = DapperExtension.Connection.GetListPaged<Log>(2, 10, null, null);
            Assert.IsNotNull(logs);
        }

        [Test]
        public void TestFilteredGetListParameters()
        {
            User user = DapperExtension.Connection.QueryFirstOrDefault<User>("SELECT * FROM [user]");
            if (user != null)
            {
                IEnumerable<User> users = DapperExtension.Connection.GetList<User>("where Id = @Id", new { user.Id });
                Assert.IsNotNull(users);
                users = DapperExtension.Connection.GetList<User>(new { user.Id });
                Assert.IsNotNull(users);
                users = DapperExtension.Connection.GetList<User>(new { });
                Assert.IsNotNull(users);
                users = DapperExtension.Connection.GetList<User>();
                Assert.IsNotNull(users);
            }
        }
    }
}
