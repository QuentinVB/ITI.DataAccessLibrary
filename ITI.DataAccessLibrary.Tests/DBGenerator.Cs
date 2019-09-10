using ITI.DataAccessLibrary.Correction.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
/// <summary>
/// http://zetcode.com/db/sqlitecsharp/trans/
/// http://www.tsjensen.com/blog/post/2012/11/10/SQLite-on-Visual-Studio-with-NuGet-and-Easy-Instructions
/// </summary>
namespace ITI.DataAccessLibrary.Tests
{
    public class DBGenerator
    {
        readonly string _path;
        readonly string _fileName = "database.sqlite";
        readonly string _dbPath;
        Random _randomSource;

        List<Harbor> _harbors;

        public List<Harbor> Harbors { get => _harbors; private set => _harbors = value; }

        public DBGenerator()
        {
            //_path = new FileInfo(Assembly.GetEntryAssembly().Location).Directory.ToString();
            _path = Environment.CurrentDirectory.ToString();
            _dbPath = $"{_path}\\{_fileName}";
            _randomSource = new Random();
            _harbors = new List<Harbor>();
        }

        public void CreateDatabase()
        {
            //clean actual data
            if(File.Exists(_dbPath)) File.Delete(_dbPath);            
            _harbors.Clear();

            //regenerate the data
            SQLiteConnection.CreateFile(_dbPath);

            using (SQLiteConnection connexion = new SQLiteConnection($"Data Source={_fileName};Version=3;"))
            {
                connexion.Open();
                CreateHarbourTable(connexion);
                connexion.Close();
            }


            

           
        }

        void CreateHarbourTable(SQLiteConnection connexion,int count = 100)
        {
            using (SQLiteTransaction transaction = connexion.BeginTransaction())
            {
                using (SQLiteCommand command = connexion.CreateCommand())
                {
                    command.CommandText = "create table Harbor " +
                                    "(" +
                                        "Id INTEGER PRIMARY KEY," +
                                        "Name text," +
                                        "LocalName text, " +
                                        "Country text, " +
                                        "Latitude real," +
                                        "Longitude real" +
                                    ")";
                    command.ExecuteNonQuery();

                    Harbor harbor;
                    for (int i = 0; i <= count; i++)
                    {
                        harbor = new Harbor()
                        {
                            Name = GetRandomName(),
                            LocalName = GetRandomName(),
                            Country = GetRandomName(),
                            Latitude = (_randomSource.NextDouble() * 180) - 90,
                            Longitude = (_randomSource.NextDouble() * 180) - 90
                        };

                        Harbors.Add(harbor);

                        command.CommandText ="insert into Harbor(Name, LocalName,Country,Longitude,Latitude) values(" +
                            $"'{harbor.Name}', " +
                            $"'{harbor.LocalName}'," +
                            $"'{harbor.Country}'," +
                            $"{harbor.Latitude.ToString(CultureInfo.InvariantCulture)}, " +
                            $"{harbor.Longitude.ToString(CultureInfo.InvariantCulture)}" +
                            ")";
                        command.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
            }
        }

        string GetRandomName()
        {
            //todo : generate real names instead of guid
            return Guid.NewGuid().ToString();
        }

    }
}
