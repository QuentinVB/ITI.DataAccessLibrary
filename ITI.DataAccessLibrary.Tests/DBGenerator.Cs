using ITI.DataAccessLibrary.Model;
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
            if (File.Exists(_dbPath)) File.Delete(_dbPath);
            SQLiteConnection.CreateFile(_dbPath);

            using(SQLiteConnection connexion = new SQLiteConnection($"Data Source={_fileName};Version=3;"))
            {
                connexion.Open();
                CreateHarbourTable(connexion);
                connexion.Close();
            }


            

            //create based on mondel

            //List<Type> types = GetClasses("ITI.DataAccessLibrary.Model").ToList();

            /*foreach (var type in types)
            {
                //var instance = type.IsInstanceOfType();
                type.GetType().GetProperties();
            }*/

        }

        void CreateHarbourTable(SQLiteConnection connexion,int count = 100)
        {

            using (SQLiteTransaction transation = connexion.BeginTransaction())
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
                transation.Commit();
            }
        }

        string GetRandomName()
        {
            return Guid.NewGuid().ToString();
        }

        //Quentin's cave
        /*
        static IEnumerable<Type> GetClasses(string nameSpace)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetTypes()
                .Where(type => type.Namespace == nameSpace)
                .Select(type => type);
        }*/
    }
}
