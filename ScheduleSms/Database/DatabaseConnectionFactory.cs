﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace ScheduleSms.Database
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string _connectionString;

        public DatabaseConnectionFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        /// <summary>
        /// Create DbConnection
        /// </summary>
        /// <returns></returns>
        public IDbConnection Create()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            return sqlConnection;
        }
    }
}