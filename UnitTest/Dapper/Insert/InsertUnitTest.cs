using System;
using System.Collections.Generic;
using System.Linq;
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
            // TODO: can`t set value to primary key. and cant`t set default value when a column not included.
            var log = new Log { LogLevel = (int)LogLevel.Information, CreateTime = DateTime.Now, Message = "TestInsertWithSpecifiedPrimaryKey" };
            var id = DapperExtension.Connection.Insert(log);
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
            string id = DapperExtension.Connection.Insert(keyMaster);
            Assert.IsNotNull(id);
        }

        [Test]
        public void TestInsertAsyncWithMultiplePrimaryKeys()
        {
            var idTask = DapperExtension.Connection.InsertAsync(new MultiplePrimaryKeyTable { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N") });
            idTask.Wait();
            var result = idTask.Result;
            Assert.IsNotNull(result);
        }
    }
}