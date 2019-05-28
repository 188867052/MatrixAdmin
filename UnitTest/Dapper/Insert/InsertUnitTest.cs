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
            var idTask = DapperExtension.Connection.InsertAsync<int?, Log>(log);
            idTask.Wait();
            id = idTask.Result;
            Assert.Greater(id, 0);
        }

        [Test]
        public void TestInsertWithMultiplePrimaryKeysAsync()
        {
            var keyMaster = new MultiplePrimaryKeyTable { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N") };
            var id = DapperExtension.Connection.Insert<string, MultiplePrimaryKeyTable>(keyMaster);
            Assert.IsNotNull(id);
            var idTask = DapperExtension.Connection.InsertAsync<string, MultiplePrimaryKeyTable>(new MultiplePrimaryKeyTable { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N") });
            idTask.Wait();
            var result = idTask.Result;
            Assert.IsNotNull(id);
        }
    }
}
