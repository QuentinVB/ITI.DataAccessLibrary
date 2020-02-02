using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ITI.DataAccessLibrary
{
    public abstract class Queries
    {
        readonly string _path;
        readonly string _fileName = "database.sqlite";
        readonly string _dbPath;
        readonly internal string _connString;

        internal SQLiteConnection _connexion;
        public Queries()
        {
            _path = Environment.CurrentDirectory.ToString();
            _dbPath = $"{_path}\\{_fileName}";
            _connString = $"Data Source={_fileName};Version=3;";
        }

        internal void ExecuteQuery(string query)
        {

            using (_connexion = new SQLiteConnection($"Data Source={_fileName};Version=3;"))
            {
                using (SQLiteCommand command = _connexion.CreateCommand())
                {
                    command.CommandText = query;
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            
                        }
                    }
                }
            }
        }
    }
}
