using ADB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public static class DatabaseConnection
{
    private static readonly object LockObject = new object();
    private static QLNHAHANGEntities1 _dbInstance;

    public static QLNHAHANGEntities1 GetInstance()
    {
        if (_dbInstance == null)
        {
            lock (LockObject)
            {
                if (_dbInstance == null)
                {
                    _dbInstance = new QLNHAHANGEntities1();
                }
            }
        }
        return _dbInstance;
    }

    public static void DisposeInstance()
    {
        lock (LockObject)
        {
            if (_dbInstance != null)
            {
                _dbInstance.Dispose();
                _dbInstance = null;
            }
        }
    }
}

namespace DatabaseConnectionHelper
{
    public class DbHelper
    {
        private readonly string _connectionString;

        public DbHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
