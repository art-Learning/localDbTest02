﻿using System.Data.SqlClient;
using System.IO;
using Dapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ScheduleSms.Database;
using ScheduleSms.Service;
using ScheduleSmsTests.Misc;

namespace ScheduleSmsTests.Service
{
    [TestClass()]
    [DeploymentItem(@"DbScripts\SampleDB_TRDATA_Create.sql")]
    [DeploymentItem(@"DbScripts\SampleDB_TRDATA_Data.sql")]
    public class Test01
    {
        private IDatabaseConnectionFactory DatabaseConnectionFactory { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DatabaseConnectionFactory = Substitute.For<IDatabaseConnectionFactory>();

            DatabaseConnectionFactory
                .Create()
                .Returns(new SqlConnection(TestHook.SampleDbConnection));
        }

        public TestContext TestContext { get; set; }

        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            DropTable();
            CreateTable();
            PrepareData();
        }

        private static void CreateTable()
        {
            using (var conn = new SqlConnection(TestHook.SampleDbConnection))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    var script = File.ReadAllText(@"SampleDB_TRDATA_Create.sql");
                    conn.Execute(sql: script, transaction: trans);
                    trans.Commit();
                }
            }
        }

        private static void PrepareData()
        {
            using (var conn = new SqlConnection(TestHook.SampleDbConnection))
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    var script = File.ReadAllText(@"SampleDB_TRDATA_Data.sql");
                    conn.Execute(sql: script, transaction: trans);
                    trans.Commit();
                }
            }
        }

        [ClassCleanup()]
        public static void TestClassCleanup()
        {
            DropTable();
        }

        private static void DropTable()
        {
            using (var conn = new SqlConnection(TestHook.SampleDbConnection))
            {
                conn.Open();
                var sqlCommand = TableCommands.DropTable("TEST");
                conn.Execute(sqlCommand);
            }
        }

        [TestMethod]
        public void TestDoSomething()
        {
            var sut = new FirstClass(DatabaseConnectionFactory);
            var actual = sut.DoSomething();
            actual.Should().BeFalse();
        }
    }
}