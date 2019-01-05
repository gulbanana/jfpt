using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Xamarin.Forms;

namespace JFPT
{
    /// <summary>temporary placeholder module for db stuff</summary>
    /// <remarks>see https://docs.microsoft.com/en-us/dotnet/api/microsoft.data.sqlite?view=msdata-sqlite-2.0.0</remarks>
    public static class Data
    {
        private static string dbPath;
        private static string connectionString;

        public static void Init(string platform)
        {
            var dbName = "jfpt.etilqs";

            switch (platform)
            {
                case Device.iOS:
                    SQLitePCL.Batteries_V2.Init();
                    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", dbName); ;
                    break;

                case Device.Android:
                    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);
                    break;

                default:
                    dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName);
                    break;
            }

            // create a new db from scratch each run - we could just use SqliteOpenMode.InMemory but i want to test out file usage
            if (File.Exists(dbPath)) File.Delete(dbPath);

            var builder = new SqliteConnectionStringBuilder();
            builder.DataSource = dbPath;
            builder.Mode = SqliteOpenMode.ReadWriteCreate;
            connectionString = builder.ConnectionString;

            using (var connection = Connect())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "create table [Test] ([Hello] text)";
                    command.ExecuteNonQuery();

                    command.CommandText = "insert into [Test] values ('Hello, dataworld!')";
                    command.ExecuteNonQuery();
                }
            }
        }

        public static SqliteConnection Connect() => new SqliteConnection(connectionString);
    }
}
