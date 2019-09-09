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
        SQLiteConnection _connexion;
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
            _connexion = new SQLiteConnection($"Data Source={_fileName};Version=3;");

            _connexion.Open();

            CreateHarbourTable();

            _connexion.Close();

            //create based on mondel

            //List<Type> types = GetClasses("ITI.DataAccessLibrary.Model").ToList();

            /*foreach (var type in types)
            {
                //var instance = type.IsInstanceOfType();
                type.GetType().GetProperties();
            }*/

        }
        void Execute(string query)
        {
            {
                SQLiteCommand commande = new SQLiteCommand(query, _connexion);
                commande.ExecuteNonQuery();
            }
        }

        void CreateHarbourTable(int count = 100)
        {
            Execute("create table Harbor " +
                "(" +
                "Id INTEGER PRIMARY KEY," +
                "Name text," +
                "LocalName text, " +
                "Country text, " +
                "Latitude real," +
                "Longitude real" +
                ")");

            Harbor harbor;
            for (int i = 0; i <= count; i++)
            {
                harbor = new Harbor()
                {
                    Name = GetRandomName(),
                    LocalName = GetRandomName(),
                    Country = GetRandomName(),
                    Latitude = (_randomSource.NextDouble()*180 ) -90,
                    Longitude = (_randomSource.NextDouble() * 180) - 90
                };
                Harbors.Add(harbor);
                Execute("insert into Harbor(Name, LocalName,Country,Longitude,Latitude) values(" +
                    $"'{harbor.Name}', " +
                    $"'{harbor.LocalName}'," +
                    $"'{harbor.Country}'," +
                    $"{harbor.Latitude.ToString(CultureInfo.InvariantCulture)}, " +
                    $"{harbor.Longitude.ToString(CultureInfo.InvariantCulture)}" +
                    ")"
                    );
            }

            //Execute("insert into ships(name, mass) values('VFRZ', 100)");
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
