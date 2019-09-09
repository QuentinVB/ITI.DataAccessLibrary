using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DataAccessLibrary.DebugCLI
{
    class Program
    {

        static void Main(string[] args)
        {

            string _path = new FileInfo(Assembly.GetEntryAssembly().Location).Directory.ToString();
            string _fileName = "database.sqlite";
            string _dbPath = $"{_path}{_fileName}";

            SQLiteConnection.CreateFile(_dbPath);

            //connect db
            SQLiteConnection _connexion = new SQLiteConnection($"Data Source={_fileName};Version=3;");
            //connexion context
            _connexion.Open();
            {
                Execute("create table ships (name text, mass int)");
                Execute("insert into ships(name, mass) values('VFRZ', 100)");
                
                //create based on mondel

            }
            _connexion.Close();

            void Execute(string query)
            {
                SQLiteCommand commande = new SQLiteCommand(query, _connexion);
                commande.ExecuteNonQuery();
            }
            Console.ReadLine();
        }
        
    }
}
