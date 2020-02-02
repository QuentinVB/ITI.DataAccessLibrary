using System;
using System.Data.SQLite;

namespace ITI.DataAccessLibrary
{
    public abstract class Queries
    {
        readonly string _path;
        readonly string _fileName = "database.sqlite";
        readonly string _dbPath;
        private readonly string connString;

        public Queries()
        {
            _path = Environment.CurrentDirectory.ToString();
            _dbPath = $"{_path}\\{_fileName}";
            connString = $"Data Source={_fileName};Version=3;";
        }

        internal string ConnString => connString;
    }
}
