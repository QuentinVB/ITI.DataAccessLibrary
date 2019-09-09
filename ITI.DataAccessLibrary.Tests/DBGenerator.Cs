using ITI.DataAccessLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ITI.DataAccessLibrary.Tests
{
    public class DBGenerator
    {
        readonly string _path;
        readonly string _fileName = "database.sqlite";
        readonly string _dbPath;
        SQLiteConnection _connexion;

        List<Harbor> _harbors;

        public List<Harbor> Harbors { get => _harbors; set => _harbors = value; }

        public DBGenerator()
        {
            _path = new FileInfo(Assembly.GetEntryAssembly().Location).Directory.ToString();
            _dbPath = $"{_path}{_fileName}";
            _harbors = new List<Harbor>();
        }

        public void CreateDatabase()
        {
            SQLiteConnection.CreateFile(_dbPath);
            _connexion = new SQLiteConnection($"Data Source={_fileName};Version=3;");

            //List<Type> types = GetClasses("ITI.DataAccessLibrary.Model").ToList();

            /*foreach (var type in types)
            {
                //var instance = type.IsInstanceOfType();
                type.GetType().GetProperties();
            }*/

        }
        void Execute(string query)
        {
            _connexion.Open();
            {
                //Execute("create table ships (name text, mass int)");
                //Execute("insert into ships(name, mass) values('VFRZ', 100)");

                //create based on mondel
                SQLiteCommand commande = new SQLiteCommand(query, _connexion);
                commande.ExecuteNonQuery();
            }
            _connexion.Close();
        }

        void CreateHarbourTable(int count = 100)
        {
            Execute("create table Harbour " +
                "(" +
                "Id int," +
                "Name text," +
                "LocalName text, " +
                "Country text, " +
                "Longitude real" +
                "Latitude real" +
                ")");

            
        }

        public string GetRandomName()
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
