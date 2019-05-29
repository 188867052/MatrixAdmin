using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;
using Core.Extension.Dapper;
using Dapper;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Core.UnitTest.Dapper
{
    /// <summary>
    /// Api unit test.
    /// </summary>
    [TestFixture]
    public class InsertUnitTest
    {
        [Test]
        public void TestInsertWithSpecifiedPrimaryKey()
        {
            var log = new Log { LogLevel = (int)LogLevel.Information, CreateTime = DateTime.Now, Message = "TestInsertWithSpecifiedPrimaryKey" };
            var id = DapperExtension.Connection.InsertReturnKey(log);
            Assert.Greater(id, 0);
        }

        [Test]
        public void TestInsertAsyncWithSpecifiedPrimaryKey()
        {
            var log = new Log { LogLevel = (int)LogLevel.Information, CreateTime = DateTime.Now, Message = "TestInsertWithSpecifiedPrimaryKey" };
            var idTask = DapperExtension.Connection.InsertAsync(log);
            idTask.Wait();
            var id = idTask.Result;
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
        public void TestInsertAsyncWithMultiplePrimaryKeys()
        {
            Task<dynamic> task = DapperExtension.Connection.InsertAsync(new MultiplePrimaryKeyTable { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N") });
            task.Wait();
            var result = task.Result;
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
        public void TestInsertAsyncUsingGenericLimitedFields()
        {
            var log = new Log { LogLevel = (int)LogLevel.Information, Message = "TestInsertWithSpecifiedPrimaryKey" };
            var task = DapperExtension.Connection.InsertAsync(log);
            task.Wait();
            Assert.IsNotNull(task.Result);
        }

        [Test]
        public void TestMassInsert()
        {
            // TODO:It is better to cache string builder.
            int count = 0;
            using (var transaction = DapperExtension.BeginTransaction())
            {
                for (int i = 0; i < 2; i++)
                {
                    Log log = new Log() { Message = SimpleCRUD.SequentialGuid().ToString(), LogLevel = (int)LogLevel.None };
                    count += DapperExtension.Connection.Insert(log, transaction);
                }

                transaction.Commit();
            }

            Assert.AreEqual(count, 2);
        }
    }
}