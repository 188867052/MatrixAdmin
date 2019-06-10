using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;
using Core.Extension.Dapper;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Core.UnitTest.Dapper
{
    /// <summary>
    /// Dapper unit test.
    /// </summary>
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public async Task TestRecordCountAsync()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                var count = DapperExtension.Connection.RecordCount<User>($"where Id = '{user.Id}'");
                Assert.AreEqual(count, 1);
                count = DapperExtension.Connection.RecordCount<User>();
                Assert.GreaterOrEqual(count, 0);
                count = await DapperExtension.Connection.RecordCountAsync<User>();
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public async Task TestRecordCount()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                int count = await DapperExtension.Connection.RecordCountAsync<User>();
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [TestCase(1)]
        public async Task TestRecordCountByObjectAsync(int id)
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                int count = await DapperExtension.Connection.RecordCountAsync<User>(new { id });
                Assert.GreaterOrEqual(count, 0);
                count = DapperExtension.Connection.RecordCount<User>(new { id });
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public async Task TestRecordCountByObjectAsyncIgnoreCaseAsync()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                int count = await DapperExtension.Connection.RecordCountAsync<User>(new { ID = 10 });
                Assert.GreaterOrEqual(count, 0);
            }
        }

        [Test]
        public async Task TestDeleteById()
        {
            Log log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.Delete<Log>(log.Id);
                Assert.AreEqual(count, 1);
            }

            log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                int count = await DapperExtension.Connection.DeleteAsync<Log>(log.Id);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestDeleteListWithWhereClause()
        {
            Log log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteList<Log>($"Where id = {log.Id}");
                Assert.AreEqual(count, 1);
            }

            log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteList<Log>(new { log.Id });
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public async Task TestDeleteWithParameters()
        {
            Log log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.DeleteList<Log>("where Id = @Id", new { log.Id });
                Assert.AreEqual(count, 1);
            }

            log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                var count = await DapperExtension.Connection.DeleteListAsync<Log>("where Id = @Id", new { log.Id });
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestDeleteByObject()
        {
            Log log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                var count = DapperExtension.Connection.Delete(log);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public async Task TestDeleteByObjectAsync()
        {
            Log log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                int count = await DapperExtension.Connection.DeleteAsync(log);
                Assert.AreEqual(count, 1);
            }

            log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                int count = DapperExtension.Connection.Delete(log);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public async Task TestDeleteByMultipleKeyObjectAsync()
        {
            MultiplePrimaryKeyTable entity = DapperExtension.Connection.QueryFirst<MultiplePrimaryKeyTable>();
            if (entity != null)
            {
                var count = await DapperExtension.Connection.DeleteAsync(entity);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestDeleteByMultipleKeyObject()
        {
            MultiplePrimaryKeyTable entity = DapperExtension.Connection.QueryFirst<MultiplePrimaryKeyTable>();
            if (entity != null)
            {
                var count = DapperExtension.Connection.Delete(entity);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public async Task TestGet()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                var users = DapperExtension.Connection.Get<User>(1);
                Assert.IsNotNull(users);
                users = await DapperExtension.Connection.GetAsync<User>(new { id = 1 });
                Assert.IsNotNull(users);
            }
        }

        [Test]
        public void TestGetList()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                var users = DapperExtension.Connection.GetList<User>($"where login_name = '{user.LoginName}'");
                Assert.IsNotNull(users);
                users = DapperExtension.Connection.GetList<User>(new { login_name = "Strange 2" });
                Assert.IsNotNull(users);
            }
        }

        [Test]
        public void TestUpdateWithSpecifiedColumnName()
        {
            var log = DapperExtension.Connection.FindAll<Log>().FirstOrDefault();
            if (log != null)
            {
                int count = DapperExtension.Connection.Delete<Log>(log.Id);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public void TestFindAllByExpression()
        {
            using (var dapper = DapperExtension.Connection)
            {
                User user = DapperExtension.Connection.QueryFirst<User>();
                if (user != null)
                {
                    var users = dapper.FindAll<User>(o => o.Id, user.Id);
                    Assert.AreEqual(users.Count, 1);
                    user = dapper.Find<User>(user.Id);
                    Assert.IsNotNull(user);
                    var roles = dapper.FindAll<Role>();
                    Assert.IsNotNull(roles);
                }
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
            logs = DapperExtension.Connection.GetListPaged<Log>(1, 30, "where Id > @Id", null, new { Id = 14 });
            Assert.IsNotNull(logs);
        }

        [Test]
        public async Task TestGetListPagedAsync()
        {
            var logs = await DapperExtension.Connection.GetListPagedAsync<Log>(2, 10, null, null);
            Assert.IsNotNull(logs);
            logs = await DapperExtension.Connection.GetListPagedAsync<Log>(1, 30, "where Id > @Id", null, new { Id = 14 });
            Assert.IsNotNull(logs);
        }

        [Test]
        public void TestFilteredGetListParameters()
        {
            User user = DapperExtension.Connection.QueryFirst<User>();
            if (user != null)
            {
                IEnumerable<User> users = DapperExtension.Connection.GetList<User>("where Id = @Id", new { user.Id });
                Assert.IsNotNull(users);
                users = DapperExtension.Connection.GetList<User>(new { user.Id });
                Assert.IsNotNull(users);
            }
        }

        [Test]
        public void TestGetListWithNullWhere()
        {
            var users = DapperExtension.Connection.GetList<User>(null);
            Assert.IsNotNull(users);
            users = DapperExtension.Connection.GetList<User>(new { });
            Assert.IsNotNull(users);
            users = DapperExtension.Connection.GetList<User>();
            Assert.IsNotNull(users);
        }

        [Test]
        public async Task TestGetListWithNullWhereAsync()
        {
            var users = await DapperExtension.Connection.GetListAsync<User>(null);
            Assert.IsNotNull(users);
            users = await DapperExtension.Connection.GetListAsync<User>(new { });
            Assert.IsNotNull(users);
            users = await DapperExtension.Connection.GetListAsync<User>();
            Assert.IsNotNull(users);
        }

        [Test]
        public void TestInsertWithSpecifiedPrimaryKey()
        {
            var log = new Log { LogLevel = (int)LogLevel.Information, CreateTime = DateTime.Now, Message = "TestInsertWithSpecifiedPrimaryKey" };
            var id = DapperExtension.Connection.InsertReturnKey(log);
            Assert.Greater(id, 0);
        }

        [Test]
        public async Task TestInsertAsyncWithSpecifiedPrimaryKeyAsync()
        {
            var log = new Log { LogLevel = (int)LogLevel.Information, CreateTime = DateTime.Now, Message = "TestInsertWithSpecifiedPrimaryKey" };
            var idTask = DapperExtension.Connection.InsertAsync(log);
            var id = await idTask;
            Assert.Greater(id, 0);
        }

        [Test]
        public void TestInsertWithMultiplePrimaryKeys()
        {
            var keyMaster = new MultiplePrimaryKeyTable { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N") };
            string id = DapperExtension.Connection.InsertReturnKey(keyMaster);
            Assert.IsNotNull(id);
        }

        [Test]
        public async Task TestInsertAsyncWithMultiplePrimaryKeysAsync()
        {
            Task<dynamic> task = DapperExtension.Connection.InsertAsync(new MultiplePrimaryKeyTable { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N") });
            var result = await task;
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestInsertUsingGenericLimitedFields()
        {
            var log = new Log { LogLevel = (int)LogLevel.Information, Message = "TestInsertWithSpecifiedPrimaryKey" };
            var id = DapperExtension.Connection.InsertReturnKey(log);
            Assert.Greater(id, 0);
        }

        [Test]
        public async Task TestInsertAsyncUsingGenericLimitedFieldsAsync()
        {
            var log = new Log { LogLevel = (int)LogLevel.Information, Message = "TestInsertWithSpecifiedPrimaryKey" };
            dynamic task = await DapperExtension.Connection.InsertAsync(log);
            Assert.IsNotNull(task);
        }

        [Test]
        public void TestMassInsert()
        {
            int count = 0;
            using (var transaction = DapperExtension.BeginTransaction())
            {
                for (int i = 0; i < 2; i++)
                {
                    Log log = new Log { Message = Guid.NewGuid().ToString(), LogLevel = (int)LogLevel.None };
                    count += DapperExtension.Connection.Insert(log, transaction);
                }

                transaction.Commit();
            }

            Assert.AreEqual(count, 2);
        }

        [Test]
        public void TestUpdate()
        {
            var message = Guid.NewGuid().ToString();
            var log = DapperExtension.Connection.QueryFirst<Log>();
            log.Message = message;
            DapperExtension.Connection.Update(log);
            var logFind = DapperExtension.Connection.Find<Log>(log.Id);

            Assert.AreEqual(logFind.Message, message);
        }

        [Test]
        public void TestMassUpdate()
        {
            var log = DapperExtension.Connection.QueryFirst<Log>();
            if (log != null)
            {
                int count = 0;
                using (var transaction = DapperExtension.BeginTransaction())
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        log.Message = Guid.NewGuid().ToString();
                        count += DapperExtension.Connection.Update(log, transaction);
                    }

                    transaction.Commit();
                }

                Assert.AreEqual(count, 1000);
            }
        }
    }
}
