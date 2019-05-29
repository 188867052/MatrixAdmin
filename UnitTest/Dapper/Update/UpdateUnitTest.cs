using System;
using System.Data;
using Core.Entity;
using Core.Extension.Dapper;
using Dapper;
using NUnit.Framework;

namespace Core.UnitTest.Dapper.Update
{
    /// <summary>
    /// Api unit test.
    /// </summary>
    [TestFixture]
    public class UpdateUnitTest
    {
        [Test]
        public void TestUpdate()
        {
            var message = Guid.NewGuid().ToString();
            var log = DapperExtension.Connection.QueryFirstOrDefault<Log>("SELECT TOP 1 * FROM [LOG]");
            log.Message = message;
            DapperExtension.Connection.Update(log);
            var logFind = DapperExtension.Connection.Find<Log>(log.Id);

            Assert.AreEqual(logFind.Message, message);
        }

        [Test]
        public void TestMassUpdate()
        {
            // TODO:IT is better cache string builder.
            var log = DapperExtension.Connection.QueryFirstOrDefault<Log>("SELECT TOP 1 * FROM [LOG]");
            if (log != null)
            {
                int count = 0;
                using (var connection = DapperExtension.Connection)
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        for (int i = 0; i < 1000; i++)
                        {
                            log.Message =Guid.NewGuid().ToString();
                            count += DapperExtension.Connection.Update(log, transaction);
                        }
                    }
                }

                Assert.AreEqual(count, 1000);
            }
        }
    }
}